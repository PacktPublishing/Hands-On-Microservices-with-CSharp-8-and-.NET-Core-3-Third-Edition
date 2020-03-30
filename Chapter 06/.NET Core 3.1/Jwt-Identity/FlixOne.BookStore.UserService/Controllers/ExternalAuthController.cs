using FlixOne.BookStore.UserService.Extensions;
using FlixOne.BookStore.UserService.Helper;
using FlixOne.BookStore.UserService.Models;
using FlixOne.BookStore.UserService.Models.Identity;
using FlixOne.BookStore.UserService.Utility;
using FlixOne.BookStore.VendorService.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlixOne.BookStore.UserService.Controllers
{
    /// <summary>
    /// External Auth controller
    /// </summary>
    [ApiController]
    public class ExternalAuthController : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly FacebookAuthSettings _fbAuthSettings;
        private readonly ITokenUtility _tokenUtility;
        private readonly JwtSettingOptions _jwtOptions;
        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// ctor
        /// </summary>
        public ExternalAuthController(IOptions<FacebookAuthSettings> fbAuthSetting, UserManager<AppUser> userManager, UserContext userContext, ITokenUtility tokenUtility, IOptions<JwtSettingOptions> jwtOptions)
        {
            _fbAuthSettings = fbAuthSetting.Value;
            _userManager = userManager;
            _userContext = userContext;
            _tokenUtility = tokenUtility;
            _jwtOptions = jwtOptions.Value;
        }
        // POST api/externalauth/facebook
        /// <summary>
        /// Loging using your FB account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>jwt</returns>
        [HttpPost]
        [Route("api/external/facebook")]
        public async Task<IActionResult> Facebook([FromBody]FacebookAuthViewModel model)
        {
            // 1.generate an app access token
            var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_fbAuthSettings.AppId}&client_secret={_fbAuthSettings.AppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
            // 2. validate the user access token
            var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                return BadRequest(new ErrorResponseModel
                {
                    Message = $"{Constants.Errors.FbLoginFailed.Code},{Constants.Errors.FbLoginFailed.Desc}",
                    Code = 400,
                    Exception = nameof(Constants.Errors.FbLoginFailed)
                });
            }

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new AppUser
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    FacebookId = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    PictureUrl = userInfo.Picture.Data.Url
                };

                var result = await _userManager.CreateAsync(appUser, (Guid.NewGuid().ToByteArray().ToBase64String()).Substring(0, 8));

                if (!result.Succeeded)
                {
                    ModelState.AddErrorsToModelState(result);
                    return BadRequestDueToModelState();
                }

                await _userContext.Users.AddAsync(new User { IdentityId = appUser.Id, Gender = userInfo.Gender });
                await _userContext.SaveChangesAsync();
            }

            // generate the jwt for the local user...
            var localUser = await _userManager.FindByNameAsync(userInfo.Email);

            if (localUser == null)
            {
                //mark modelstate as invalid
                //return BadRequest(ModelState.AddErrorToModelState(Constants.Errors.LoginFailed.Code,Constants.Errors.LoginFailed.Desc));
                return BadRequest(new ErrorResponseModel
                {
                    Message = $"{Constants.Errors.LoginFailed.Code},{Constants.Errors.LoginFailed.Desc}",
                    Code = 400,
                    Exception = nameof(Constants.Errors.LoginFailed)
                });
            }
            var identity = _tokenUtility.GenerateClaimsIdentity(localUser.UserName, localUser.Id);
            var jwt = await identity.GenerateJwtJson(_tokenUtility, localUser.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }
        private BadRequestObjectResult BadRequestDueToModelState()
        {
            return BadRequest(new ErrorResponseModel
            {
                Message = ModelState.ToErrorMessage(),
                Code = 400,
                Exception = ModelState.GetType().Name
            });
        }
    }
}

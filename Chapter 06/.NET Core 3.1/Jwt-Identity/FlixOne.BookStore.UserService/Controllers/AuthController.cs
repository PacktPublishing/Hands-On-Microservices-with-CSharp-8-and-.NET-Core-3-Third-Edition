using FlixOne.BookStore.UserService.Extensions;
using FlixOne.BookStore.UserService.Helper;
using FlixOne.BookStore.UserService.Models;
using FlixOne.BookStore.UserService.Models.Identity;
using FlixOne.BookStore.UserService.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlixOne.BookStore.UserService.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenUtility _tokenUtility;
        private readonly JwtSettingOptions _options;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="tokenUtility"></param>
        /// <param name="options"></param>

        public AuthController(UserManager<AppUser> userManager, ITokenUtility tokenUtility, IOptions<JwtSettingOptions> options)
        {
            _userManager = userManager;
            _tokenUtility = tokenUtility;
            _options = options.Value;
        }

        ///  <summary>
        ///      Generate Auth Token
        ///  </summary>
        ///  <remarks>
        ///  Example:
        /// 
        ///      POST /api/security/token
        ///      {
        ///         "LoginId": "login@flixone.com",
        ///         "Password": "password123"
        ///      }
        ///  
        ///  </remarks>
        /// <param name="authRequest">Parameters to validate request <c>authRequest</c> </param>
        /// <returns>jwt token</returns>
        ///  <response code="200">Return jwt token</response>
        ///  <response code="400">Request is not valid, operation aborted.</response>
        ///  <response code="401">User is not found, operation aborted.</response>

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [Route("api/security/token")]
        public async Task<IActionResult> Token([FromBody] AuthRequestViewModel authRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestDueToModelState();
            }
            try
            {
                var identity = await GetClaimsIdentity(authRequest.LoginId, authRequest.Password);
                if (identity == null)
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

                var jwt = await identity.GenerateJwtJson(_tokenUtility, authRequest.LoginId, _options, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            catch (Exception ex)
            {   
                return BadRequest(new ErrorResponseModel
                {
                    Message = ex.Message,
                    Code = 400,
                    Exception = ex.GetType().Name
                });
            }
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

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_tokenUtility.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}

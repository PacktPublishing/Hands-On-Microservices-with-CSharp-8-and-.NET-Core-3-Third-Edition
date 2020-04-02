using AutoMapper;
using FlixOne.BookStore.UserService.Extensions;
using FlixOne.BookStore.UserService.Models;
using FlixOne.BookStore.UserService.Models.Identity;
using FlixOne.BookStore.VendorService.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FlixOne.BookStore.UserService.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="mapper"></param>
        /// <param name="userContext"></param>
        public UserController(UserManager<AppUser> userManager, IMapper mapper, UserContext userContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userContext = userContext;
        }
        /// <summary>
        ///     creates a new user
        /// </summary>
        ///  <remarks>
        ///  Example:
        /// 
        ///      POST /api/user/register
        ///      {
        ///         "FirstName": "Gaurav",
        ///         "LastName": "Arora",
        ///         "EmailId": "login@flixone.com",
        ///         "Password": "password123",
        ///         "Mobile": "1234567890"
        ///      }
        ///  
        ///  </remarks> 
        /// <param name="registerUser"></param>
        /// <returns>specific user</returns>
        [HttpPost]
        [Route("api/user/register", Name = "Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestDueToModelState();
            }

            try
            {
                var userIdentity = _mapper.Map<AppUser>(registerUser);

                var result = await _userManager.CreateAsync(userIdentity, registerUser.Password);

                if (!result.Succeeded)
                {
                    ModelState.AddErrorsToModelState(result);
                    return BadRequestDueToModelState();
                }

                await _userContext.Users.AddAsync(new User { IdentityId = userIdentity.Id, FirstName = registerUser.FirstName, LastName = registerUser.LastName });
                await _userContext.SaveChangesAsync();

                return new OkObjectResult("User registered successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseModel
                {
                    Message = ex.Message,
                    Exception = ex.GetType().Name,
                    Code = 400
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
    }
}

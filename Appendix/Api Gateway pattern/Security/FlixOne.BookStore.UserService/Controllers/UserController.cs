using System;
using FlixOne.BookStore.ProductService.Persistence;
using FlixOne.BookStore.UserService.Models;
using FlixOne.BookStore.UserService.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlixOne.BookStore.UserService.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenUtility _tokenUtility;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="tokenUtility"></param>
        public UserController(IUserRepository userRepository, ITokenUtility tokenUtility)
        {
            _userRepository = userRepository;
            _tokenUtility = tokenUtility;
        }
        /// <summary>
        ///     creates a new user
        /// </summary>
        /// <remarks>
        /// 
        /// Example:
        ///     POST /api/user/register
        ///     {
        ///         "FirstName":"Gaurav",
        ///         "LastName":"Arora",
        ///         "EmailId":"login@flixone.com",
        ///         "Password":"password123",
        ///         "Mobile":"1234567890"
        ///      }
        ///      
        /// </remarks>
        /// <param name="registerUser"></param>
        /// <returns>specific user</returns>
        [HttpPost]
        [Route("api/user/register", Name = "Register")]
        public IActionResult RegisterUser([FromBody] RegisterUser registerUser)
        {
            if (registerUser == null)
                return BadRequest(new ErrorResponseModel
                {
                    Message = "Null entity supplied.",
                    Code = 400
                });
            
            if (string.IsNullOrWhiteSpace(registerUser.FirstName))
                return BadRequest(new ErrorResponseModel
                {
                    Message = "First name is required.",
                    Code = 400
                });
            if (string.IsNullOrWhiteSpace(registerUser.LastName))
                return BadRequest(new ErrorResponseModel
                {
                    Message = "Last name is required.",
                    Code = 400
                });

            if (string.IsNullOrWhiteSpace(registerUser.Password))
                return BadRequest(new ErrorResponseModel
                {
                    Message = "Password is required.",
                    Code = 400
                });
            if (_userRepository.FindBy(registerUser.EmailId) != null)
                return BadRequest(new ErrorResponseModel
                {
                    Message = $"Email '{registerUser.EmailId}' is registered with someone else.",
                    Code = 400
                });
            if (_userRepository.FindBy(registerUser.Mobile) != null)
            {
                return BadRequest(new ErrorResponseModel
                {
                    Message = $"Mobile '{registerUser.Mobile}' is registered with someone else.",
                    Code = 400
                });
            }

            var userModel = new User
            {
                FirstName=registerUser.FirstName,
                LastName = registerUser.LastName,
                EmailId=registerUser.EmailId,
                Mobile=registerUser.Mobile,
                UserType = "customer"
            };
            
            try
            {

                var user = _userRepository.CreateUser(userModel, registerUser.Password);
                var newUser = new UserViewModel
                {
                    UserId = user.Id.ToString(),
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    EmailId=user.EmailId,
                    Mobile=user.Mobile,
                    UserType=user.UserType,
                    IsActive = user.IsActive
                };
               
                return Ok(newUser);
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
    }
}

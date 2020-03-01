using FlixOne.BookStore.ProductService.Persistence;
using FlixOne.BookStore.UserService.Models;
using FlixOne.BookStore.UserService.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FlixOne.BookStore.UserService.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenUtility _tokenUtility;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="tokenUtility"></param>

        public AuthController(IUserRepository userRepository, ITokenUtility tokenUtility)
        {
            _userRepository = userRepository;
            _tokenUtility = tokenUtility;
        }
        ///  <summary>
        ///      Generate Auth Token
        ///  </summary>
        ///  <remarks>
        ///  Example:
        /// 
        ///      POST /api/security/token/false
        ///      {
        ///         "LoginId": "login@flixone.com",
        ///         "Password": "password123"
        ///      }
        ///  
        ///  </remarks>
        /// <param name="authRequest">Parameters to validate request <c>authRequest</c> </param>
        /// <param name="isLong"></param>
        /// <returns>jwt token</returns>
        ///  <response code="200">Return jwt token</response>
        ///  <response code="400">Request is not valid, operation aborted.</response>
        ///  <response code="401">User is not found, operation aborted.</response>

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [Route("api/security/token/{isLong:bool}")]
        public IActionResult Token([FromBody] AuthRequest authRequest, bool isLong = false)
        {
            try
            {
                return GetToken(authRequest, isLong, true);
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
        private IActionResult GetToken(AuthRequest authRequest, bool isLong = false, bool returnToken = false)
        {
            var user = GetUserByLogin(authRequest);

            if (user != null)
            {
                user = _userRepository.FindBy(authRequest);
            }

            if (user == null)
            {

                return NotFound(new ErrorResponseModel
                {
                    Message = "Invalid credentials.",
                    Code = 401
                });
            }

            if (!user.IsActive)
            {


                return BadRequest(new ErrorResponseModel
                {
                    Message = $"User '{user.UserName}' is not active.",
                    Code = 400
                });
            }

            return GetJwtToken(isLong, returnToken, user);
        }
        private User GetUserByLogin(AuthRequest authRequest)
        {
            var users = _userRepository.GetAll();
            var user = users.Where(x => (string.Equals(x.EmailId, authRequest.LoginId, StringComparison.OrdinalIgnoreCase)) ||
                                                                 (string.Equals(x.UserName, authRequest.LoginId, StringComparison.OrdinalIgnoreCase)) ||
                                                                 (x.Mobile == authRequest.LoginId));
            return user.FirstOrDefault();
        }

        private IActionResult GetJwtToken(bool isLong, bool returnToken, User user)
        {

            //put other checks here
            var token = _tokenUtility.GenerateToken(user, isLong);
            var jwt = _tokenUtility.JwtSecurityTokenSerialized(token);

            return returnToken
                ? Ok(jwt)
                : Ok(new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.UserName,
                    token = jwt
                });
        }
    }
}

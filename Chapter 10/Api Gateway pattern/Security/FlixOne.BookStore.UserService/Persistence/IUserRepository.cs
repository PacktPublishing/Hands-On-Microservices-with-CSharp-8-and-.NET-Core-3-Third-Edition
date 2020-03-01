using FlixOne.BookStore.UserService.Models;
using System.Collections.Generic;

namespace FlixOne.BookStore.ProductService.Persistence
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository
    {
        IEnumerable<User> GetAll(bool isActive = false);
        User CreateUser(User userModel, string userPassword);
        User FindBy(AuthRequest authRequest, bool isActive = false);
        User FindBy(string loginId, bool isActive = false);




    }
}
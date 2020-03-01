using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FlixOne.BookStore.UserService.Models;
using FlixOne.BookStore.VendorService.Contexts;

namespace FlixOne.BookStore.ProductService.Persistence
{
   
    /// <summary>
    /// User repository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAll(bool isActive = false)
        {
            return isActive
                ? _context.Users.Where(u => u.IsActive).ToList()
                : _context.Users.ToList();
        }
        public User CreateUser(User userModel, string userPassword)
        {
            var userName = CreateUserName(userModel.FirstName, userModel.LastName);

            var password = CreatePasswordHash(userPassword);
            userModel.UserName = userName;
            userModel.PasswordHash = password.Item1;
            userModel.PasswordSalt = password.Item2;

            userModel.IsActive = true; //activate account on registration, not recommended for production
            _context.Users.Add(userModel);
            _context.SaveChanges();
            //return user
            return _context.Users.Single(u => u.Id == userModel.Id);
           
        }

        public User FindBy(AuthRequest authRequest, bool isActive = false)
        {
            var user = FindBy(authRequest.LoginId, isActive);
            if (user == null) throw new ArgumentException("You are not registered with us.");
            if (VerifyPasswordHash(authRequest.Password, user.PasswordHash, user.PasswordSalt)) return user;
            throw new ArgumentException("Incorrect username or password.");
        }
        public User FindBy(string loginId, bool isActive = false)
        {
            var user = isActive
                ? _context.Users.Where(u => string.Equals(u.EmailId, loginId) || u.Mobile == loginId || string.Equals(u.UserName, loginId) && u.IsActive).FirstOrDefault()
                : _context.Users.Where(u => string.Equals(u.EmailId, loginId) || u.Mobile == loginId || string.Equals(u.UserName, loginId)).FirstOrDefault();
            return user;
        }
        private string CreateUserName(string firstName, string lastName)
        {
            //refine thie logic
            var userName = $"{lastName}{firstName.Substring(0, 1)}";
            var user = GetUserBy(userName);
            var enumerable = user.ToList();
            return enumerable.Any() ? $"{userName}{enumerable.Count}" : userName;
        }

        private IEnumerable<User> GetUserBy(string userName)
        {
            var user = _context.Users.Where(u => string.Equals(u.UserName, userName)).ToList();
            return user;
        }
        //This should be moved to utility or helper project
        private Tuple<byte[], byte[]> CreatePasswordHash(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            byte[] passwordSalt;
            byte[] passwordHash;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            return new Tuple<byte[], byte[]>(passwordHash, passwordSalt);
        }
        
        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (passwordHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(passwordHash));
            if (passwordSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).",              nameof(passwordSalt));

            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                if (computedHash.Where((t, i) => t != passwordHash[i]).Any()) return false;
            }

            return true;
        }
    }
}
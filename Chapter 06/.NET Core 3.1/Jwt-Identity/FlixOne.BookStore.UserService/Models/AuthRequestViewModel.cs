namespace FlixOne.BookStore.UserService.Models
{
    /// <summary>
    /// Auth request
    /// </summary>
    public class AuthRequestViewModel
    {
        /// <summary>
        /// Login <c>id</c>, Email <c>id</c> or Mobile Number
        /// </summary>
        public string LoginId { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}

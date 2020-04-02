using Microsoft.AspNetCore.Http;

namespace FlixOne.BookStore.UserService.Extensions
{
    /// <summary>
    /// Response extension
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Application error
        /// </summary>
        /// <param name="response"></param>
        /// <param name="message"></param>
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            // CORS
            response.Headers.Add("access-control-expose-headers", "Application-Error");
        }
    }
}

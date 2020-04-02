using FlixOne.BookStore.UserService.Utility;
using System;

namespace FlixOne.BookStore.UserService.Extensions
{
    public static class JwtOptionExtension
    {
        public static void ShoutOnErrors(this JwtSettingOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtSettingOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtSettingOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtSettingOptions.JtiGenerator));
            }
        }
    }
}

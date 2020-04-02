namespace FlixOne.BookStore.OrderService.Helper
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class JwtClaims
            {
                public const string FlixOneApiAccess = "flixone_api_access";
            }
        }

        public static class Errors
        {
            public static class LoginFailed
            {
                public const string Code = "login_failure", Desc = "Invalid loginId/username or password";
            }
            public static class FbLoginFailed
            {
                public const string Code = "login_failure", Desc = "Invalid facebook token";
            }
        }
    }
}

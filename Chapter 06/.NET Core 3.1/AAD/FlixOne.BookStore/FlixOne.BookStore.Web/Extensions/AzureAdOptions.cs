//Lets put AzureAdOptions in Authentication namespace
namespace Microsoft.AspNetCore.Authentication
{
    public class AzureAdOptions
    {
        //
        // Summary:
        //     Gets or sets the OpenID Connect authentication scheme to use for authentication
        //     with this instance of Azure Active Directory authentication.
        public string OpenIdConnectSchemeName { get; set; }
        //
        // Summary:
        //     Gets or sets the Cookie authentication scheme to use for sign in with this instance
        //     of Azure Active Directory authentication.
        public string CookieSchemeName { get; set; }
        //
        // Summary:
        //     Gets or sets the Jwt bearer authentication scheme to use for validating access
        //     tokens for this instance of Azure Active Directory Bearer authentication.
        public string JwtBearerSchemeName { get; }
        //
        // Summary:
        //     Gets or sets the client Id.
        public string ClientId { get; set; }
        //
        // Summary:
        //     Gets or sets the client secret.
        public string ClientSecret { get; set; }
        //
        // Summary:
        //     Gets or sets the tenant Id.
        public string TenantId { get; set; }
        //
        // Summary:
        //     Gets or sets the Azure Active Directory instance.
        public string Instance { get; set; }
        //
        // Summary:
        //     Gets or sets the domain of the Azure Active Directory tenant.
        public string Domain { get; set; }
        //
        // Summary:
        //     Gets or sets the sign in callback path.
        public string CallbackPath { get; set; }
        //
        // Summary:
        //     Gets or sets the sign out callback path.
        public string SignedOutCallbackPath { get; set; }
        //
        // Summary:
        //     Gets all the underlying authentication schemes.
        public string[] AllSchemes { get; }

        /// <summary>
        /// Authority delivering the token for your tenant
        /// </summary>
        public string Authority
        {
            get
            {
                return $"{Instance}{TenantId}";
            }
        }

        /// <summary>
        /// Instance of the settings for this Web application (to be used in controllers)
        /// </summary>
        public static AzureAdOptions Settings { set; get; }

        /// <summary>
        /// Client Id (Application ID) of the ProductListService, obtained from the Azure portal for that application
        /// </summary>
        public string ProductListResourceId { get; set; }

        /// <summary>
        /// Base URL of the ProductListService
        /// </summary>
        public string ProductListBaseAddress { get; set; }
    }
}


using $safeprojectname$.Ecommerce.Entities;
using System;
using System.Configuration;

namespace $safeprojectname$.Ecommerce.Authentication
{
    /// <summary>
    /// This class is a helper class providing properties and methods used by AuthenticationEcommerceManager
    /// for Consultix Ecommerce API connexion
    /// </summary>
    /// <seealso cref="AuthenticationEcommerceManager"/>
    public static class AuthenticationHelper
    {
        /// <value>Consultix admin username.</value>
        public static string Username => ConfigurationManager.AppSettings["CSX_eCommerce:adminUsername"];

        /// <value>Consultix admin password.</value>
        public static string Password => ConfigurationManager.AppSettings["CSX_eCommerce:adminPassword"];

        /// <value>Consultix Url from authorization (retrieving access token).</value>
        public static string AuthorizationUrl => string.Concat(ConfigurationManager.AppSettings["ConsultixAuthBaseUrl"],
                                            ConfigurationManager.AppSettings["ConsultixAuth:TokenRequestPath"]);

        /// <value>Grant type authorization requested.</value>
        public static string GrantType => ConfigurationManager.AppSettings["ConsultixAuth:GrantType"];

        /// <summary>
        /// This method validates if the parameter <paramref name="token" /> is valid and can be used.
        /// </summary>
        /// <returns>
        /// True if valid and false otherwise
        /// </returns>
        /// <param name="token"> A token class of type BearerToken to be validated</param>
        /// <seealso cref="BearerToken"/>
        public static bool IsValidToken(BearerToken token)
        {
            // Adding 2 seconds guard to ensure the request is done before the expiry time has reached
            return token != null &&
                   !string.IsNullOrEmpty(token.AccessToken) &&
                   token.ExpirationDate > DateTime.UtcNow.AddSeconds(2);
        }
    }
}

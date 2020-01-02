using Cake.Core;
using Cake.Core.Annotations;
using Microsoft.Rest.Azure.Authentication;

namespace Cake.AzureZ
{
    /// <summary>
    /// Contains Cake aliases for running Azure login commands.
    /// </summary>
    [CakeAliasCategory("Azure")]
    public static class AzureLoginAliases
    {
        /// <summary>
        /// Authenticates into Azure as a service principal with a password.
        /// </summary>
        /// <param name="context">The Cake context.</param>
        /// <param name="tenantId">The Active Directory tenant ID.</param>
        /// <param name="applicationId">The application ID.</param>
        /// <param name="password">The service principal password.</param>
        /// <returns>Azure credendials.</returns>
        [CakeAliasCategory("Login")]
        [CakeMethodAlias]
        public static Credentials AzureLogin(this ICakeContext context, string tenantId,
            string applicationId, string password)
        {
            return new Credentials(ApplicationTokenProvider.LoginSilentAsync(tenantId, applicationId, password)
                .GetAwaiter().GetResult());
        }

        /// <summary>
        /// Authenticates into Azure as a service principal with a certificate.
        /// </summary>
        /// <param name="context">The Cake context.</param>
        /// <param name="tenantId">The Active Directory tenant ID.</param>
        /// <param name="applicationId">The application ID.</param>
        /// <param name="certificate">The certificate content.</param>
        /// <param name="password">The certificate password.</param>
        /// <returns>Azure credendials.</returns>
        [CakeAliasCategory("Login")]
        [CakeMethodAlias]
        public static Credentials AzureLogin(this ICakeContext context, string tenantId,
            string applicationId, byte[] certificate, string password)
        {
            return new Credentials(ApplicationTokenProvider.LoginSilentAsync(tenantId, applicationId, certificate,
                password).GetAwaiter().GetResult());
        }
    }
}
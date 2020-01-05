using Microsoft.Rest.Azure.Authentication;

namespace Cake.AzureZ
{
    public static class AzureLoginService
    {
        public static Credentials AzureLogin(string tenantId, string applicationId, string password)
        {
            return new Credentials(ApplicationTokenProvider.LoginSilentAsync(tenantId, applicationId, password).GetAwaiter().GetResult());
        }

        public static Credentials AzureLogin(string tenantId, string applicationId, byte[] certificate, string password)
        {
            return new Credentials(ApplicationTokenProvider.LoginSilentAsync(tenantId, applicationId, certificate, password).GetAwaiter().GetResult());
        }
    }
}
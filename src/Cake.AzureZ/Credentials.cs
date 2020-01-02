using Microsoft.Rest;

namespace Cake.AzureZ
{
    public class Credentials
    {
        internal Credentials(ServiceClientCredentials credentials)
        {
            ServiceClientCredentials = credentials;
        }

        internal ServiceClientCredentials ServiceClientCredentials { get; }
    }
}
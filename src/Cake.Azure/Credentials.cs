using Microsoft.Rest;

namespace Cake.Azure
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
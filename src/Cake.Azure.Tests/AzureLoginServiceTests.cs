using NUnit.Framework;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Cake.Azure.Tests
{
    public class AzureLoginServiceTests
    {
        [Test]
        public void AzureLogin_ShouldAttemptToLogin_WhenAccessedCorrectlyWithoutACertificate()
        {
            Assert.Throws<AdalServiceException>(() => {
                AzureLoginService.AzureLogin("a", "b", "c");
            });
        }
    }
}
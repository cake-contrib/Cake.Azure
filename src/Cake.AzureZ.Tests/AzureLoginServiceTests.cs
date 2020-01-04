using Cake.AzureZ;
using NUnit.Framework;

namespace Cake.AzureZ.Tests
{
    public class AzureLoginServiceTests
    {
        [Test]
        public void AzureLogin_ShouldAttemptToLogin_WhenAccessedCorrectlyWithoutACertificate()
        {
            AzureLoginService.AzureLogin("a", "b", "c");
        }

        [Test]
        public void AzureLogin_ShouldAttemptToLogin_WhenAccessedCorrectlyWithACertificate()
        {
            AzureLoginService.AzureLogin("a", "b", new byte[0], "c");
        }
    }
}
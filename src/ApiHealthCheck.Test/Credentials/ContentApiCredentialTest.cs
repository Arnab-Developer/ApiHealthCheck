using ApiHealthCheck.Lib.Credentials;
using Xunit;

namespace ApiHealthCheck.Test.Credentials
{
    public class ContentApiCredentialTest
    {
        [Fact]
        public void ContentApiCredentialSuccessTest()
        {
            ProductApiCredential productApiCredential = new()
            {
                UserName = "u1",
                Password = "p1"
            };

            Assert.Equal("u1", productApiCredential.UserName);
            Assert.Equal("p1", productApiCredential.Password);
        }
    }
}

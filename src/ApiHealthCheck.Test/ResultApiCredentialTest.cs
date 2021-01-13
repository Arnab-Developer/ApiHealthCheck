using ApiHealthCheck.Lib.Credentials;
using Xunit;

namespace ApiHealthCheck.Test
{
    public class ResultApiCredentialTest
    {
        [Fact]
        public void ResultApiCredentialSuccessTest()
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

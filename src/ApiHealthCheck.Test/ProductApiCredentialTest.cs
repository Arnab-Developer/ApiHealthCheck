using ApiHealthCheck.Lib.Credentials;
using Xunit;

namespace ApiHealthCheck.Test
{
    public class ProductApiCredentialTest
    {
        [Fact]
        public void ProductApiCredentialSuccessTest()
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

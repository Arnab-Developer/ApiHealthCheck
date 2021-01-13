using ApiHealthCheck.Lib.Credentials;
using Xunit;

namespace ApiHealthCheck.Test
{
    public class TestApiCredentialTest
    {
        [Fact]
        public void TestApiCredentialSuccessTest()
        {
            TestApiCredential testApiCredential = new()
            {
                UserName = "u1",
                Password = "p1"
            };

            Assert.Equal("u1", testApiCredential.UserName);
            Assert.Equal("p1", testApiCredential.Password);
        }
    }
}

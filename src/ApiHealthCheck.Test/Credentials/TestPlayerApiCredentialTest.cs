using ApiHealthCheck.Lib.Credentials;
using Xunit;

namespace ApiHealthCheck.Test.Credentials
{
    public class TestPlayerApiCredentialTest
    {
        [Fact]
        public void TestPlayerApiCredentialSuccessTest()
        {
            TestPlayerApiCredential testPlayerApiCredential = new()
            {
                UserName = "u1",
                Password = "p1"
            };

            Assert.Equal("u1", testPlayerApiCredential.UserName);
            Assert.Equal("p1", testPlayerApiCredential.Password);
        }
    }
}

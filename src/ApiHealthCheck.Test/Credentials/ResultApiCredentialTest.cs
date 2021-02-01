using Xunit;

namespace ApiHealthCheck.Test.Credentials
{
    public class ResultApiCredentialTest
    {
        [Fact]
        public void ResultApiCredentialSuccessTest()
        {
            ResultApiCredential resultApiCredential = new()
            {
                UserName = "u1",
                Password = "p1"
            };

            Assert.Equal("u1", resultApiCredential.UserName);
            Assert.Equal("p1", resultApiCredential.Password);
        }
    }
}

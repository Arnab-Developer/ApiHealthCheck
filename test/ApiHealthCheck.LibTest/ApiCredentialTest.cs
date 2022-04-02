using ApiHealthCheck.Lib;
using Tynamix.ObjectFiller;
using Xunit;

namespace ApiHealthCheck.LibTest;

public class ApiCredentialTest
{
    [Fact]
    public void ProductApiCredentialSuccessTest()
    {
        string expectedUserName = Randomizer<string>.Create();
        string expectedPassword = Randomizer<string>.Create();

        ApiCredential apiCredential = new(expectedUserName, expectedPassword);

        string actualUserName = apiCredential.UserName;
        string actualPassword = apiCredential.Password;

        Assert.Equal("", actualUserName);
        Assert.Equal(expectedPassword, actualPassword);
    }
}

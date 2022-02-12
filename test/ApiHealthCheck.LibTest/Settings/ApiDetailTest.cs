using ApiHealthCheck.Lib;
using ApiHealthCheck.Lib.Settings;
using Tynamix.ObjectFiller;
using Xunit;

namespace ApiHealthCheck.LibTest.Settings;

public class ApiDetailTest
{
    [Fact]
    public void ApiDetailTrueTest()
    {
        string expectedName = Randomizer<string>.Create();
        string expectedUri = new RandomUri().GetValue().AbsoluteUri;
        ApiCredential expectedApiCredential = new(Randomizer<string>.Create(), Randomizer<string>.Create());
        bool expectedIsEnable = Randomizer<bool>.Create();

        ApiDetail apiDetail =
            new(expectedName, expectedUri, expectedApiCredential, expectedIsEnable);

        Assert.Equal(expectedName, apiDetail.Name);
        Assert.Equal(expectedUri, apiDetail.Url);
        Assert.Equal(expectedApiCredential.UserName, apiDetail.ApiCredential!.UserName);
        Assert.Equal(expectedApiCredential.Password, apiDetail.ApiCredential.Password);
        Assert.Equal(expectedIsEnable, apiDetail.IsEnable);
    }
}

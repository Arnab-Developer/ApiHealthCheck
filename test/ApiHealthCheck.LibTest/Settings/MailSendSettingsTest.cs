using ApiHealthCheck.Lib.Settings;
using Tynamix.ObjectFiller;
using Xunit;

namespace ApiHealthCheck.LibTest.Settings;

public class MailSendSettingsTest
{
    [Fact]
    public void MailSendSettingsSuccessTest()
    {
        bool expectedIsMailSendEnable = Randomizer<bool>.Create();
        MailSendSettings mailSendSettings = new(expectedIsMailSendEnable);
        Assert.Equal(expectedIsMailSendEnable, mailSendSettings.IsMailSendEnable);
    }
}

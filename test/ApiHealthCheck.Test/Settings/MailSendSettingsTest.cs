using ApiHealthCheck.Console.Settings;
using Xunit;

namespace ApiHealthCheck.Test.Settings
{
    public class MailSendSettingsTest
    {
        [Fact]
        public void MailSendSettingsSuccessTest()
        {
            MailSendSettings mailSendSettings = new(true);
            Assert.True(mailSendSettings.IsMailSendEnable);
        }

        [Fact]
        public void MailSendSettingsFailTest()
        {
            MailSendSettings mailSendSettings = new(false);
            Assert.False(mailSendSettings.IsMailSendEnable);
        }
    }
}

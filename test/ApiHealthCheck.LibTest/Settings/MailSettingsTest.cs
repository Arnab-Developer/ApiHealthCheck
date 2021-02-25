using ApiHealthCheck.Lib.Settings;
using Xunit;

namespace ApiHealthCheck.LibTest.Settings
{
    public class MailSettingsTest
    {
        [Fact]
        public void MailSettingsSuccessTest()
        {
            MailSettings mailSettings = new
            (
                "from email",
                "to email",
                "email sub",
                "email host",
                105,
                "smtp user",
                "smtp pwd",
                "true"
            );

            Assert.Equal("from email", mailSettings.From);
            Assert.Equal("to email", mailSettings.To);
            Assert.Equal("email sub", mailSettings.Subject);
            Assert.Equal("email host", mailSettings.Host);
            Assert.Equal(105, mailSettings.Port);
            Assert.Equal("smtp user", mailSettings.UserName);
            Assert.Equal("smtp pwd", mailSettings.Password);
            Assert.Equal("true", mailSettings.EnableSsl);
        }
    }
}

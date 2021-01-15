using ApiHealthCheck.Lib.Settings;
using Xunit;

namespace ApiHealthCheck.Test
{
    public class MailSettingsTest
    {
        [Fact]
        public void MailSettingsSuccessTest()
        {
            MailSettings mailSettings = new()
            {
                From = "from email",
                To = "to email",
                Subject = "email sub",
                Host = "email host",
                Port = 105,
                UserName = "smtp user",
                Password = "smtp pwd",
                EnableSsl = "true"
            };

            Assert.Equal("from email", mailSettings.From);
            Assert.Equal("to email", mailSettings.To);
            Assert.Equal("email sub", mailSettings.Subject);
            Assert.Equal("email host", mailSettings.Host);
            Assert.Equal(105, mailSettings.Port);
            Assert.Equal("smtp user", mailSettings.UserName);
            Assert.Equal("smtp pwd", mailSettings.Password);
            Assert.Equal("true", mailSettings.EnableSsl);
        }

        [Fact]
        public void MailSettingsFalseTest()
        {
            MailSettings mailSettings = new()
            {
                From = "from email",
                To = "to email",
                Subject = "email sub",
                Host = "email host",
                Port = 105,
                UserName = "smtp user",
                Password = "smtp pwd",
                EnableSsl = "true"
            };

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

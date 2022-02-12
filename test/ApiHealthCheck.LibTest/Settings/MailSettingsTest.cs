using ApiHealthCheck.Lib.Settings;
using Tynamix.ObjectFiller;
using Xunit;

namespace ApiHealthCheck.LibTest.Settings;

public class MailSettingsTest
{
    [Fact]
    public void MailSettingsSuccessTest()
    {
        string From = Randomizer<string>.Create();
        string To = Randomizer<string>.Create();
        string Subject = Randomizer<string>.Create();
        string Host = Randomizer<string>.Create();
        int Port = Randomizer<int>.Create();
        string UserName = Randomizer<string>.Create();
        string Password = Randomizer<string>.Create();
        string EnableSsl = Randomizer<bool>.Create().ToString();

        MailSettings mailSettings
            = new(From, To, Subject, Host, Port, UserName, Password, EnableSsl);

        Assert.Equal(From, mailSettings.From);
        Assert.Equal(To, mailSettings.To);
        Assert.Equal(Subject, mailSettings.Subject);
        Assert.Equal(Host, mailSettings.Host);
        Assert.Equal(Port, mailSettings.Port);
        Assert.Equal(UserName, mailSettings.UserName);
        Assert.Equal(Password, mailSettings.Password);
        Assert.Equal(EnableSsl, mailSettings.EnableSsl);
    }
}

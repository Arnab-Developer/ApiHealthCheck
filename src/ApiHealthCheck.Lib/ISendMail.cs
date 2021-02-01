using ApiHealthCheck.Lib.Settings;

namespace ApiHealthCheck.Lib
{
    public interface ISendMail
    {
        MailSettings MailSettings { get; }

        void SendMailToCustomer(string message);
    }
}

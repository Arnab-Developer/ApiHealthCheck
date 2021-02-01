using ApiHealthCheck.Lib.Settings;
using System.Net;
using System.Net.Mail;

namespace ApiHealthCheck.Lib
{
    public class SendMail : ISendMail
    {
        private readonly MailSettings _mailSettings;

        public SendMail(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        MailSettings ISendMail.MailSettings => _mailSettings;

        void ISendMail.SendMailToCustomer(string message)
        {
            SmtpClient smtpClient = new(_mailSettings.Host, _mailSettings.Port)
            {
                EnableSsl = _mailSettings.EnableSsl == "1",
                Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password)
            };
            foreach (string to in _mailSettings.To.Split(';'))
            {
                MailMessage mailMessage = new(_mailSettings.From, to, _mailSettings.Subject, message);
                smtpClient.Send(mailMessage);
            }
        }
    }
}

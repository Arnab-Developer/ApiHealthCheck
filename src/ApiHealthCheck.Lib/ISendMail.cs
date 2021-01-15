using ApiHealthCheck.Lib.Settings;

namespace ApiHealthCheck.Lib
{
    /// <summary>
    /// Interface to send mail.
    /// </summary>
    public interface ISendMail
    {
        /// <summary>
        /// Get the mail settings.
        /// </summary>
        MailSettings MailSettings { get; }

        /// <summary>
        /// Send mail.
        /// </summary>
        /// <param name="message">Mail message to send.</param>
        void SendMailToCustomer(string message);
    }
}

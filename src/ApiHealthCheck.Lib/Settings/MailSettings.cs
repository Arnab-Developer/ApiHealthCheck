namespace ApiHealthCheck.Lib.Settings
{
    /// <summary>
    /// Mail settings.
    /// </summary>
    public record MailSettings
    {
        /// <summary>
        /// From address.
        /// </summary>
        public string From { get; init; }

        /// <summary>
        /// To address.
        /// </summary>
        public string To { get; init; }

        /// <summary>
        /// Subject.
        /// </summary>
        public string Subject { get; init; }

        /// <summary>
        /// Mail host.
        /// </summary>
        public string Host { get; init; }

        /// <summary>
        /// Mail port.
        /// </summary>
        public int Port { get; init; }

        /// <summary>
        /// SMTP user name.
        /// </summary>
        public string UserName { get; init; }

        /// <summary>
        /// SMTP password.
        /// </summary>
        public string Password { get; init; }

        /// <summary>
        /// Enable ssl or not.
        /// </summary>
        public string EnableSsl { get; init; }

        /// <summary>
        /// Creates a new object of MailSettings class.
        /// </summary>
        public MailSettings()
        {
            From = string.Empty;
            To = string.Empty;
            Subject = string.Empty;
            Host = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
            EnableSsl = string.Empty;
        }
    }
}
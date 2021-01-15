namespace ApiHealthCheck.Lib.Settings
{
    public record MailSettings
    {
        public string From { get; init; }
        public string To { get; init; }
        public string Subject { get; init; }
        public string Host { get; init; }
        public int Port { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
        public string EnableSsl { get; init; }

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
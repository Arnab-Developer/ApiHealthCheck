namespace ApiHealthCheck.Lib.Settings
{
    public record MailSettings
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EnableSsl { get; set; }

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
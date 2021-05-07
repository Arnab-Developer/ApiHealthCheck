namespace ApiHealthCheck.Lib.Settings
{
    public record MailSendSettings
    {
        public bool IsMailSendEnable { get; init; }

        public MailSendSettings()
        {
            IsMailSendEnable = false;
        }

        public MailSendSettings(bool isMailSendEnable)
        {
            IsMailSendEnable = isMailSendEnable;
        }
    }
}

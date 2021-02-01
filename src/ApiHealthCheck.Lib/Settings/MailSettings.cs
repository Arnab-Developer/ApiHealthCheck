namespace ApiHealthCheck.Lib.Settings
{
    public record MailSettings
    (
        string From,
        string To,
        string Subject,
        string Host,
        int Port,
        string UserName,
        string Password,
        string EnableSsl
    );
}
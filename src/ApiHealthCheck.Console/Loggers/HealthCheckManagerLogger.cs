namespace ApiHealthCheck.Console.Loggers
{
    internal static partial class HealthCheckManagerLogger
    {
        [LoggerMessage(
            EventId = 1,
            Level = LogLevel.Debug,
            Message = "{apiName} health check result {action}")]
        public static partial void HealthCheckResultAction(
            this ILogger<HealthCheckManager> logger, string apiName, string action);

        [LoggerMessage(
            EventId = 2,
            Level = LogLevel.Error,
            Message = "Health check error in {apiName}")]
        public static partial void HealthCheckError(
            this ILogger<HealthCheckManager> logger, string apiName, Exception ex);

        [LoggerMessage(
            EventId = 3,
            Level = LogLevel.Information,
            Message = "{apiStatusMessage}")]
        public static partial void ApiStatusMessage(
            this ILogger<HealthCheckManager> logger, string apiStatusMessage);

        [LoggerMessage(
            EventId = 4,
            Level = LogLevel.Error,
            Message = "Mail sending error")]
        public static partial void MailSendingError(
            this ILogger<HealthCheckManager> logger, Exception ex);
    }
}

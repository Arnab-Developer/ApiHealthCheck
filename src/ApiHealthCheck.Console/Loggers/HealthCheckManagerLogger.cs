using Microsoft.Extensions.Logging;
using System;

namespace ApiHealthCheck.Console.Loggers
{
    internal static class HealthCheckManagerLogger
    {
        private static readonly Action<ILogger, string, string, Exception> _healthCheckResultAction = LoggerMessage.Define<string, string>(
            LogLevel.Debug,
            new EventId(1, nameof(HealthCheckResultStart)),
            "{apiName} health check result {action}");

        private static readonly Action<ILogger, string, Exception> _healthCheckError = LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(1, nameof(HealthCheckError)),
            "Health check error in {apiName}.");

        private static readonly Action<ILogger, string, Exception> _apiStatusMessage = LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, nameof(ApiStatusMessage)),
            "{apiStatusMessage}.");

        private static readonly Action<ILogger, Exception> _mailSendingError = LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1, nameof(MailSendingError)),
            "Mail sending error.");

        public static void HealthCheckResultStart(this ILogger<HealthCheckManager> logger, string apiName, string action)
        {
            _healthCheckResultAction(logger, apiName, action, null);
        }

        public static void HealthCheckError(this ILogger<HealthCheckManager> logger, string apiName, Exception ex)
        {
            _healthCheckError(logger, apiName, ex);
        }

        public static void ApiStatusMessage(this ILogger<HealthCheckManager> logger, string apiStatusMessage)
        {
            _apiStatusMessage(logger, apiStatusMessage, null);
        }

        public static void MailSendingError(this ILogger<HealthCheckManager> logger, Exception ex)
        {
            _mailSendingError(logger, ex);
        }
    }
}

using Microsoft.Extensions.Logging;
using System;

namespace ApiHealthCheck.Console.Loggers
{
    internal static class HealthCheckServiceLogger
    {
        private static readonly Action<ILogger, Exception> _healthCheckError = LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1, nameof(HealthCheckError)),
            "Health check error:");

        public static void HealthCheckError(this ILogger<HealthCheckService> logger, Exception ex)
        {
            _healthCheckError(logger, ex);
        }
    }
}

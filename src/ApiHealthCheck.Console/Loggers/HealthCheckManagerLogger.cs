using Microsoft.Extensions.Logging;
using System;

namespace ApiHealthCheck.Console.Loggers
{
    internal static class HealthCheckManagerLogger
    {
        private static readonly Action<ILogger, Exception> _productHealthCheckResultStart = LoggerMessage.Define(
            LogLevel.Debug,
            new EventId(1, nameof(ProductHealthCheckResultStart)),
            "Product health check result start");

        private static readonly Action<ILogger, Exception> _productHealthCheckResultEnd = LoggerMessage.Define(
            LogLevel.Debug,
            new EventId(1, nameof(ProductHealthCheckResultEnd)),
            "Product health check result end");

        private static readonly Action<ILogger, string, Exception> _healthCheckError = LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(1, nameof(HealthCheckError)),
            "Health check error in {apiName}");

        private static readonly Action<ILogger, string, Exception> _apiStatusMessage = LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, nameof(ApiStatusMessage)),
            "Api status message {apiStatusMessage}");

        public static void ProductHealthCheckResultStart(this ILogger<HealthCheckManager> logger)
        {
            _productHealthCheckResultStart(logger, null);
        }

        public static void ProductHealthCheckResultEnd(this ILogger<HealthCheckManager> logger)
        {
            _productHealthCheckResultEnd(logger, null);
        }

        public static void HealthCheckError(this ILogger<HealthCheckManager> logger, string apiName, Exception ex)
        {
            _healthCheckError(logger, apiName, ex);
        }

        public static void ApiStatusMessage(this ILogger<HealthCheckManager> logger, string apiStatusMessage)
        {
            _apiStatusMessage(logger, apiStatusMessage, null);
        }
    }
}

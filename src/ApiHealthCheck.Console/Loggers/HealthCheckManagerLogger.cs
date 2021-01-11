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

        public static void ProductHealthCheckResultStart(this ILogger<HealthCheckManager> logger)
        {
            _productHealthCheckResultStart(logger, null);
        }

        public static void ProductHealthCheckResultEnd(this ILogger<HealthCheckManager> logger)
        {
            _productHealthCheckResultEnd(logger, null);
        }
    }
}

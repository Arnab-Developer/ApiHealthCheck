using ApiHealthCheck.Console.Loggers;
using ApiHealthCheck.Console.Settings;
using ApiHealthCheck.Lib;
using ApiHealthCheck.Lib.Credentials;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiHealthCheck.Console
{
    internal class HealthCheckManager : IHealthCheckManager
    {
        private readonly IHealthCheck _healthCheck;
        private readonly ILogger<HealthCheckManager> _logger;
        private readonly Urls _urls;
        private readonly ProductApiCredential _credential;

        public HealthCheckManager(
            IHealthCheck healthCheck,
            IOptionsMonitor<Urls> urlsOptionsMonitor,
            IOptionsMonitor<ProductApiCredential> credentialOptionsMonitor,
            ILogger<HealthCheckManager> logger)
        {
            _healthCheck = healthCheck;
            _logger = logger;
            _urls = urlsOptionsMonitor.CurrentValue;
            _credential = credentialOptionsMonitor.CurrentValue;
        }

        Urls IHealthCheckManager.Urls => _urls;

        ProductApiCredential IHealthCheckManager.Credential => _credential;

        void IHealthCheckManager.PrintHealthCheckResult()
        {
            string productApiStatusMessage = GetProductApiStatusMessage();
            System.Console.WriteLine(productApiStatusMessage);
        }

        private string GetProductApiStatusMessage()
        {
            _logger.ProductHealthCheckResultStart();
            bool isProductApiHealthy = _healthCheck.IsApiHealthy(_urls.ProductApiUrl, _credential);
            string productApiStatusMessage = isProductApiHealthy ? "OK" : "Error";
            _logger.ProductHealthCheckResultEnd();
            return $"Product api status is: {productApiStatusMessage}";
        }
    }
}

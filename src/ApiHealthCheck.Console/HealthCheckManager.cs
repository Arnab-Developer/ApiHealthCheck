using ApiHealthCheck.Console.Loggers;
using ApiHealthCheck.Console.Settings;
using ApiHealthCheck.Lib;
using ApiHealthCheck.Lib.Credentials;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;

namespace ApiHealthCheck.Console
{
    internal class HealthCheckManager : IHealthCheckManager
    {
        private readonly IHealthCheck _healthCheck;
        private readonly ISendMail _sendMail;
        private readonly ILogger<HealthCheckManager> _logger;
        private readonly Urls _urls;
        private readonly ProductApiCredential _productApiCredential;
        private readonly ResultApiCredential _resultApiCredential;

        public HealthCheckManager(
            IHealthCheck healthCheck,
            ISendMail sendMail,
            IOptionsMonitor<Urls> urlsOptionsMonitor,
            IOptionsMonitor<ProductApiCredential> productApiCredentialOptionsMonitor,
            IOptionsMonitor<ResultApiCredential> resultApiCredentialOptionsMonitor,
            ILogger<HealthCheckManager> logger)
        {
            _healthCheck = healthCheck;
            _sendMail = sendMail;
            _logger = logger;
            _urls = urlsOptionsMonitor.CurrentValue;
            _productApiCredential = productApiCredentialOptionsMonitor.CurrentValue;
            _resultApiCredential = resultApiCredentialOptionsMonitor.CurrentValue;
        }

        Urls IHealthCheckManager.Urls => _urls;

        ProductApiCredential IHealthCheckManager.Credential => _productApiCredential;

        string IHealthCheckManager.LogHealthCheckResult()
        {
            StringBuilder apiStatusMessage = new(string.Empty);

            apiStatusMessage.Append(GetProductApiStatusMessage());
            apiStatusMessage.Append('\n');

            apiStatusMessage.Append(GetResultApiStatusMessage());
            apiStatusMessage.Append('\n');

            _logger.ApiStatusMessage(apiStatusMessage.ToString());
            try
            {
                _sendMail.SendMailToCustomer(apiStatusMessage.ToString());
            }
            catch (Exception ex)
            {
                _logger.MailSendingError(ex);
            }

            return apiStatusMessage.ToString();
        }

        private string GetProductApiStatusMessage()
        {
            _logger.HealthCheckResultStart("Product api", "start");
            try
            {
                bool isProductApiHealthy = _healthCheck.IsApiHealthy(_urls.ProductApiUrl, _productApiCredential);
                string productApiStatusMessage = isProductApiHealthy ? "OK" : "Error";
                _logger.HealthCheckResultStart("Product api", "end");
                return $"Product api status is: {productApiStatusMessage}";
            }
            catch (Exception ex)
            {
                _logger.HealthCheckError("product api", ex);
                return $"Product api status is: Error";
            }
        }

        private string GetResultApiStatusMessage()
        {
            _logger.HealthCheckResultStart("Result api", "start");
            try
            {
                bool isProductApiHealthy = _healthCheck.IsApiHealthy(_urls.ResultApiUrl, _resultApiCredential);
                string productApiStatusMessage = isProductApiHealthy ? "OK" : "Error";
                _logger.HealthCheckResultStart("Result api", "end");
                return $"Result api status is: {productApiStatusMessage}";
            }
            catch (Exception ex)
            {
                _logger.HealthCheckError("result api", ex);
                return $"Result api status is: Error";
            }
        }
    }
}

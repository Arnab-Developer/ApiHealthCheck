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
        private readonly ContentApiCredential _contentApiCredential;
        private readonly TestApiCredential _testApiCredential;
        private readonly TestPlayerApiCredential _testPlayerApiCredential;
        private readonly MailSendSettings _mailSendSettings;
        private readonly UrlsIsEnable _urlsEnable;

        public HealthCheckManager(
            IHealthCheck healthCheck,
            ISendMail sendMail,
            IOptionsMonitor<Urls> urlsOptionsMonitor,
            IOptionsMonitor<ProductApiCredential> productApiCredentialOptionsMonitor,
            IOptionsMonitor<ResultApiCredential> resultApiCredentialOptionsMonitor,
            IOptionsMonitor<ContentApiCredential> contentApiCredentialOptionsMonitor,
            IOptionsMonitor<TestApiCredential> testApiCredentialOptionsMonitor,
            IOptionsMonitor<TestPlayerApiCredential> testPlayerApiCredentialOptionsMonitor,
            IOptionsMonitor<MailSendSettings> mailSendSettingsOptionsMonitor,
            IOptionsMonitor<UrlsIsEnable> urlsEnableOptionsMonitor,
            ILogger<HealthCheckManager> logger)
        {
            _healthCheck = healthCheck;
            _sendMail = sendMail;
            _logger = logger;
            _urls = urlsOptionsMonitor.CurrentValue;
            _productApiCredential = productApiCredentialOptionsMonitor.CurrentValue;
            _resultApiCredential = resultApiCredentialOptionsMonitor.CurrentValue;
            _contentApiCredential = contentApiCredentialOptionsMonitor.CurrentValue;
            _testApiCredential = testApiCredentialOptionsMonitor.CurrentValue;
            _testPlayerApiCredential = testPlayerApiCredentialOptionsMonitor.CurrentValue;
            _mailSendSettings = mailSendSettingsOptionsMonitor.CurrentValue;
            _urlsEnable = urlsEnableOptionsMonitor.CurrentValue;
        }

        Urls IHealthCheckManager.Urls => _urls;

        ProductApiCredential IHealthCheckManager.ProductApiCredential => _productApiCredential;

        ResultApiCredential IHealthCheckManager.ResultApiCredential => _resultApiCredential;

        ContentApiCredential IHealthCheckManager.ContentApiCredential => _contentApiCredential;

        TestApiCredential IHealthCheckManager.TestApiCredential => _testApiCredential;

        TestPlayerApiCredential IHealthCheckManager.TestPlayerApiCredential => _testPlayerApiCredential;

        string IHealthCheckManager.LogHealthCheckResult()
        {
            StringBuilder apiStatusMessage = new(string.Empty);

            if (_urlsEnable.IsCheckProductApi)
            {
                apiStatusMessage.Append(GetProductApiStatusMessage());
                apiStatusMessage.Append('\n');
            }

            if (_urlsEnable.IsCheckResultApi)
            {
                apiStatusMessage.Append(GetResultApiStatusMessage());
                apiStatusMessage.Append('\n');
            }

            if (_urlsEnable.IsCheckContentApi)
            {
                apiStatusMessage.Append(GetContentApiStatusMessage());
                apiStatusMessage.Append('\n');
            }

            if (_urlsEnable.IsCheckTestApi)
            {
                apiStatusMessage.Append(GetTestApiStatusMessage());
                apiStatusMessage.Append('\n');
            }

            if (_urlsEnable.IsCheckTestPlayerApi)
            {
                apiStatusMessage.Append(GetTestPlayerApiStatusMessage());
                apiStatusMessage.Append('\n');
            }

            if (apiStatusMessage.ToString() != string.Empty)
            {
                _logger.ApiStatusMessage(apiStatusMessage.ToString());

                if (_mailSendSettings.IsMailSendEnable)
                {
                    try
                    {
                        _sendMail.SendMailToCustomer(apiStatusMessage.ToString());
                    }
                    catch (Exception ex)
                    {
                        _logger.MailSendingError(ex);
                    }
                }
            }

            return apiStatusMessage.ToString();
        }

        private string GetProductApiStatusMessage()
        {
            _logger.HealthCheckResultAction("Product api", "start");
            try
            {
                bool isProductApiHealthy = _healthCheck.IsApiHealthy(_urls.ProductApiUrl, _productApiCredential);
                string productApiStatusMessage = isProductApiHealthy ? "OK" : "Error";
                _logger.HealthCheckResultAction("Product api", "end");
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
            _logger.HealthCheckResultAction("Result api", "start");
            try
            {
                bool isProductApiHealthy = _healthCheck.IsApiHealthy(_urls.ResultApiUrl, _resultApiCredential);
                string resultApiStatusMessage = isProductApiHealthy ? "OK" : "Error";
                _logger.HealthCheckResultAction("Result api", "end");
                return $"Result api status is: {resultApiStatusMessage}";
            }
            catch (Exception ex)
            {
                _logger.HealthCheckError("result api", ex);
                return $"Result api status is: Error";
            }
        }

        private string GetContentApiStatusMessage()
        {
            _logger.HealthCheckResultAction("Content api", "start");
            try
            {
                bool isContentApiHealthy = _healthCheck.IsApiHealthy(_urls.ContentApiUrl, _contentApiCredential);
                string contentApiStatusMessage = isContentApiHealthy ? "OK" : "Error";
                _logger.HealthCheckResultAction("Content api", "end");
                return $"Content api status is: {contentApiStatusMessage}";
            }
            catch (Exception ex)
            {
                _logger.HealthCheckError("content api", ex);
                return $"Content api status is: Error";
            }
        }

        private string GetTestApiStatusMessage()
        {
            _logger.HealthCheckResultAction("Test api", "start");
            try
            {
                bool isTestApiHealthy = _healthCheck.IsApiHealthy(_urls.TestApiUrl, _testApiCredential);
                string testApiStatusMessage = isTestApiHealthy ? "OK" : "Error";
                _logger.HealthCheckResultAction("test api", "end");
                return $"Test api status is: {testApiStatusMessage}";
            }
            catch (Exception ex)
            {
                _logger.HealthCheckError("test api", ex);
                return $"Test api status is: Error";
            }
        }

        private string GetTestPlayerApiStatusMessage()
        {
            _logger.HealthCheckResultAction("Test player api", "start");
            try
            {
                bool isTestPlayerApiHealthy = _healthCheck.IsApiHealthy(_urls.TestPlayerApiUrl, _testPlayerApiCredential);
                string testPlayerApiStatusMessage = isTestPlayerApiHealthy ? "OK" : "Error";
                _logger.HealthCheckResultAction("test player api", "end");
                return $"Test player api status is: {testPlayerApiStatusMessage}";
            }
            catch (Exception ex)
            {
                _logger.HealthCheckError("test player api", ex);
                return $"Test player api status is: Error";
            }
        }
    }
}

using ApiHealthCheck.Console.Loggers;
using ApiHealthCheck.Console.Settings;
using ApiHealthCheck.Lib;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiHealthCheck.Console
{
    internal class HealthCheckManager : IHealthCheckManager
    {
        private readonly IHealthCheck _healthCheck;
        private readonly ISendMail _sendMail;
        private readonly IEnumerable<ApiDetail> _urlDetails;
        private readonly ILogger<HealthCheckManager> _logger;
        private readonly MailSendSettings _mailSendSettings;

        public HealthCheckManager(
            IHealthCheck healthCheck,
            ISendMail sendMail,
            IOptionsMonitor<MailSendSettings> mailSendSettingsOptionsMonitor,
            IEnumerable<ApiDetail> urlDetails,
            ILogger<HealthCheckManager> logger)
        {
            _healthCheck = healthCheck;
            _sendMail = sendMail;
            _urlDetails = urlDetails;
            _mailSendSettings = mailSendSettingsOptionsMonitor.CurrentValue;
            _logger = logger;
        }

        IEnumerable<ApiDetail> IHealthCheckManager.ApiDetails => _urlDetails;

        string IHealthCheckManager.LogHealthCheckResult()
        {
            StringBuilder apiStatusMessages = new(string.Empty);
            foreach (ApiDetail urlDetail in _urlDetails)
            {
                if (urlDetail.IsEnable)
                {
                    apiStatusMessages.Append(GetApiStatusMessage(urlDetail));
                    apiStatusMessages.Append('\n');
                }
            }
            if (apiStatusMessages.ToString() != string.Empty)
            {
                _logger.ApiStatusMessage(apiStatusMessages.ToString());

                if (_mailSendSettings.IsMailSendEnable)
                {
                    try
                    {
                        _sendMail.SendMailToCustomer(apiStatusMessages.ToString());
                    }
                    catch (Exception ex)
                    {
                        _logger.MailSendingError(ex);
                    }
                }
            }
            return apiStatusMessages.ToString();
        }

        private string GetApiStatusMessage(ApiDetail apiDetail)
        {
            _logger.HealthCheckResultAction(apiDetail.Name, "start");
            try
            {
                bool isApiHealthy = false;
                if (apiDetail.ApiCredential is null ||
                    string.IsNullOrWhiteSpace(apiDetail.ApiCredential.UserName) ||
                    string.IsNullOrWhiteSpace(apiDetail.ApiCredential.Password))
                {
                    isApiHealthy = _healthCheck.IsApiHealthy(apiDetail.Url);
                }
                else
                {
                    isApiHealthy = _healthCheck.IsApiHealthy(apiDetail.Url, apiDetail.ApiCredential);
                }
                string apiStatusMessage = isApiHealthy ? "OK" : "Error";
                _logger.HealthCheckResultAction(apiDetail.Name, "end");
                return $"{apiDetail.Name} status is: {apiStatusMessage}";
            }
            catch (Exception ex)
            {
                _logger.HealthCheckError(apiDetail.Name, ex);
                return $"{apiDetail.Name} status is: Error";
            }
        }
    }
}

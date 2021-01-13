using ApiHealthCheck.Console;
using ApiHealthCheck.Console.Settings;
using ApiHealthCheck.Lib;
using ApiHealthCheck.Lib.Credentials;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using Xunit;

namespace ApiHealthCheck.Test
{
    public class HealthCheckManagerTest
    {
        private readonly Mock<IHealthCheck> _healthCheckMock;
        private readonly Mock<ISendMail> _sendMailMock;
        private readonly Mock<IOptionsMonitor<Urls>> _urlsOptionMonitorMock;
        private readonly Mock<IOptionsMonitor<ProductApiCredential>> _productApiCredentialOptionMonitorMock;
        private readonly Mock<IOptionsMonitor<ResultApiCredential>> _resultApiCredentialOptionMonitorMock;
        private readonly Mock<ILogger<HealthCheckManager>> _healthCheckManagerLoggerMock;
        private IHealthCheckManager? _healthCheckManager;

        public HealthCheckManagerTest()
        {
            _healthCheckMock = new Mock<IHealthCheck>();
            _sendMailMock = new Mock<ISendMail>();
            _urlsOptionMonitorMock = new Mock<IOptionsMonitor<Urls>>();
            _productApiCredentialOptionMonitorMock = new Mock<IOptionsMonitor<ProductApiCredential>>();
            _resultApiCredentialOptionMonitorMock = new Mock<IOptionsMonitor<ResultApiCredential>>();
            _healthCheckManagerLoggerMock = new Mock<ILogger<HealthCheckManager>>();
        }

        [Fact]
        public void LogHealthCheckResultSuccessTest()
        {
            ProductApiCredential productApiCredential = new() { UserName = "jon", Password = "pass" };
            ResultApiCredential resultApiCredential = new() { UserName = "jon", Password = "pass" };

            _urlsOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(new Urls { ProductApiUrl = "pr url", ResultApiUrl = "rs url" });

            _productApiCredentialOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(productApiCredential);

            _resultApiCredentialOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(resultApiCredential);

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), productApiCredential))
                .Returns(true)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), resultApiCredential))
                .Returns(true)
                .Verifiable();

            _sendMailMock
                .Setup(s => s.SendMailToCustomer(It.IsAny<string>()))
                .Verifiable();

            _healthCheckManager = new HealthCheckManager(
                _healthCheckMock.Object,
                _sendMailMock.Object,
                _urlsOptionMonitorMock.Object,
                _productApiCredentialOptionMonitorMock.Object,
                _resultApiCredentialOptionMonitorMock.Object,
                _healthCheckManagerLoggerMock.Object);

            string apiStatusMessage = _healthCheckManager.LogHealthCheckResult();

            _healthCheckMock.Verify();
            _sendMailMock.Verify();
            Assert.Equal("Product api status is: OK\nResult api status is: OK\n", apiStatusMessage);
        }

        [Fact]
        public void LogHealthCheckResultFailedTest()
        {
            ProductApiCredential productApiCredential = new() { UserName = "jon", Password = "pass" };
            ResultApiCredential resultApiCredential = new() { UserName = "jon", Password = "pass" };

            _urlsOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(new Urls { ProductApiUrl = "pr url", ResultApiUrl = "rs url" });

            _productApiCredentialOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(productApiCredential);

            _resultApiCredentialOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(resultApiCredential);

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), productApiCredential))
                .Throws<ArgumentException>()
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), resultApiCredential))
                .Returns(false)
                .Verifiable();

            _sendMailMock
                .Setup(s => s.SendMailToCustomer(It.IsAny<string>()))
                .Verifiable();

            _healthCheckManager = new HealthCheckManager(
                _healthCheckMock.Object,
                _sendMailMock.Object,
                _urlsOptionMonitorMock.Object,
                _productApiCredentialOptionMonitorMock.Object,
                _resultApiCredentialOptionMonitorMock.Object,
                _healthCheckManagerLoggerMock.Object);

            string apiStatusMessage = _healthCheckManager.LogHealthCheckResult();

            _healthCheckMock.Verify();
            _sendMailMock.Verify();
            Assert.Equal("Product api status is: Error\nResult api status is: Error\n", apiStatusMessage);
        }

        [Fact]
        public void LogHealthCheckResultSuccessFailedTest()
        {
            ProductApiCredential productApiCredential = new() { UserName = "jon", Password = "pass" };
            ResultApiCredential resultApiCredential = new() { UserName = "jon", Password = "pass" };

            _urlsOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(new Urls { ProductApiUrl = "pr url", ResultApiUrl = "rs url" });

            _productApiCredentialOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(productApiCredential);

            _resultApiCredentialOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(resultApiCredential);

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), productApiCredential))
                .Returns(true)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), resultApiCredential))
                .Returns(false)
                .Verifiable();

            _sendMailMock
                .Setup(s => s.SendMailToCustomer(It.IsAny<string>()))
                .Verifiable();

            _healthCheckManager = new HealthCheckManager(
                _healthCheckMock.Object,
                _sendMailMock.Object,
                _urlsOptionMonitorMock.Object,
                _productApiCredentialOptionMonitorMock.Object,
                _resultApiCredentialOptionMonitorMock.Object,
                _healthCheckManagerLoggerMock.Object);

            string apiStatusMessage = _healthCheckManager.LogHealthCheckResult();

            _healthCheckMock.Verify();
            _sendMailMock.Verify();
            Assert.Equal("Product api status is: OK\nResult api status is: Error\n", apiStatusMessage);
        }
    }
}

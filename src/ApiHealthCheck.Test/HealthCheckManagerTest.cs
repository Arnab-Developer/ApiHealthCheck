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
        private readonly Mock<ILogger<HealthCheckManager>> _healthCheckManagerLoggerMock;

        private readonly Mock<IOptionsMonitor<Urls>> _urlsOptionMonitorMock;
        private readonly Mock<IOptionsMonitor<ProductApiCredential>> _productApiCredentialOptionMonitorMock;
        private readonly Mock<IOptionsMonitor<ResultApiCredential>> _resultApiCredentialOptionMonitorMock;
        private readonly Mock<IOptionsMonitor<ContentApiCredential>> _contentApiCredentialOptionMonitorMock;
        private readonly Mock<IOptionsMonitor<TestApiCredential>> _testApiCredentialOptionMonitorMock;
        private readonly Mock<IOptionsMonitor<TestPlayerApiCredential>> _testPlayerApiCredentialOptionMonitorMock;
        private readonly Mock<IOptionsMonitor<MailSendSettings>> _mailSendSettingsOptionsMonitor;

        private readonly ProductApiCredential _productApiCredential;
        private readonly ResultApiCredential _resultApiCredential;
        private readonly ContentApiCredential _contentApiCredential;
        private readonly TestApiCredential _testApiCredential;
        private readonly TestPlayerApiCredential _testPlayerApiCredential;
        private readonly MailSendSettings _mailSendSettings;

        private IHealthCheckManager? _healthCheckManager;

        public HealthCheckManagerTest()
        {
            _healthCheckMock = new Mock<IHealthCheck>();
            _sendMailMock = new Mock<ISendMail>();
            _healthCheckManagerLoggerMock = new Mock<ILogger<HealthCheckManager>>();

            _urlsOptionMonitorMock = new Mock<IOptionsMonitor<Urls>>();
            _productApiCredentialOptionMonitorMock = new Mock<IOptionsMonitor<ProductApiCredential>>();
            _resultApiCredentialOptionMonitorMock = new Mock<IOptionsMonitor<ResultApiCredential>>();
            _contentApiCredentialOptionMonitorMock = new Mock<IOptionsMonitor<ContentApiCredential>>();
            _testApiCredentialOptionMonitorMock = new Mock<IOptionsMonitor<TestApiCredential>>();
            _testPlayerApiCredentialOptionMonitorMock = new Mock<IOptionsMonitor<TestPlayerApiCredential>>();
            _mailSendSettingsOptionsMonitor = new Mock<IOptionsMonitor<MailSendSettings>>();

            _productApiCredential = new() { UserName = "jon", Password = "pass" };
            _resultApiCredential = new() { UserName = "jon", Password = "pass" };
            _contentApiCredential = new() { UserName = "jon", Password = "pass" };
            _testApiCredential = new() { UserName = "jon", Password = "pass" };
            _testPlayerApiCredential = new() { UserName = "jon", Password = "pass" };
            _mailSendSettings = new MailSendSettings() { IsMailSendEnable = true };
        }

        [Fact]
        public void LogHealthCheckResultSuccessTest()
        {
            Setup();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _productApiCredential))
                .Returns(true)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _resultApiCredential))
                .Returns(true)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _contentApiCredential))
                .Returns(true)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _testApiCredential))
                .Returns(true)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _testPlayerApiCredential))
                .Returns(true)
                .Verifiable();

            CreateHealthCheckManager();

            string apiStatusMessage = _healthCheckManager!.LogHealthCheckResult();

            _healthCheckMock.Verify();
            _sendMailMock.Verify();
            Assert.Equal(
                "Product api status is: OK\nResult api status is: OK\nContent api status is: OK\nTest api status is: OK\nTest player api status is: OK\n",
                apiStatusMessage);
        }

        [Fact]
        public void LogHealthCheckResultFailedTest()
        {
            Setup();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _productApiCredential))
                .Throws<ArgumentException>()
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _resultApiCredential))
                .Returns(false)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _contentApiCredential))
                .Returns(false)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _testApiCredential))
                .Returns(false)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _testPlayerApiCredential))
                .Returns(false)
                .Verifiable();

            CreateHealthCheckManager();

            string apiStatusMessage = _healthCheckManager!.LogHealthCheckResult();

            _healthCheckMock.Verify();
            _sendMailMock.Verify();
            Assert.Equal(
                "Product api status is: Error\nResult api status is: Error\nContent api status is: Error\nTest api status is: Error\nTest player api status is: Error\n",
                apiStatusMessage);
        }

        [Fact]
        public void LogHealthCheckResultSuccessFailedTest()
        {
            Setup();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _productApiCredential))
                .Returns(true)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _resultApiCredential))
                .Returns(false)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _contentApiCredential))
                .Returns(true)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _testApiCredential))
                .Returns(false)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _testPlayerApiCredential))
                .Returns(true)
                .Verifiable();

            CreateHealthCheckManager();

            string apiStatusMessage = _healthCheckManager!.LogHealthCheckResult();

            _healthCheckMock.Verify();
            _sendMailMock.Verify();
            Assert.Equal(
                "Product api status is: OK\nResult api status is: Error\nContent api status is: OK\nTest api status is: Error\nTest player api status is: OK\n",
                apiStatusMessage);
        }

        [Fact]
        public void LogHealthCheckResultEmailFailedTest()
        {
            Setup();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _productApiCredential))
                .Returns(true)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _resultApiCredential))
                .Returns(false)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _contentApiCredential))
                .Returns(true)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _testApiCredential))
                .Returns(false)
                .Verifiable();

            _healthCheckMock
                .Setup(s => s.IsApiHealthy(It.IsAny<string>(), _testPlayerApiCredential))
                .Returns(true)
                .Verifiable();

            _mailSendSettings.IsMailSendEnable = false;

            CreateHealthCheckManager();

            string apiStatusMessage = _healthCheckManager!.LogHealthCheckResult();

            _healthCheckMock.Verify();
            _sendMailMock.VerifyNoOtherCalls();
            Assert.Equal(
                "Product api status is: OK\nResult api status is: Error\nContent api status is: OK\nTest api status is: Error\nTest player api status is: OK\n",
                apiStatusMessage);
        }

        private void Setup()
        {
            _urlsOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(new Urls
                {
                    ProductApiUrl = "pr url",
                    ResultApiUrl = "rs url",
                    ContentApiUrl = "con url",
                    TestApiUrl = "test url"
                });

            _productApiCredentialOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(_productApiCredential);

            _resultApiCredentialOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(_resultApiCredential);

            _contentApiCredentialOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(_contentApiCredential);

            _testApiCredentialOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(_testApiCredential);

            _testPlayerApiCredentialOptionMonitorMock
                .Setup(s => s.CurrentValue)
                .Returns(_testPlayerApiCredential);

            _mailSendSettingsOptionsMonitor
                .Setup(s => s.CurrentValue)
                .Returns(_mailSendSettings);

            _sendMailMock
                .Setup(s => s.SendMailToCustomer(It.IsAny<string>()))
                .Verifiable();
        }

        private void CreateHealthCheckManager()
        {
            _healthCheckManager = new HealthCheckManager(
                _healthCheckMock.Object,
                _sendMailMock.Object,
                _urlsOptionMonitorMock.Object,
                _productApiCredentialOptionMonitorMock.Object,
                _resultApiCredentialOptionMonitorMock.Object,
                _contentApiCredentialOptionMonitorMock.Object,
                _testApiCredentialOptionMonitorMock.Object,
                _testPlayerApiCredentialOptionMonitorMock.Object,
                _mailSendSettingsOptionsMonitor.Object,
                _healthCheckManagerLoggerMock.Object);
        }
    }
}

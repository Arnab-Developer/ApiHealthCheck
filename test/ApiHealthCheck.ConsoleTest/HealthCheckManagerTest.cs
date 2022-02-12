using ApiHealthCheck.Console;
using ApiHealthCheck.Lib;
using ApiHealthCheck.Lib.Settings;
using DeepCopy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ApiHealthCheck.ConsoleTest;

public class HealthCheckManagerTest
{
    private readonly Mock<IHealthCheck> _healthCheckMock;
    private readonly Mock<ISendMail> _sendMailMock;
    private readonly Mock<ILogger<HealthCheckManager>> _healthCheckManagerLoggerMock;
    private readonly Mock<IOptionsMonitor<MailSendSettings>> _mailSendSettingsOptionsMonitorMock;
    private MailSendSettings? _mailSendSettings;
    private IHealthCheckManager? _healthCheckManager;

    public HealthCheckManagerTest()
    {
        _healthCheckMock = new Mock<IHealthCheck>();
        _sendMailMock = new Mock<ISendMail>();
        _healthCheckManagerLoggerMock = new Mock<ILogger<HealthCheckManager>>();
        _mailSendSettingsOptionsMonitorMock = new Mock<IOptionsMonitor<MailSendSettings>>();
    }

    [Fact]
    public void LogHealthCheckResultSuccessTest()
    {
        _mailSendSettings = new MailSendSettings(true);

        _mailSendSettingsOptionsMonitorMock
            .Setup(s => s.CurrentValue)
            .Returns(_mailSendSettings);

        _sendMailMock
            .Setup(s => s.SendMailToCustomer(It.IsAny<string>()))
            .Verifiable();

        IEnumerable<ApiDetail> apiDetails = new List<ApiDetail>()
        {
            new ApiDetail
            (
                "api1",
                "https://api1/hc",
                new ApiCredential("u1", "p1"),
                true
            ),
            new ApiDetail
            (
                "api2",
                "https://api2/hc",
                new ApiCredential("u2", "p2"),
                true
            )
        };

        foreach (ApiDetail apiDetail in apiDetails)
        {
            _healthCheckMock
                .Setup(s => s.IsApiHealthy(apiDetail.Url, apiDetail.ApiCredential))
                .Returns(true);
        }

        _healthCheckManager = new HealthCheckManager(
            _healthCheckMock.Object,
            _sendMailMock.Object,
            _mailSendSettingsOptionsMonitorMock.Object,
            apiDetails,
            _healthCheckManagerLoggerMock.Object);

        string apiStatusMessage = _healthCheckManager!.LogHealthCheckResult();

        _healthCheckMock
            .Verify(v => v.IsApiHealthy(It.IsAny<string>(), It.IsAny<ApiCredential>()),
                Times.Exactly(apiDetails.Count()));

        _sendMailMock
            .Verify(v => v.SendMailToCustomer(It.IsAny<string>()),
                Times.Once);

        Assert.Equal(
            "api1 status is: OK\napi2 status is: OK\n",
            apiStatusMessage);
    }

    [Fact]
    public void LogHealthCheckResultFailTest()
    {
        _mailSendSettings = new MailSendSettings(true);

        _mailSendSettingsOptionsMonitorMock
            .Setup(s => s.CurrentValue)
            .Returns(_mailSendSettings);

        _sendMailMock
            .Setup(s => s.SendMailToCustomer(It.IsAny<string>()))
            .Verifiable();

        IEnumerable<ApiDetail> apiDetails = new List<ApiDetail>()
        {
            new ApiDetail
            (
                "api1",
                "https://api1/hc",
                new ApiCredential("u1", "p1"),
                true
            ),
            new ApiDetail
            (
                "api2",
                "https://api2/hc",
                new ApiCredential("u2", "p2"),
                true
            )
        };

        _healthCheckMock
            .Setup(s => s.IsApiHealthy(apiDetails.ElementAt(0).Url, apiDetails.ElementAt(0).ApiCredential))
            .Returns(true);

        _healthCheckMock
            .Setup(s => s.IsApiHealthy(apiDetails.ElementAt(1).Url, apiDetails.ElementAt(1).ApiCredential))
            .Returns(false);

        _healthCheckManager = new HealthCheckManager(
            _healthCheckMock.Object,
            _sendMailMock.Object,
            _mailSendSettingsOptionsMonitorMock.Object,
            apiDetails,
            _healthCheckManagerLoggerMock.Object);

        string apiStatusMessage = _healthCheckManager!.LogHealthCheckResult();

        _healthCheckMock
            .Verify(v => v.IsApiHealthy(It.IsAny<string>(), It.IsAny<ApiCredential>()),
                Times.Exactly(apiDetails.Count()));

        _sendMailMock
            .Verify(v => v.SendMailToCustomer(It.IsAny<string>()),
                Times.Once);

        Assert.Equal(
            "api1 status is: OK\napi2 status is: Error\n",
            apiStatusMessage);
    }

    [Fact]
    public void LogHealthCheckResultExceptionTest()
    {
        _mailSendSettings = new MailSendSettings(true);

        _mailSendSettingsOptionsMonitorMock
            .Setup(s => s.CurrentValue)
            .Returns(_mailSendSettings);

        _sendMailMock
            .Setup(s => s.SendMailToCustomer(It.IsAny<string>()))
            .Verifiable();

        IEnumerable<ApiDetail> apiDetails = new List<ApiDetail>()
        {
            new ApiDetail
            (
                "api1",
                "https://api1/hc",
                new ApiCredential("u1", "p1"),
                true
            ),
            new ApiDetail
            (
                "api2",
                "https://api2/hc",
                new ApiCredential("u2", "p2"),
                true
            )
        };

        _healthCheckMock
            .Setup(s => s.IsApiHealthy(apiDetails.ElementAt(0).Url, apiDetails.ElementAt(0).ApiCredential))
            .Throws<ArgumentException>();

        _healthCheckMock
            .Setup(s => s.IsApiHealthy(apiDetails.ElementAt(1).Url, apiDetails.ElementAt(1).ApiCredential))
            .Returns(true);

        _healthCheckManager = new HealthCheckManager(
            _healthCheckMock.Object,
            _sendMailMock.Object,
            _mailSendSettingsOptionsMonitorMock.Object,
            apiDetails,
            _healthCheckManagerLoggerMock.Object);

        string apiStatusMessage = _healthCheckManager!.LogHealthCheckResult();

        _healthCheckMock
            .Verify(v => v.IsApiHealthy(It.IsAny<string>(), It.IsAny<ApiCredential>()),
                Times.Exactly(apiDetails.Count()));

        _sendMailMock
            .Verify(v => v.SendMailToCustomer(It.IsAny<string>()),
                Times.Once);

        Assert.Equal(
            "api1 status is: Error\napi2 status is: OK\n",
            apiStatusMessage);
    }

    [Fact]
    public void LogHealthCheckResultDonotSendMailTest()
    {
        _mailSendSettings = new MailSendSettings(false);

        _mailSendSettingsOptionsMonitorMock
            .Setup(s => s.CurrentValue)
            .Returns(_mailSendSettings);

        _sendMailMock
            .Setup(s => s.SendMailToCustomer(It.IsAny<string>()))
            .Verifiable();

        IEnumerable<ApiDetail> apiDetails = new List<ApiDetail>()
        {
            new ApiDetail
            (
                "api1",
                "https://api1/hc",
                new ApiCredential("u1", "p1"),
                true
            ),
            new ApiDetail
            (
                "api2",
                "https://api2/hc",
                new ApiCredential("u2", "p2"),
                true
            )
        };

        _healthCheckMock
            .Setup(s => s.IsApiHealthy(apiDetails.ElementAt(0).Url, apiDetails.ElementAt(0).ApiCredential))
            .Throws<ArgumentException>();

        _healthCheckMock
            .Setup(s => s.IsApiHealthy(apiDetails.ElementAt(1).Url, apiDetails.ElementAt(1).ApiCredential))
            .Returns(true);

        _healthCheckManager = new HealthCheckManager(
            _healthCheckMock.Object,
            _sendMailMock.Object,
            _mailSendSettingsOptionsMonitorMock.Object,
            apiDetails,
            _healthCheckManagerLoggerMock.Object);

        string apiStatusMessage = _healthCheckManager!.LogHealthCheckResult();

        _healthCheckMock
            .Verify(v => v.IsApiHealthy(It.IsAny<string>(), It.IsAny<ApiCredential>()),
                Times.Exactly(apiDetails.Count()));

        _sendMailMock
            .Verify(v => v.SendMailToCustomer(It.IsAny<string>()),
                Times.Never);

        Assert.Equal(
            "api1 status is: Error\napi2 status is: OK\n",
            apiStatusMessage);
    }

    [Fact]
    public void LogHealthCheckResultDisableUrlTest()
    {
        _mailSendSettings = new MailSendSettings(false);

        _mailSendSettingsOptionsMonitorMock
            .Setup(s => s.CurrentValue)
            .Returns(_mailSendSettings);

        _sendMailMock
            .Setup(s => s.SendMailToCustomer(It.IsAny<string>()))
            .Verifiable();

        IEnumerable<ApiDetail> apiDetails = new List<ApiDetail>()
        {
            new ApiDetail
            (
                "api1",
                "https://api1/hc",
                new ApiCredential("u1", "p1"),
                false
            ),
            new ApiDetail
            (
                "api2",
                "https://api2/hc",
                new ApiCredential("u2", "p2"),
                true
            )
        };

        _healthCheckMock
            .Setup(s => s.IsApiHealthy(apiDetails.ElementAt(0).Url, apiDetails.ElementAt(0).ApiCredential))
            .Throws<ArgumentException>();

        _healthCheckMock
            .Setup(s => s.IsApiHealthy(apiDetails.ElementAt(1).Url, apiDetails.ElementAt(1).ApiCredential))
            .Returns(true);

        _healthCheckManager = new HealthCheckManager(
            _healthCheckMock.Object,
            _sendMailMock.Object,
            _mailSendSettingsOptionsMonitorMock.Object,
            apiDetails,
            _healthCheckManagerLoggerMock.Object);

        string apiStatusMessage = _healthCheckManager!.LogHealthCheckResult();

        _healthCheckMock
            .Verify(v => v.IsApiHealthy(It.IsAny<string>(), It.IsAny<ApiCredential>()),
                Times.Exactly(apiDetails.Count(apiDetail => apiDetail.IsEnable)));

        _sendMailMock
            .Verify(v => v.SendMailToCustomer(It.IsAny<string>()),
                Times.Never);

        Assert.Equal(
            "api2 status is: OK\n",
            apiStatusMessage);
    }

    [Fact]
    public void LogHealthCheckResultApiDetailsTest()
    {
        _mailSendSettings = new MailSendSettings(false);

        _mailSendSettingsOptionsMonitorMock
            .Setup(s => s.CurrentValue)
            .Returns(_mailSendSettings);

        _sendMailMock
            .Setup(s => s.SendMailToCustomer(It.IsAny<string>()))
            .Verifiable();

        IEnumerable<ApiDetail> apiDetails = new List<ApiDetail>()
        {
            new ApiDetail
            (
                "api1",
                "https://api1/hc",
                new ApiCredential("u1", "p1"),
                false
            ),
            new ApiDetail
            (
                "api2",
                "https://api2/hc",
                new ApiCredential("u2", "p2"),
                true
            )
        };

        IEnumerable<ApiDetail> expectedApiDetails = DeepCopier.Copy(apiDetails);

        _healthCheckMock
            .Setup(s => s.IsApiHealthy(apiDetails.ElementAt(0).Url, apiDetails.ElementAt(0).ApiCredential))
            .Throws<ArgumentException>();

        _healthCheckMock
            .Setup(s => s.IsApiHealthy(apiDetails.ElementAt(1).Url, apiDetails.ElementAt(1).ApiCredential))
            .Returns(true);

        _healthCheckManager = new HealthCheckManager(
            _healthCheckMock.Object,
            _sendMailMock.Object,
            _mailSendSettingsOptionsMonitorMock.Object,
            apiDetails,
            _healthCheckManagerLoggerMock.Object);

        string apiStatusMessage = _healthCheckManager!.LogHealthCheckResult();

        Assert.Same(apiDetails, _healthCheckManager.ApiDetails);
    }

    [Fact]
    public void LogHealthCheckResultNoCredTest()
    {
        _mailSendSettings = new MailSendSettings(true);

        _mailSendSettingsOptionsMonitorMock
            .Setup(s => s.CurrentValue)
            .Returns(_mailSendSettings);

        _sendMailMock
            .Setup(s => s.SendMailToCustomer(It.IsAny<string>()))
            .Verifiable();

        IEnumerable<ApiDetail> apiDetails = new List<ApiDetail>()
        {
            new ApiDetail
            (
                "api1",
                "https://api1/hc",
                null,
                true
            ),
            new ApiDetail
            (
                "api2",
                "https://api2/hc",
                new ApiCredential("u2", "p2"),
                true
            )
        };

        _healthCheckMock
            .Setup(s => s.IsApiHealthy(apiDetails.ElementAt(0).Url, null))
            .Returns(true);

        _healthCheckMock
            .Setup(s => s.IsApiHealthy(apiDetails.ElementAt(1).Url, apiDetails.ElementAt(1).ApiCredential))
            .Returns(true);

        _healthCheckManager = new HealthCheckManager(
            _healthCheckMock.Object,
            _sendMailMock.Object,
            _mailSendSettingsOptionsMonitorMock.Object,
            apiDetails,
            _healthCheckManagerLoggerMock.Object);

        string apiStatusMessage = _healthCheckManager!.LogHealthCheckResult();

        _healthCheckMock
            .Verify(v => v.IsApiHealthy(It.IsAny<string>(), It.IsAny<ApiCredential>()),
                Times.Exactly(apiDetails.Count(apiDetail => apiDetail.IsEnable)));

        _sendMailMock
            .Verify(v => v.SendMailToCustomer(It.IsAny<string>()),
                Times.Once);

        Assert.Equal(
            "api1 status is: OK\napi2 status is: OK\n",
            apiStatusMessage);
    }
}

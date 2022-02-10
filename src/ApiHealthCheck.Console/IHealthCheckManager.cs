using ApiHealthCheck.Lib.Settings;

namespace ApiHealthCheck.Console;

internal interface IHealthCheckManager
{
    IEnumerable<ApiDetail> ApiDetails { get; }

    string LogHealthCheckResult();
}

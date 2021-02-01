using ApiHealthCheck.Console.Settings;
using System.Collections.Generic;

namespace ApiHealthCheck.Console
{
    internal interface IHealthCheckManager
    {
        IEnumerable<ApiDetail> UrlDetails { get; }

        string LogHealthCheckResult();
    }
}
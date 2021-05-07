using ApiHealthCheck.Lib.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiHealthCheck.Console
{
    internal class HealthCheckService : IHostedService, IDisposable
    {
        private Timer? _timer;
        private readonly IOptionsMonitor<ExecutionSettings> _executionSettingsOptionsMonitor;
        private readonly IHealthCheckManager _healthCheckManager;

        public HealthCheckService(
            IHealthCheckManager healthCheckManager,
            IOptionsMonitor<ExecutionSettings> executionSettingsOptionsMonitor)
        {
            _healthCheckManager = healthCheckManager;
            _executionSettingsOptionsMonitor = executionSettingsOptionsMonitor;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(_executionSettingsOptionsMonitor.CurrentValue.ExecutionFrequency));
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            _healthCheckManager.LogHealthCheckResult();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

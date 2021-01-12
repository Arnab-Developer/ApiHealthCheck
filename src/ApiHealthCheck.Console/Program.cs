using ApiHealthCheck.Console;
using ApiHealthCheck.Console.Settings;
using ApiHealthCheck.Lib;
using ApiHealthCheck.Lib.Credentials;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ApiHealthCheck.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

using IHost host = CreateHostBuilder().Build();
await host.StartAsync();
await host.WaitForShutdownAsync();

static IHostBuilder CreateHostBuilder() =>
    Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            services
                .AddTransient(typeof(IHealthCheckManager), typeof(HealthCheckManager))
                .AddTransient(typeof(IHealthCheck), typeof(HealthCheck))
                .Configure<ExecutionSettings>(context.Configuration)
                .Configure<Urls>(context.Configuration.GetSection("Urls"))
                .Configure<ProductApiCredential>(context.Configuration.GetSection("Credential:ProductApi"))
                .Configure<ExecutionSettings>(context.Configuration)
                .AddHostedService<HealthCheckService>();
        })
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddEventLog(configuration => configuration.SourceName = "Api health check");
        });
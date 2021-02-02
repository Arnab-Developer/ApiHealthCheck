using ApiHealthCheck.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using System;
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
                .AddServices(context)
                .ConfigureSettings(context.Configuration)
                .AddHostedService<HealthCheckService>();
        })
        .ConfigureLogging((context, builder) =>
        {
            builder.ClearProviders();
            builder.AddEventLog(configuration => configuration.SourceName = "Api health check");
            builder.AddApplicationInsights(context.Configuration.GetValue<string>("ApplicationInsights:Key"));
            builder.AddFilter<ApplicationInsightsLoggerProvider>(
                typeof(HealthCheckManager).FullName,
                (LogLevel)Enum.Parse(typeof(LogLevel), context.Configuration.GetValue<string>("ApplicationInsights:LogLevel:Default")));
        });
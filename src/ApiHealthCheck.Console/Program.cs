using ApiHealthCheck.Console;
using ApiHealthCheck.Console.Settings;
using ApiHealthCheck.Lib;
using ApiHealthCheck.Lib.Credentials;
using ApiHealthCheck.Lib.Settings;
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
                .AddTransient(typeof(IHealthCheckManager), typeof(HealthCheckManager))
                .AddTransient(typeof(IHealthCheck), typeof(HealthCheck))
                .AddTransient<ISendMail>(options =>
                {
                    MailSettings mailSettings = new()
                    {
                        IsEnable = context.Configuration.GetValue<bool>("MailSettings:IsEnable"),
                        From = context.Configuration.GetValue<string>("MailSettings:From"),
                        To = context.Configuration.GetValue<string>("MailSettings:To"),
                        Subject = context.Configuration.GetValue<string>("MailSettings:Subject"),
                        Host = context.Configuration.GetValue<string>("MailSettings:Host"),
                        Port = context.Configuration.GetValue<int>("MailSettings:Port"),
                        UserName = context.Configuration.GetValue<string>("MailSettings:UserName"),
                        Password = context.Configuration.GetValue<string>("MailSettings:Password"),
                        EnableSsl = context.Configuration.GetValue<string>("MailSettings:EnableSsl")
                    };
                    SendMail sendMail = new(mailSettings);
                    return sendMail;
                })
                .Configure<ExecutionSettings>(context.Configuration)
                .Configure<Urls>(context.Configuration.GetSection("Urls"))
                .Configure<ProductApiCredential>(context.Configuration.GetSection("Credential:ProductApi"))
                .Configure<ResultApiCredential>(context.Configuration.GetSection("Credential:ResultApi"))
                .Configure<ContentApiCredential>(context.Configuration.GetSection("Credential:ContentApi"))
                .Configure<TestApiCredential>(context.Configuration.GetSection("Credential:TestApi"))
                .Configure<TestPlayerApiCredential>(context.Configuration.GetSection("Credential:TestPlayerApi"))
                .Configure<ExecutionSettings>(context.Configuration)
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
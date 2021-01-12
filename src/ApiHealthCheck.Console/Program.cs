using ApiHealthCheck.Console;
using ApiHealthCheck.Console.Settings;
using ApiHealthCheck.Lib;
using ApiHealthCheck.Lib.Credentials;
using ApiHealthCheck.Lib.Settings;
using Microsoft.Extensions.Configuration;
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
                .AddTransient<ISendMail>(options =>
                {
                    MailSettings mailSettings = new()
                    {
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
                .Configure<ExecutionSettings>(context.Configuration)
                .AddHostedService<HealthCheckService>();
        })
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddEventLog(configuration => configuration.SourceName = "Api health check");
        });
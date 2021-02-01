using ApiHealthCheck.Console;
using ApiHealthCheck.Console.Settings;
using ApiHealthCheck.Lib;
using ApiHealthCheck.Lib.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using System;
using System.Collections.Generic;
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
                .AddTransient(options =>
                {
                    IEnumerable<ApiDetail> urlDetails = new List<ApiDetail>()
                    {
                        new ApiDetail()
                        {
                            Name = "Product api",
                            Url = context.Configuration.GetValue<string>("Urls:ProductApiUrl"),
                            ApiCredential = new ApiCredential()
                            {
                                UserName = context.Configuration.GetValue<string>("Credential:ProductApi:UserName"),
                                Password = context.Configuration.GetValue<string>("Credential:ProductApi:Password")
                            },
                            IsEnable = context.Configuration.GetValue<bool>("UrlsIsEnable:IsCheckProductApi")
                        },
                        new ApiDetail()
                        {
                            Name = "Result api",
                            Url = context.Configuration.GetValue<string>("Urls:ResultApiUrl"),
                            ApiCredential = new ApiCredential()
                            {
                                UserName = context.Configuration.GetValue<string>("Credential:ResultApi:UserName"),
                                Password = context.Configuration.GetValue<string>("Credential:ResultApi:Password")
                            },
                            IsEnable = context.Configuration.GetValue<bool>("UrlsIsEnable:IsCheckResultApi")
                        },
                        new ApiDetail()
                        {
                            Name = "Content api",
                            Url = context.Configuration.GetValue<string>("Urls:ContentApiUrl"),
                            ApiCredential = new ApiCredential()
                            {
                                UserName = context.Configuration.GetValue<string>("Credential:ContentApi:UserName"),
                                Password = context.Configuration.GetValue<string>("Credential:ContentApi:Password")
                            },
                            IsEnable = context.Configuration.GetValue<bool>("UrlsIsEnable:IsCheckContentApi")
                        },
                        new ApiDetail()
                        {
                            Name = "Test api",
                            Url = context.Configuration.GetValue<string>("Urls:TestApiUrl"),
                            ApiCredential = new ApiCredential()
                            {
                                UserName = context.Configuration.GetValue<string>("Credential:TestApi:UserName"),
                                Password = context.Configuration.GetValue<string>("Credential:TestApi:Password")
                            },
                            IsEnable = context.Configuration.GetValue<bool>("UrlsIsEnable:IsCheckTestApi")
                        },
                        new ApiDetail()
                        {
                            Name = "Test player api",
                            Url = context.Configuration.GetValue<string>("Urls:TestPlayerApiUrl"),
                            ApiCredential = new ApiCredential()
                            {
                                UserName = context.Configuration.GetValue<string>("Credential:TestPlayerApi:UserName"),
                                Password = context.Configuration.GetValue<string>("Credential:TestPlayerApi:Password")
                            },
                            IsEnable = context.Configuration.GetValue<bool>("UrlsIsEnable:IsCheckTestPlayerApi")
                        }
                    };
                    return urlDetails;
                })
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
                .Configure<ExecutionSettings>(context.Configuration)
                .Configure<MailSendSettings>(context.Configuration)
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
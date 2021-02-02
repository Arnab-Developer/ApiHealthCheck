using ApiHealthCheck.Console.Settings;
using ApiHealthCheck.Lib;
using ApiHealthCheck.Lib.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace ApiHealthCheck.Console
{
    internal static class DIExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, HostBuilderContext context)
        {
            services
                .AddTransient(options =>
                {
                    IEnumerable<ApiDetail> urlDetails = new List<ApiDetail>()
                    {
                        new ApiDetail
                        (
                            "Product api",
                            context.Configuration.GetValue<string>("Urls:ProductApiUrl"),
                            new ApiCredential
                            (
                                context.Configuration.GetValue<string>("Credential:ProductApi:UserName"),
                                context.Configuration.GetValue<string>("Credential:ProductApi:Password")
                            ),
                            context.Configuration.GetValue<bool>("UrlsIsEnable:IsCheckProductApi")
                        ),
                        new ApiDetail
                        (
                            "Result api",
                            context.Configuration.GetValue<string>("Urls:ResultApiUrl"),
                            new ApiCredential
                            (
                                context.Configuration.GetValue<string>("Credential:ResultApi:UserName"),
                                context.Configuration.GetValue<string>("Credential:ResultApi:Password")
                            ),
                            context.Configuration.GetValue<bool>("UrlsIsEnable:IsCheckResultApi")
                        ),
                        new ApiDetail
                        (
                            "Content api",
                            context.Configuration.GetValue<string>("Urls:ContentApiUrl"),
                            new ApiCredential
                            (
                                context.Configuration.GetValue<string>("Credential:ContentApi:UserName"),
                                context.Configuration.GetValue<string>("Credential:ContentApi:Password")
                            ),
                            context.Configuration.GetValue<bool>("UrlsIsEnable:IsCheckContentApi")
                        ),
                        new ApiDetail
                        (
                            "Test api",
                            context.Configuration.GetValue<string>("Urls:TestApiUrl"),
                            new ApiCredential
                            (
                                context.Configuration.GetValue<string>("Credential:TestApi:UserName"),
                                context.Configuration.GetValue<string>("Credential:TestApi:Password")
                            ),
                            context.Configuration.GetValue<bool>("UrlsIsEnable:IsCheckTestApi")
                        ),
                        new ApiDetail
                        (
                            "Test player api",
                            context.Configuration.GetValue<string>("Urls:TestPlayerApiUrl"),
                            new ApiCredential
                            (
                                context.Configuration.GetValue<string>("Credential:TestPlayerApi:UserName"),
                                context.Configuration.GetValue<string>("Credential:TestPlayerApi:Password")
                            ),
                            context.Configuration.GetValue<bool>("UrlsIsEnable:IsCheckTestPlayerApi")
                        )
                    };
                    return urlDetails;
                })
                .AddTransient(typeof(IHealthCheckManager), typeof(HealthCheckManager))
                .AddTransient(typeof(IHealthCheck), typeof(HealthCheck))
                .AddTransient<ISendMail>(options =>
                {
                    MailSettings mailSettings = new
                    (
                        context.Configuration.GetValue<string>("MailSettings:From"),
                        context.Configuration.GetValue<string>("MailSettings:To"),
                        context.Configuration.GetValue<string>("MailSettings:Subject"),
                        context.Configuration.GetValue<string>("MailSettings:Host"),
                        context.Configuration.GetValue<int>("MailSettings:Port"),
                        context.Configuration.GetValue<string>("MailSettings:UserName"),
                        context.Configuration.GetValue<string>("MailSettings:Password"),
                        context.Configuration.GetValue<string>("MailSettings:EnableSsl")
                    );
                    SendMail sendMail = new(mailSettings);
                    return sendMail;
                });

            return services;
        }

        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration config)
        {
            services
                .Configure<ExecutionSettings>(config)
                .Configure<MailSendSettings>(config);

            return services;
        }
    }
}

using ApiHealthCheck.Lib;
using ApiHealthCheck.Lib.Settings;

namespace ApiHealthCheck.Console;

internal static class DIExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, HostBuilderContext context)
    {
        services
            .AddTransient(options =>
            {
                IEnumerable<IConfigurationSection> sections = context.Configuration.GetSection("ApiDetails").GetChildren();
                List<ApiDetail> urlDetails = new();
                for (var counter = 0; counter < sections.Count(); counter++)
                {
                    urlDetails.Add(new ApiDetail
                    (
                        context.Configuration.GetValue<string>($"ApiDetails:{counter}:Name"),
                        context.Configuration.GetValue<string>($"ApiDetails:{counter}:Url"),
                        new ApiCredential
                        (
                            context.Configuration.GetValue<string>($"ApiDetails:{counter}:Credential:UserName"),
                            context.Configuration.GetValue<string>($"ApiDetails:{counter}:Credential:Password")
                        ),
                        context.Configuration.GetValue<bool>($"ApiDetails:{counter}:IsEnable")
                    ));
                }
                return urlDetails.AsEnumerable();
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

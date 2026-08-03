using KinGraph.Core.Aggregates.UserAggregate;
using KinGraph.Core.Interfaces;
using KinGraph.Infrastructure;
using KinGraph.Infrastructure.Email;
using Microsoft.AspNetCore.Identity;

namespace KinGraph.Web.Configurations;

public static class ServiceConfigurations
{
    public const string AngularDevCorsPolicy = "AngularDev";
    public static IServiceCollection AddServiceConfigurations(
        this IServiceCollection services,
        Microsoft.Extensions.Logging.ILogger logger,
        WebApplicationBuilder builder
    )
    {
        services
            .AddInfrastructureServices(builder.Configuration, logger)
            .AddMediatorSourceGenerator(logger);

        if (builder.Environment.IsDevelopment())
        {
            // Use a local test email server - configured in Aspire
            // See: https://ardalis.com/configuring-a-local-test-email-server/
            services.AddScoped<IEmailSender, MimeKitEmailSender>();

            // Otherwise use this:
            //builder.Services.AddScoped<IEmailSender, FakeEmailSender>();
        }
        else
        {
            services.AddScoped<IEmailSender, MimeKitEmailSender>();
        }

        // Stateless/thread-safe, so a singleton is fine (and avoids per-request allocation).
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        logger.LogInformation(
            "{Project} services registered",
            "Mediator Source Generator and Email Sender"
        );

        return services;
    }
}

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

        services.AddScoped<IEmailSender, MimeKitEmailSender>();

        // Stateless/thread-safe, so a singleton is fine (and avoids per-request allocation).
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddCors(options =>
        {
            options.AddPolicy(
                AngularDevCorsPolicy,
                policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()
            );
        });

        logger.LogInformation(
            "{Project} services registered",
            "Mediator Source Generator and Email Sender"
        );

        return services;
    }
}

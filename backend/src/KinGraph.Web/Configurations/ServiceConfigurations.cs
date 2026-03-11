using KinGraph.Core.Interfaces;
using KinGraph.Infrastructure;
using KinGraph.Infrastructure.Email;

namespace KinGraph.Web.Configurations;

public static class ServiceConfigurations
{
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

        logger.LogInformation(
            "{Project} services registered",
            "Mediator Source Generator and Email Sender"
        );

        return services;
    }
}

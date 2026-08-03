namespace KinGraph.Web.Configurations;

public static class CorsConfigurations
{
    public const string DefaultCorsPolicy = "DefaultCorsPolicy";

    public static IServiceCollection AddCorsConfigurations(
        this IServiceCollection services,
        IConfiguration configuration,
        Microsoft.Extensions.Logging.ILogger logger
    )
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

        services.AddCors(options =>
        {
            options.AddPolicy(
                DefaultCorsPolicy,
                policy => policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod()
            );
        });

        logger.LogInformation("{Project} were configured", "CORS");

        return services;
    }
}

var builder = DistributedApplication.CreateBuilder(args);

// Add Postgre SQL Server container
var postgres = builder
    .AddPostgres("postgres")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

// Add the database
var applicationDatabase = postgres.AddDatabase("kingraph");

// register the API project and link the DB
var api = builder
    .AddProject<Projects.KinGraph_Web>("web")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", builder.Environment.EnvironmentName)
    .WithReference(applicationDatabase)
    .WaitFor(applicationDatabase)
    .WithHttpEndpoint(name: "api-http");
;

// Add frontend service and reference the API
var frontend = builder
    .AddViteApp("frontend", "../../../frontend")
    .WithHttpEndpoint(name: "frontend-http", env: "PORT")
    .WithReference(api);

builder.Build().Run();

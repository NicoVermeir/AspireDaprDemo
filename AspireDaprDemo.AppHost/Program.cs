var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Presentation_API>("presentation-api")
    .WithDaprSidecar("external-api-dapr");

builder.AddProject<Projects.UI>("ui")
    .WithDaprSidecar("external-ui-dapr");

builder.Build().Run();

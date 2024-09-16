var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.Presentation_API>("presentation-api");

builder.AddProject<Projects.UI>("ui")
    .WithReference(api);

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Presentation_API>("presentation-api");

builder.AddProject<Projects.UI>("ui");

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

//pw
//fixedport
//volume

var sql = builder.AddSqlServer("DemoSqlServer", builder.AddParameter("sqlServerDevPass", true), 1433)
    .WithVolume("VolumeMount.sqlserver.data", "/var/opt/mssql")
    .AddDatabase("DemoDB");

var fileStore = builder.AddDaprStateStore("filestore");
var appInsights = builder.AddConnectionString("appInsights");

var api =
    builder.AddProject<Projects.Presentation_Api>("presentation-api")
        .WithReference(sql)
        .WithReference(fileStore)
        .WithReference(appInsights)
        .WithDaprSidecar("presentation-api-dapr");

builder.AddProject<Projects.UI>("ui")
    .WithDaprSidecar("presentation-ui-dapr")
    .WithReference(api);


builder.AddProject<Projects.ExternalApp_MigrationService>("externalapp-migrationservice");


builder.Build().Run();

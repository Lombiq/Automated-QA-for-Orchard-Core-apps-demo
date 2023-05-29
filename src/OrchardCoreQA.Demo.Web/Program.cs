using OrchardCore.Logging;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLogHost();

var configuration = builder.Configuration;

builder.Services
    .AddSingleton(configuration)
    .AddOrchardCms(orchardCoreBuilder =>
    {
        orchardCoreBuilder
            .AddDatabaseShellsConfigurationIfAvailable(configuration)
            .ConfigureSmtpSettings();

        if (!configuration.IsUITesting())
        {
            orchardCoreBuilder
                .AddSetupFeatures("OrchardCore.AutoSetup");
        }
    });

var app = builder.Build();

app.UseStaticFiles();
app.UseOrchardCore();
app.Run();

[SuppressMessage(
    "Design",
    "CA1050: Declare types in namespaces",
    Justification = "As described here: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0.")]
public partial class Program
{
    protected Program()
    {
        // Nothing to do here.
    }
}

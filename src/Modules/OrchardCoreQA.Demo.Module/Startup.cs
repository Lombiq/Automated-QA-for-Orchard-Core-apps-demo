using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCoreQA.Demo.Module.Services;

namespace OrchardCoreQA.Demo.Module;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services) =>
        services.AddScoped<ITestedService, TestedService>();
}

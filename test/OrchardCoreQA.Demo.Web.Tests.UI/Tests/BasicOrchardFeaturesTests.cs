using Lombiq.Tests.UI.Attributes;
using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using System.Threading.Tasks;
using OrchardCoreQA.Demo.Web.Tests.UI.Constants;
using Xunit;
using Xunit.Abstractions;

namespace OrchardCoreQA.Demo.Web.Tests.UI.Tests;

public class BasicOrchardFeaturesTests : UITestBase
{
    public BasicOrchardFeaturesTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }

    [Theory, Chrome]
    public Task BasicOrchardFeaturesShouldWork(Browser browser) =>
        ExecuteTestAsync(
            context => context.TestBasicOrchardFeaturesExceptRegistrationAsync(RecipeIds.Tests),
            browser);
}

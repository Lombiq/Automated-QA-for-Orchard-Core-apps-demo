using Lombiq.Tests.UI;
using Lombiq.Tests.UI.Services;
using OrchardCoreQA.Demo.Web.Tests.UI.Helpers;
using System;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace OrchardCoreQA.Demo.Web.Tests.UI;

public class UITestBase : OrchardCoreUITestBase<Program>
{
    protected UITestBase(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }

    protected override Task ExecuteTestAfterSetupAsync(
        Func<UITestContext, Task> testAsync,
        Browser browser,
        Func<OrchardCoreUITestExecutorConfiguration, Task> changeConfigurationAsync) =>
        ExecuteTestAsync(testAsync, browser, SetupHelpers.RunSetupAsync, changeConfigurationAsync);

    protected override Task ExecuteTestAsync(
        Func<UITestContext, Task> testAsync,
        Browser browser,
        Func<UITestContext, Task<Uri>> setupOperation = null,
        Action<OrchardCoreUITestExecutorConfiguration> changeConfiguration = null) =>
        base.ExecuteTestAsync(
            testAsync,
            browser,
            setupOperation,
            configuration =>
            {
                configuration
                    .AccessibilityCheckingConfiguration.RunAccessibilityCheckingAssertionOnAllPageChanges = true;

                // Uncomment if SQL Server will be used for the tests.
                ////configuration.UseSqlServer = true;

                changeConfiguration?.Invoke(configuration);
            });
}

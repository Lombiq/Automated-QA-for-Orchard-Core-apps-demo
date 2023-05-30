using Lombiq.Tests.UI.Attributes;
using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.MonkeyTesting;
using Lombiq.Tests.UI.Services;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace OrchardCoreQA.Demo.Web.Tests.UI.Tests;

public class MonkeyTests : UITestBase
{
    public MonkeyTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }

    [Theory, Chrome]
    public Task TestFrontendAsMonkey(Browser browser) =>
    ExecuteTestAfterSetupAsync(
        async context => await context.TestFrontendAuthenticatedAsMonkeyRecursivelyAsync(CreateMonkeyTestingOptions()),
        browser);

    private static MonkeyTestingOptions CreateMonkeyTestingOptions() =>
        new()
        {
            PageTestTime = TimeSpan.FromSeconds(10),
        };
}

using Lombiq.Tests.UI.Attributes;
using Lombiq.Tests.UI.Constants;
using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace OrchardCoreQA.Demo.Web.Tests.UI.Tests;

public class LoginTests : UITestBase
{
    public LoginTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }

    [Theory, Chrome]
    public Task LoginShouldWorkWithCorrectCredentials(Browser browser) =>
        ExecuteTestAfterSetupAsync(
            async context =>
            {
                await context.GoToRelativeUrlAsync("/Login");

                await context.FillInWithRetriesAsync(By.Id("UserName"), DefaultUser.UserName);
                await context.FillInWithRetriesAsync(By.Id("Password"), DefaultUser.Password);

                await context.ClickReliablyOnSubmitAsync();

                await context.ClickReliablyOnAsync(By.ClassName("navbar-toggler"));
                context.Exists(By.ClassName("fa-user"));
                context.Get(By.CssSelector("a.nav-link.dropdown-toggle")).Text.ShouldContain(DefaultUser.UserName);

                (await context.GetCurrentUserNameAsync()).ShouldBe(DefaultUser.UserName);
            },
            browser);
}

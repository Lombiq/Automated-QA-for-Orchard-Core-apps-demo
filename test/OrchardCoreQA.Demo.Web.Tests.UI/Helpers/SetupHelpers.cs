using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Pages;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using OrchardCoreQA.Demo.Web.Tests.UI.Constants;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace OrchardCoreQA.Demo.Web.Tests.UI.Helpers;

public static class SetupHelpers
{
    public static async Task<Uri> RunSetupAsync(UITestContext context)
    {
        var homepageUri = await context.GoToSetupPageAndSetupOrchardCoreAsync(
            new OrchardCoreSetupParameters(context)
            {
                SiteName = "Orchard Core QA Demo",
                RecipeId = RecipeIds.Tests,
                TablePrefix = "Demo",
                SiteTimeZoneValue = "Europe/Budapest",
            });

        context.Get(By.CssSelector("h1.fs-4")).Text.ShouldBe("Log in");

        return homepageUri;
    }
}

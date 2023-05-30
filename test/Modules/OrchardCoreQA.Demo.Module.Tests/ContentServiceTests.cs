using Moq;
using Moq.AutoMock;
using OrchardCore.ContentManagement;
using OrchardCoreQA.Demo.Module.Services;
using Shouldly;
using Xunit;

namespace OrchardCoreQA.Demo.Module.Tests;

public class ContentServiceTests
{
    private const string TestContentId = "content ID";

    #region Simple test
    [Fact]
    public void ChangeDisplayTextShouldChangeDisplayText()
    {
        var service = CreateTestedService(out _);

        var contentItem = new ContentItem();

        var changedContentItem = service.ChangeDisplayText(contentItem, "new display text");

        changedContentItem.DisplayText.ShouldBe("new display text");
    }
    #endregion

    #region Test with multiple inputs
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NullOrEmptyArgumentsShouldThrow(string id)
    {
        var service = CreateTestedService(out _);
        Should.Throw<ArgumentNullException>(() => service.GetContentItemOrThrowAsync(id));
    }
    #endregion

    #region Tests with mocking
    [Fact]
    public void NonExistingContentItemsShouldThrow()
    {
        var service = CreateTestedService(out var mocker);

        Should.Throw<InvalidOperationException>(() => service.GetContentItemOrThrowAsync(TestContentId));

        mocker
            .GetMock<IContentManager>()
            .Verify(contentManager => contentManager.GetAsync(It.Is<string>(id => id == TestContentId)));
    }

    [Fact]
    public async Task ContentItemsAreRetrieved()
    {
        var service = CreateTestedService(out var mocker);

        mocker
            .GetMock<IContentManager>()
            .Setup(contentManager => contentManager.GetAsync(It.IsAny<string>()))
            .ReturnsAsync<string, IContentManager, ContentItem>(id => new ContentItem { ContentItemId = id });

        var contentItem = await service.GetContentItemOrThrowAsync(TestContentId);

        contentItem.ContentItemId.ShouldBe(TestContentId);
    }

    private static ContentService CreateTestedService(out AutoMocker mocker)
    {
        mocker = new AutoMocker();
        return mocker.CreateInstance<ContentService>();
    }
    #endregion
}

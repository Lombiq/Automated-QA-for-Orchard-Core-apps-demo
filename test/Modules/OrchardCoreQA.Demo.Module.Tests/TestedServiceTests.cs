using Moq;
using Moq.AutoMock;
using OrchardCore.ContentManagement;
using OrchardCoreQA.Demo.Module.Services;
using Shouldly;
using Xunit;

namespace OrchardCoreQA.Demo.Module.Tests;

public class TestedServiceTests
{
    private const string TestContentId = "content ID";

    [Fact]
    public void NonExistingContentItemsShouldThrow()
    {
        var service = CreateTestedService(out var mocker);

        Should.Throw<InvalidOperationException>(() => service.GetContentItemOrThrowAsync(TestContentId));

        mocker
            .GetMock<IContentManager>()
            .Verify(contentManager => contentManager.GetAsync(It.Is<string>(id => id == TestContentId)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NullOrEmptyArgumentsShouldThrow(string id)
    {
        var service = CreateTestedService(out _);
        Should.Throw<ArgumentNullException>(() => service.GetContentItemOrThrowAsync(id));
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

    private static TestedService CreateTestedService(out AutoMocker mocker)
    {
        mocker = new AutoMocker();
        return mocker.CreateInstance<TestedService>();
    }
}

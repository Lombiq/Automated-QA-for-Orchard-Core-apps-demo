using OrchardCore.ContentManagement;

namespace OrchardCoreQA.Demo.Module.Services;

public interface ITestedService
{
    Task<ContentItem> GetContentItemOrThrowAsync(string id);

}

public class TestedService : ITestedService
{
    private readonly IContentManager _contentManager;

    public TestedService(IContentManager contentManager) => _contentManager = contentManager;

    public Task<ContentItem> GetContentItemOrThrowAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id), "The supplied content item ID was null or empty.");
        }

        var contentItem = GetContentItemOrThrowInternal(id);

        return GetContentItemOrThrowInternal(id);
    }

    private async Task<ContentItem> GetContentItemOrThrowInternal(string id) =>
        await _contentManager.GetAsync(id) ?? throw new InvalidOperationException($"The content item with the ID {id} doesn't exist.");

    // TODO: Add method to retrieve multiple content items.
}

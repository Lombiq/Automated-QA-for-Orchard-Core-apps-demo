using OrchardCore.ContentManagement;

namespace OrchardCoreQA.Demo.Module.Services;

public interface IContentService
{
    Task<ContentItem> GetContentItemOrThrowAsync(string id);

    ContentItem ChangeDisplayText(ContentItem contentItem, string displayText);
}

public class ContentService : IContentService
{
    private readonly IContentManager _contentManager;

    public ContentService(IContentManager contentManager) => _contentManager = contentManager;

    public Task<ContentItem> GetContentItemOrThrowAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id), "The supplied content item ID was null or empty.");
        }

        var contentItem = GetContentItemOrThrowInternal(id);

        return GetContentItemOrThrowInternal(id);
    }

    public ContentItem ChangeDisplayText(ContentItem contentItem, string displayText)
    {
        contentItem.DisplayText = displayText;
        return contentItem;
    }

    private async Task<ContentItem> GetContentItemOrThrowInternal(string id) =>
        await _contentManager.GetAsync(id) ?? throw new InvalidOperationException($"The content item with the ID {id} doesn't exist.");

    // TODO: Add method to retrieve multiple content items.
}

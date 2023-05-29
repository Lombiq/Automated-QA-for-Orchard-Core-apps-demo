using OrchardCore.ContentManagement;
using System.Diagnostics.CodeAnalysis;

namespace OrchardCoreQA.Demo.Module.Services;

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Just a simple sample.")]
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

        return GetContentItemOrThrowInternalAsync(id);
    }

    private async Task<ContentItem> GetContentItemOrThrowInternalAsync(string id) =>
        await _contentManager.GetAsync(id) ?? throw new InvalidOperationException($"The content item with the ID {id} doesn't exist.");
}

using MediaHub.Contracts.Feeds;
using MediaHub.Contracts.Platforms;
using MediaHub.Contracts.Subscriptions;

namespace MediaHub.Api.Providers;

/// <summary>
/// v0 provider: Podcast episodes via RSS.
/// ExternalId should be the RSS URL.
/// </summary>
public sealed class PodcastRssProvider : IMediaProvider
{
    public PlatformKind Platform => PlatformKind.PodcastRss;

    public async Task<IReadOnlyList<MediaItemDto>> FetchAsync(SubscriptionDto subscription, CancellationToken ct)
    {
        // TODO: implement properly. This is scaffold-only.
        await Task.CompletedTask;
        return [];
    }
}

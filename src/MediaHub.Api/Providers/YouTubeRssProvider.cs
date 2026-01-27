using System.ServiceModel.Syndication;
using System.Xml;
using MediaHub.Contracts.Feeds;
using MediaHub.Contracts.Platforms;
using MediaHub.Contracts.Subscriptions;

namespace MediaHub.Api.Providers;

/// <summary>
/// v0 provider: YouTube channel uploads via RSS/Atom feed.
/// ExternalId can be a channel id or a full feed URL, depending on your choice.
/// </summary>
public sealed class YouTubeRssProvider : IMediaProvider
{
    public PlatformKind Platform => PlatformKind.YouTube;

    public async Task<IReadOnlyList<MediaItemDto>> FetchAsync(SubscriptionDto subscription, CancellationToken ct)
    {
        // TODO: implement properly. This is scaffold-only.
        // Suggested: use HttpClient + SyndicationFeed to parse Atom.
        await Task.CompletedTask;
        return [];
    }
}

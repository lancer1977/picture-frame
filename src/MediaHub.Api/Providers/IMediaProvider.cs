using MediaHub.Contracts.Feeds;
using MediaHub.Contracts.Platforms;
using MediaHub.Contracts.Subscriptions;

namespace MediaHub.Api.Providers;

public interface IMediaProvider
{
    PlatformKind Platform { get; }
    Task<IReadOnlyList<MediaItemDto>> FetchAsync(SubscriptionDto subscription, CancellationToken ct);
}

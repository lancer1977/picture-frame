using MediaHub.Contracts.Platforms;

namespace MediaHub.Contracts.Subscriptions;

public sealed record SubscriptionDto(
    string Id,
    string ChannelId,
    PlatformKind Platform,
    string ExternalId,
    string DisplayName,
    bool Enabled,
    int PollIntervalSeconds
);

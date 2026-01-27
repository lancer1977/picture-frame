using MediaHub.Contracts.Platforms;

namespace MediaHub.Contracts.Providers;

public sealed record ProviderFetchRequest(
    PlatformKind Platform,
    string ExternalId,
    string? Cursor,
    DateTimeOffset? SinceUtc
);

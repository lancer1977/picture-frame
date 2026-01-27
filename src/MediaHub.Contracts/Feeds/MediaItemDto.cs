using MediaHub.Contracts.Platforms;

namespace MediaHub.Contracts.Feeds;

public sealed record MediaItemDto(
    string ProviderItemId,
    PlatformKind Platform,
    string SourceExternalId,
    string Title,
    string Url,
    string? ThumbnailUrl,
    string? CreatorName,
    DateTimeOffset PublishedAtUtc,
    int? DurationSeconds,
    string? Summary
);

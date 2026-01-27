using MediaHub.Contracts.Feeds;

namespace MediaHub.Contracts.Providers;

public sealed record ProviderFetchResult(
    IReadOnlyList<MediaItemDto> Items,
    string? NextCursor
);

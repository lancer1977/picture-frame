using System.Collections.Concurrent;
using MediaHub.Contracts.Feeds;

namespace MediaHub.Api.Services;

public sealed class InMemoryMediaStore : IMediaStore
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, MediaItemDto>> _items = new();

    public void Upsert(string channelId, MediaItemDto item)
    {
        var dict = _items.GetOrAdd(channelId, _ => new ConcurrentDictionary<string, MediaItemDto>());
        dict[item.ProviderItemId] = item;
    }

    public IReadOnlyList<MediaItemDto> GetStreamFeed(string channelId, int limit)
    {
        if (!_items.TryGetValue(channelId, out var dict)) return [];
        return dict.Values
            .OrderByDescending(x => x.PublishedAtUtc)
            .Take(limit)
            .ToList();
    }
}

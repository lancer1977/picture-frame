using MediaHub.Contracts.Feeds;

namespace MediaHub.Api.Services;

public sealed class FileBackedMediaStore : IMediaStore
{
    private readonly object _gate = new();
    private readonly string _storagePath;
    private Dictionary<string, Dictionary<string, MediaItemDto>> _state;

    public FileBackedMediaStore(string storageDirectory)
    {
        _storagePath = Path.Combine(storageDirectory, "media-items.json");
        _state = LoadState();
    }

    public void Upsert(string channelId, MediaItemDto item)
    {
        lock (_gate)
        {
            GetChannel(channelId)[item.ProviderItemId] = item;
            Persist();
        }
    }

    public IReadOnlyList<MediaItemDto> GetStreamFeed(string channelId, int limit)
    {
        lock (_gate)
        {
            if (!_state.TryGetValue(channelId, out var channelItems))
                return [];

            return channelItems.Values
                .OrderByDescending(item => item.PublishedAtUtc)
                .ThenByDescending(item => item.ProviderItemId, StringComparer.Ordinal)
                .Take(limit)
                .ToList();
        }
    }

    private Dictionary<string, Dictionary<string, MediaItemDto>> LoadState()
    {
        var loaded = FileStorageUtilities.LoadOrCreate(
            _storagePath,
            () => new Dictionary<string, Dictionary<string, MediaItemDto>>(StringComparer.Ordinal));

        return Normalize(loaded);
    }

    private void Persist() => FileStorageUtilities.Save(_storagePath, _state);

    private Dictionary<string, MediaItemDto> GetChannel(string channelId)
    {
        if (!_state.TryGetValue(channelId, out var channelItems))
        {
            channelItems = new Dictionary<string, MediaItemDto>(StringComparer.Ordinal);
            _state[channelId] = channelItems;
        }

        return channelItems;
    }

    private static Dictionary<string, Dictionary<string, MediaItemDto>> Normalize(
        Dictionary<string, Dictionary<string, MediaItemDto>> loaded)
    {
        var normalized = new Dictionary<string, Dictionary<string, MediaItemDto>>(StringComparer.Ordinal);

        foreach (var (channelId, items) in loaded)
        {
            normalized[channelId] = new Dictionary<string, MediaItemDto>(items, StringComparer.Ordinal);
        }

        return normalized;
    }
}

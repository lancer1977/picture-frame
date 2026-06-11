using MediaHub.Contracts.Subscriptions;

namespace MediaHub.Api.Services;

public sealed class FileBackedSubscriptionService : ISubscriptionService
{
    private readonly object _gate = new();
    private readonly string _storagePath;
    private Dictionary<string, Dictionary<string, SubscriptionDto>> _state;

    public FileBackedSubscriptionService(string storageDirectory)
    {
        _storagePath = Path.Combine(storageDirectory, "subscriptions.json");
        _state = LoadState();
    }

    public IReadOnlyList<SubscriptionDto> List(string channelId)
    {
        lock (_gate)
        {
            return GetChannel(channelId).Values
                .OrderBy(subscription => subscription.DisplayName, StringComparer.OrdinalIgnoreCase)
                .ThenBy(subscription => subscription.Id, StringComparer.Ordinal)
                .ToList();
        }
    }

    public void Upsert(string channelId, SubscriptionDto dto)
    {
        lock (_gate)
        {
            GetChannel(channelId)[dto.Id] = dto;
            Persist();
        }
    }

    public void Remove(string channelId, string subscriptionId)
    {
        lock (_gate)
        {
            if (_state.TryGetValue(channelId, out var channelSubscriptions))
            {
                channelSubscriptions.Remove(subscriptionId);
                if (channelSubscriptions.Count == 0)
                    _state.Remove(channelId);
                Persist();
            }
        }
    }

    private Dictionary<string, Dictionary<string, SubscriptionDto>> LoadState()
    {
        var loaded = FileStorageUtilities.LoadOrCreate(
            _storagePath,
            () => new Dictionary<string, Dictionary<string, SubscriptionDto>>(StringComparer.Ordinal));

        return Normalize(loaded);
    }

    private void Persist() => FileStorageUtilities.Save(_storagePath, _state);

    private Dictionary<string, SubscriptionDto> GetChannel(string channelId)
    {
        if (!_state.TryGetValue(channelId, out var channelSubscriptions))
        {
            channelSubscriptions = new Dictionary<string, SubscriptionDto>(StringComparer.Ordinal);
            _state[channelId] = channelSubscriptions;
        }

        return channelSubscriptions;
    }

    private static Dictionary<string, Dictionary<string, SubscriptionDto>> Normalize(
        Dictionary<string, Dictionary<string, SubscriptionDto>> loaded)
    {
        var normalized = new Dictionary<string, Dictionary<string, SubscriptionDto>>(StringComparer.Ordinal);

        foreach (var (channelId, subscriptions) in loaded)
        {
            normalized[channelId] = new Dictionary<string, SubscriptionDto>(subscriptions, StringComparer.Ordinal);
        }

        return normalized;
    }
}

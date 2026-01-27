using System.Collections.Concurrent;
using MediaHub.Contracts.Subscriptions;

namespace MediaHub.Api.Services;

public sealed class InMemorySubscriptionService : ISubscriptionService
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, SubscriptionDto>> _byChannel = new();

    public IReadOnlyList<SubscriptionDto> List(string channelId)
        => _byChannel.TryGetValue(channelId, out var dict) ? dict.Values.ToList() : [];

    public void Upsert(string channelId, SubscriptionDto dto)
    {
        var dict = _byChannel.GetOrAdd(channelId, _ => new ConcurrentDictionary<string, SubscriptionDto>());
        dict[dto.Id] = dto;
    }

    public void Remove(string channelId, string subscriptionId)
    {
        if (_byChannel.TryGetValue(channelId, out var dict))
            dict.TryRemove(subscriptionId, out _);
    }
}

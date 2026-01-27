using MediaHub.Contracts.Subscriptions;

namespace MediaHub.Api.Services;

public interface ISubscriptionService
{
    IReadOnlyList<SubscriptionDto> List(string channelId);
    void Upsert(string channelId, SubscriptionDto dto);
    void Remove(string channelId, string subscriptionId);
}

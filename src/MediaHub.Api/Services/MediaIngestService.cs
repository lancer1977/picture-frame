using MediaHub.Api.Providers;

namespace MediaHub.Api.Services;

public sealed class MediaIngestService : IMediaIngestService
{
    private readonly ISubscriptionService _subs;
    private readonly IProviderRegistry _providers;
    private readonly IMediaStore _store;
    private readonly IClock _clock;

    public MediaIngestService(ISubscriptionService subs, IProviderRegistry providers, IMediaStore store, IClock clock)
    {
        _subs = subs;
        _providers = providers;
        _store = store;
        _clock = clock;
    }

    public async Task PollOnceAsync(CancellationToken ct)
    {
        // TODO: replace with real channel iteration from DB
        var channelIds = new[] { "demo-channel" };

        foreach (var channelId in channelIds)
        {
            var subs = _subs.List(channelId).Where(s => s.Enabled).ToList();
            foreach (var sub in subs)
            {
                var provider = _providers.Resolve(sub.Platform);
                if (provider is null) continue;

                var result = await provider.FetchAsync(sub, ct);

                foreach (var item in result)
                    _store.Upsert(channelId, item);
            }
        }
    }
}

using MediaHub.Contracts.Platforms;

namespace MediaHub.Api.Providers;

public sealed class ProviderRegistry : IProviderRegistry
{
    private readonly IReadOnlyDictionary<PlatformKind, IMediaProvider> _providers;

    public ProviderRegistry(IEnumerable<IMediaProvider> providers)
        => _providers = providers.ToDictionary(p => p.Platform, p => p);

    public IMediaProvider? Resolve(PlatformKind platform)
        => _providers.TryGetValue(platform, out var p) ? p : null;
}

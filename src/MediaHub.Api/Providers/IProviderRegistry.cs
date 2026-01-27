using MediaHub.Contracts.Platforms;

namespace MediaHub.Api.Providers;

public interface IProviderRegistry
{
    IMediaProvider? Resolve(PlatformKind platform);
}

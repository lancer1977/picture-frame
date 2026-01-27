using MediaHub.Contracts.Feeds;

namespace MediaHub.Api.Services;

public interface IMediaStore
{
    void Upsert(string channelId, MediaItemDto item);
    IReadOnlyList<MediaItemDto> GetStreamFeed(string channelId, int limit);
}

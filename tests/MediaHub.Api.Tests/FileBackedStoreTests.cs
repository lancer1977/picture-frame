using MediaHub.Api.Services;
using MediaHub.Contracts.Feeds;
using MediaHub.Contracts.Platforms;
using MediaHub.Contracts.Subscriptions;
using Xunit;

namespace MediaHub.Api.Tests;

public sealed class FileBackedStoreTests
{
    [Fact]
    public void SubscriptionService_persists_channel_state_between_instances()
    {
        using var scope = new TempStorageScope();

        var first = new FileBackedSubscriptionService(scope.Root);
        first.Upsert("channel-1", new SubscriptionDto("sub-1", "channel-1", PlatformKind.YouTube, "yt-1", "Alpha", true, 60));
        first.Upsert("channel-1", new SubscriptionDto("sub-2", "channel-1", PlatformKind.PodcastRss, "pod-1", "Beta", true, 120));

        var second = new FileBackedSubscriptionService(scope.Root);
        var items = second.List("channel-1");

        Assert.Collection(items,
            item => Assert.Equal("Alpha", item.DisplayName),
            item => Assert.Equal("Beta", item.DisplayName));

        second.Remove("channel-1", "sub-1");

        var third = new FileBackedSubscriptionService(scope.Root);
        var remaining = third.List("channel-1");

        Assert.Single(remaining);
        Assert.Equal("sub-2", remaining[0].Id);
    }

    [Fact]
    public void MediaStore_persists_and_sorts_stream_feed_between_instances()
    {
        using var scope = new TempStorageScope();

        var first = new FileBackedMediaStore(scope.Root);
        first.Upsert("channel-1", new MediaItemDto("item-1", PlatformKind.YouTube, "yt-1", "First", "https://example.com/1", null, null, DateTimeOffset.Parse("2026-06-10T10:00:00Z"), null, null));
        first.Upsert("channel-1", new MediaItemDto("item-2", PlatformKind.PodcastRss, "pod-1", "Second", "https://example.com/2", null, null, DateTimeOffset.Parse("2026-06-11T10:00:00Z"), null, null));
        first.Upsert("channel-1", new MediaItemDto("item-3", PlatformKind.PodcastRss, "pod-2", "Third", "https://example.com/3", null, null, DateTimeOffset.Parse("2026-06-09T10:00:00Z"), null, null));

        var second = new FileBackedMediaStore(scope.Root);
        var items = second.GetStreamFeed("channel-1", 2);

        Assert.Equal(2, items.Count);
        Assert.Equal("item-2", items[0].ProviderItemId);
        Assert.Equal("item-1", items[1].ProviderItemId);
    }

    private sealed class TempStorageScope : IDisposable
    {
        public string Root { get; } = Path.Combine(Path.GetTempPath(), "picture-frame-tests", Guid.NewGuid().ToString("N"));

        public TempStorageScope() => Directory.CreateDirectory(Root);

        public void Dispose()
        {
            if (Directory.Exists(Root))
                Directory.Delete(Root, recursive: true);
        }
    }
}

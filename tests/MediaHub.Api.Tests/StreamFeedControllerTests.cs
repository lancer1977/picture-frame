using MediaHub.Api.Controllers;
using MediaHub.Api.Services;
using MediaHub.Contracts.Feeds;
using MediaHub.Contracts.Platforms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MediaHub.Api.Tests;

public sealed class StreamFeedControllerTests
{
    [Fact]
    public void GetStreamFeed_emits_an_etag_and_returns_304_for_matching_if_none_match()
    {
        var store = new FakeMediaStore(new[]
        {
            new MediaItemDto(
                "item-1",
                PlatformKind.YouTube,
                "source-1",
                "First",
                "https://example.com/first",
                null,
                null,
                DateTimeOffset.Parse("2026-06-11T10:00:00Z"),
                null,
                null)
        });

        var controller = new StreamFeedController(store);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        var firstResult = Assert.IsType<OkObjectResult>(controller.GetStreamFeed("channel-1"));
        var etag = controller.Response.Headers.ETag.ToString();

        Assert.NotEmpty(etag);
        Assert.Single(Assert.IsAssignableFrom<IReadOnlyList<MediaItemDto>>(firstResult.Value));

        var secondController = new StreamFeedController(store);
        secondController.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        secondController.Request.Headers.IfNoneMatch = etag;

        var secondResult = Assert.IsType<StatusCodeResult>(secondController.GetStreamFeed("channel-1"));

        Assert.Equal(StatusCodes.Status304NotModified, secondResult.StatusCode);
        Assert.Equal(etag, secondController.Response.Headers.ETag.ToString());
    }

    private sealed class FakeMediaStore : IMediaStore
    {
        private readonly IReadOnlyList<MediaItemDto> _items;

        public FakeMediaStore(IReadOnlyList<MediaItemDto> items) => _items = items;

        public void Upsert(string channelId, MediaItemDto item)
        {
        }

        public IReadOnlyList<MediaItemDto> GetStreamFeed(string channelId, int limit) => _items.Take(limit).ToList();
    }
}

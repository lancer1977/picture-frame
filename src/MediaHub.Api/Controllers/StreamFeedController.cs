using MediaHub.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaHub.Api.Controllers;

[ApiController]
[Route("api/channels/{channelId}")]
public sealed class StreamFeedController : ControllerBase
{
    private readonly IMediaStore _store;

    public StreamFeedController(IMediaStore store) => _store = store;

    [HttpGet("stream-feed")]
    public IActionResult GetStreamFeed([FromRoute] string channelId, [FromQuery] int limit = 20)
    {
        // TODO: add ETag/If-None-Match
        var items = _store.GetStreamFeed(channelId, limit);
        return Ok(items);
    }
}

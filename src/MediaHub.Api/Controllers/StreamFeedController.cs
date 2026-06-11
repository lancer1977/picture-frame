using MediaHub.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

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
        var items = _store.GetStreamFeed(channelId, limit);
        var etag = ComputeEtag(items);

        Response.Headers.ETag = etag;
        if (Request.GetTypedHeaders().IfNoneMatch?.Any(tag => tag.Tag.ToString() == etag || tag.Tag.ToString() == "*") == true)
            return StatusCode(StatusCodes.Status304NotModified);

        return Ok(items);
    }

    private static string ComputeEtag<T>(IEnumerable<T> items)
    {
        var payload = JsonSerializer.Serialize(items);
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(payload));
        return $"\"{Convert.ToHexString(hash).ToLowerInvariant()}\"";
    }
}

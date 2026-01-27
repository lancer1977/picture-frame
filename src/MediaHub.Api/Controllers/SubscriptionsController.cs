using MediaHub.Api.Services;
using MediaHub.Contracts.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace MediaHub.Api.Controllers;

[ApiController]
[Route("api/channels/{channelId}/subscriptions")]
public sealed class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subs;

    public SubscriptionsController(ISubscriptionService subs) => _subs = subs;

    [HttpGet]
    public IActionResult List([FromRoute] string channelId) => Ok(_subs.List(channelId));

    [HttpPost]
    public IActionResult Add([FromRoute] string channelId, [FromBody] SubscriptionDto dto)
    {
        _subs.Upsert(channelId, dto);
        return Accepted();
    }

    [HttpDelete("{subscriptionId}")]
    public IActionResult Remove([FromRoute] string channelId, [FromRoute] string subscriptionId)
    {
        _subs.Remove(channelId, subscriptionId);
        return NoContent();
    }
}

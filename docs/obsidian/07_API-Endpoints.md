# API Endpoints (v1)

## Stream feed (read-only)
- `GET /api/channels/{channelId}/stream-feed?limit=20`
  - returns lightweight items + status
  - supports ETag

## Admin feed
- `GET /api/channels/{channelId}/feed?status=New|Saved|Featured`
- `POST /api/channels/{channelId}/curation/{mediaItemId}` (set status)

## Subscriptions
- `GET /api/channels/{channelId}/subscriptions`
- `POST /api/channels/{channelId}/subscriptions`
- `DELETE /api/channels/{channelId}/subscriptions/{subscriptionId}`

## Provider discovery (optional)
- `GET /api/providers` (list supported providers + config schema)

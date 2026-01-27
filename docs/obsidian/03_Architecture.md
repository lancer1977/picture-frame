# Architecture

## High-level
- **C# API service** owns persistence, polling orchestration, and per-channel state
- **Providers** implement platform-specific fetch logic and normalize to shared DTOs
- **Angular UI**
  - **Admin mode (main site):** manage subs + curate items
  - **Stream mode (app.channelcheevos.com):** read-only, browser-source friendly, polls minimal endpoints

## Key services
- SubscriptionService (per channel): add/remove/list subscriptions
- ProviderRegistry: resolves provider by platform + config
- MediaIngestService: polling job that pulls items and upserts into storage
- CurationService: per-channel decisions (hidden/featured/saved/watched)
- FeedService: provides pre-filtered feed outputs (admin feed, stream feed)

## Storage (v1)
- SQLite or Postgres
- Tables: Channels, Subscriptions, MediaItems, CurationStates, ProviderTokens (optional)

## Polling strategy
- Background service runs every N minutes
- Backoff per subscription/provider
- Cache last-seen timestamps/ETags where supported

## Browser source endpoint
- GET /api/channels/{channelId}/stream-feed
- Response optimized for frequent polling (ETag/If-None-Match, max-items, lightweight JSON)

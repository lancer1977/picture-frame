# Angular UI

## Two surfaces
### Main site (admin)
- Manage subscriptions
- Curate items (hide/save/feature)
- Filters and rules

### Dashboard / stream (browser source friendly)
- Read-only view
- Polls the stream-feed endpoint
- Renders a compact list or ticker strip
- Optional animation + “now playing” style transitions

## Components (starter set)
- `media-headlines-list` (compact list)
- `media-headlines-ticker` (horizontal strip / rolling)
- `media-item-card` (small thumbnail + title + meta)

## Services
- `MediaFeedClient` (poll stream feed + admin feed)
- `SubscriptionClient` (admin only)
- `CurationClient` (admin only)

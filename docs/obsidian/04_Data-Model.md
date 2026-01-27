# Data Model

## Core concept: ChannelFeature
Channels are the “real users” here: every subscription and curation choice is scoped to a channel.

### Channel (existing)
- Id
- Name / Handle
- Owner / Admins

### Subscription
- Id
- ChannelId
- Platform (YouTube|Podcast|Twitch|Web|…)
- ExternalId (channel id, rss url, etc.)
- DisplayName
- Enabled
- PollIntervalSeconds
- LastPolledAtUtc
- LastCursor (etag/page token/etc.)

### MediaItem
- Id (internal)
- ProviderItemId (external stable id)
- Platform
- SourceId (maps to subscription)
- Title
- Url
- ThumbnailUrl
- CreatorName
- PublishedAtUtc
- DurationSeconds (optional)
- Summary (optional)
- RawJson (optional for debugging)

### CurationState (per channel + item)
- ChannelId
- MediaItemId
- Status: New|Seen|Hidden|Saved|Featured|Watched
- Notes (optional)
- UpdatedAtUtc

## Normalized shape (DTO)
The UI should be able to render any item using:
- Title, ThumbnailUrl, Url
- Source/CreatorName
- PublishedAtUtc + DurationSeconds
- Status (curation)

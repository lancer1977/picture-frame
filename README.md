# Channel Cheevos - Media Hub Headlines (Scaffold)

This zip contains:
- `docs/obsidian/` : Obsidian-friendly project documentation + a Canvas map.
- `src/MediaHub.Contracts/` : shared DTOs + enums for platform providers and UI.
- `src/MediaHub.Api/` : C# API scaffold (Minimal API + Controllers + provider registry).
- `src/MediaHub.Angular/` : Angular folder scaffold (models/services/components).

## Tags

- picture-frame
- docs
- picture
- frame
- obs
- streaming

## Card-backed follow-up

- `picture-frame-persistent-storage.md` — persistent storage for subscriptions and media items
- `picture-frame-add-solution-file.md` — solution-file bootstrap for easier building
- `picture-frame-etag-stream-feed.md` — ETag support for `GET /api/channels/{channelId}/stream-feed`
- Solution entry point: `cd /home/lancer1977/code/picture-frame && dotnet build MediaHub.sln`
- OBS Browser Source wiring belongs to the downstream dashboard/consumer repo and is kept as integration context rather than duplicated here.


## 📖 Documentation
Detailed documentation can be found in the following sections:
- [Feature Index](./docs/features/README.md)
- [Core Capabilities](./docs/features/core-capabilities.md)

# Channel Cheevos - Media Hub Headlines (Scaffold)

This zip contains:
- `docs/obsidian/` : Obsidian-friendly project documentation + a Canvas map.
- `src/MediaHub.Contracts/` : shared DTOs + enums for platform providers and UI.
- `src/MediaHub.Api/` : C# API scaffold (Minimal API + Controllers + provider registry).
- `src/MediaHub.Angular/` : Angular folder scaffold (models/services/components).

## Suggested next steps
1. Decide storage (SQLite vs Postgres) and replace the in-memory services.
2. Implement YouTube RSS provider (Atom feed) + Podcast RSS provider.
3. Add ETag support to stream-feed endpoint.
4. Wire Angular component into your dashboard and test in an OBS Browser Source.

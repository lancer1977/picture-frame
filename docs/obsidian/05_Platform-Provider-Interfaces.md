# Platform Provider Interfaces

We want to support many platforms. The app should depend on a small set of interfaces and DTOs.

## Design principles
- Providers return *normalized MediaItem DTOs*
- Providers encapsulate auth, pagination, rate limits, and protocol details
- Providers can be implemented in:
  - the main API service
  - a plugin/service layer (e.g., Twitch plugin) that implements the same contract

## Provider responsibilities
- Validate subscription config
- Fetch new items (since cursor/time)
- Return next cursor + items
- Optionally return “source metadata” (creator name, icon, etc.)

## Extension approach
- Add a new provider by implementing `IMediaProvider`
- Register it via DI (ProviderRegistry)
- No changes required in UI except maybe new platform icon mapping

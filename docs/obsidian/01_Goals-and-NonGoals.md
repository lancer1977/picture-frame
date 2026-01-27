# Goals and Non-goals

## Goals
- Unified media item model across platforms (YouTube, podcasts, Twitch, etc.)
- Per-channel configuration (subscriptions, filters, display rules)
- Stream-friendly output endpoints (simple polling, cache headers)
- Extensible provider architecture (add a new platform without refactoring the app)
- Avoid scraping when possible; prefer RSS or official APIs

## Non-goals (for v1)
- Full recommendation engine
- Full-text search across transcripts
- Deep analytics / watch-time tracking
- Multi-replica write concurrency across providers (keep it simple first)

## Success criteria
- A channel can subscribe to 10–50 sources and see new items reliably
- The browser-source UI can run for hours without memory leaks or “refresh storms”
- Provider implementations can be swapped/added with minimal code changes

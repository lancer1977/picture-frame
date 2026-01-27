# Media Hub Headlines

A lightweight, streamer-friendly **media consolidation** feature for Channel Cheevos.

It collects *headlines + tiny thumbnails + source + age + duration* from multiple platforms (YouTube, podcasts, Twitch, etc.) and exposes them through a stable internal API so we can render:

- a **management UI** (edit/remove/curate choices) on the main site
- a **browser-source friendly “headline strip” UI** in the dashboard (app.channelcheevos.com)

## What it does
- Periodically polls platform subscriptions for **new media items**
- Normalizes items into a **common shape** (title, thumbnail, url, published time, duration, creator/source)
- Stores and serves **per-channel feeds** (channels are the “real users”)
- Allows channel owners/admins to:
  - add/remove subscriptions
  - choose what is displayed (hide/save/feature)
  - configure filters (e.g., “tech only”, “< 15 minutes”, “only favorites”)

## What it wants to achieve
- Reduce app switching (YouTube + podcasts + “tech news” everywhere)
- Make “what to watch/listen to next” decision *fast*
- Provide a clean streamer-safe UI for OBS/browser sources

## Who it is for
- **Primary:** Streamers using Channel Cheevos who want a fast “tech/media headlines” view
- **Secondary:** Power users who follow many creators across platforms and want consolidation
- **Tertiary:** Future integrations (plugins/3rd-party) that can implement the provider interfaces

## How it can be used
- **Dashboard / stream mode:** small thumbnails + titles, continuously refreshed, perfect for an OBS Browser Source
- **Main site / admin mode:** manage subscriptions, curate items, mark watched, promote featured items
- **Future:** “now watching” overlays or channel point redeems that pick an item from the curated queue

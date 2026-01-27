export type PlatformKind = 'YouTube' | 'PodcastRss' | 'Twitch' | 'WebRss' | 'Unknown';

export type CurationStatus = 'New' | 'Seen' | 'Hidden' | 'Saved' | 'Featured' | 'Watched';

export interface MediaItem {
  providerItemId: string;
  platform: PlatformKind;
  sourceExternalId: string;
  title: string;
  url: string;
  thumbnailUrl?: string;
  creatorName?: string;
  publishedAtUtc: string; // ISO
  durationSeconds?: number;
  summary?: string;
  status?: CurationStatus;
}

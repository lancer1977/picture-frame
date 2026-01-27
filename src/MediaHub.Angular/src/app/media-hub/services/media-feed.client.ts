import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, timer, switchMap, shareReplay } from 'rxjs';
import { MediaItem } from '../models/media-item.model';

@Injectable({ providedIn: 'root' })
export class MediaFeedClient {
  constructor(private http: HttpClient) {}

  /** Stream-friendly polling: call this from a browser-source component. */
  pollStreamFeed(channelId: string, intervalMs = 5000, limit = 20): Observable<MediaItem[]> {
    return timer(0, intervalMs).pipe(
      switchMap(() => this.http.get<MediaItem[]>(`/api/channels/${channelId}/stream-feed?limit=${limit}`)),
      shareReplay({ bufferSize: 1, refCount: true })
    );
  }
}

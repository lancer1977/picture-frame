import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { MediaFeedClient } from '../../services/media-feed.client';
import { MediaItem } from '../../models/media-item.model';

@Component({
  selector: 'cc-media-headlines-list',
  templateUrl: './media-headlines-list.component.html',
  styleUrls: ['./media-headlines-list.component.css']
})
export class MediaHeadlinesListComponent implements OnInit, OnDestroy {
  @Input() channelId!: string;
  @Input() pollMs = 5000;
  @Input() limit = 12;

  items: MediaItem[] = [];
  private sub?: Subscription;

  constructor(private feed: MediaFeedClient) {}

  ngOnInit(): void {
    this.sub = this.feed.pollStreamFeed(this.channelId, this.pollMs, this.limit)
      .subscribe(items => this.items = items ?? []);
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  trackById(_: number, item: MediaItem) {
    return item.providerItemId;
  }
}

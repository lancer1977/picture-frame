namespace MediaHub.Api.Services;

public sealed class MediaPollingWorker : BackgroundService
{
    private readonly IMediaIngestService _ingest;

    public MediaPollingWorker(IMediaIngestService ingest) => _ingest = ingest;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Simple loop; replace with smarter per-subscription scheduling later.
        while (!stoppingToken.IsCancellationRequested)
        {
            await _ingest.PollOnceAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}

namespace MediaHub.Api.Services;

public interface IMediaIngestService
{
    Task PollOnceAsync(CancellationToken ct);
}

using MediaHub.Api.Providers;
using MediaHub.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Core services
builder.Services.AddSingleton<IClock, SystemClock>();
builder.Services.AddSingleton<IProviderRegistry, ProviderRegistry>();
builder.Services.AddSingleton<ISubscriptionService, InMemorySubscriptionService>();
builder.Services.AddSingleton<IMediaStore, InMemoryMediaStore>();
builder.Services.AddSingleton<IMediaIngestService, MediaIngestService>();
builder.Services.AddHostedService<MediaPollingWorker>();

// Providers (add more via DI)
builder.Services.AddSingleton<IMediaProvider, YouTubeRssProvider>();
builder.Services.AddSingleton<IMediaProvider, PodcastRssProvider>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

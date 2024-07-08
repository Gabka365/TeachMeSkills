using PortalAboutEverything.VideoLibrary.Data.Context;
using PortalAboutEverything.VideoLibrary.Data.Extensions;
using PortalAboutEverything.VideoLibrary.FilesystemWatcher.Extensions;
using PortalAboutEverything.VideoLibrary.FilesystemWatcher.Helper;
using PortalAboutEverything.VideoLibrary.FilesystemWatcher.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRabbitMqServices();
builder.Services.AddRepositories();
builder.Services.AddSingleton<FilesystemHelper>();
builder.Services.AddHostedService<WatcherService>();

builder.Services.AddDbContext<VideoLibraryDbContext>();

var app = builder.Build();

var rabbitService = app.Services.GetRequiredService<RabbitMqService>();
await rabbitService.StartListening();

app.Run();
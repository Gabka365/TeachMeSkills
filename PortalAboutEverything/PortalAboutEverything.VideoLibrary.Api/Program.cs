﻿using Microsoft.Extensions.FileProviders;
using PortalAboutEverything.VideoLibrary.Api.Extensions;
using PortalAboutEverything.VideoLibrary.Api.Services;
using PortalAboutEverything.VideoLibrary.Data.Context;
using PortalAboutEverything.VideoLibrary.Data.Extensions;
using PortalAboutEverything.VideoLibrary.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRabbitMqServices();
builder.Services.AddRepositories();

builder.Services.AddDbContext<VideoLibraryDbContext>();

var app = builder.Build();

var rabbitService = app.Services.GetRequiredService<RabbitMqService>();
await rabbitService.StartListening();

var cacheMaxAgeOneWeek = (60 * 60 * 24 * 7).ToString();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(SharedVariables.ThumbnailsFolder),
    RequestPath = "/thumbnails",
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
            "Cache-Control", $"public, max-age={cacheMaxAgeOneWeek}");
    }
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(SharedVariables.UnsortedVideosFolder),
    RequestPath = "/videos"
});

app.MapControllers();

app.Run();
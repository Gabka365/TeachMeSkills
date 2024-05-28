using Microsoft.Extensions.DependencyInjection;
using PortalAboutEverything.Data.Repositories.VideoLibrary;
using PortalAboutEverything.VideoServices.Services;

namespace PortalAboutEverything.VideoServices.Extensions;

public static class VideoServicesExtension
{
    public static void AddVideoLibraryServices(this IServiceCollection services)
    {
        services.AddScoped<VideoRepository>();
        services.AddScoped<FolderRepository>();
        services.AddSingleton<VideoProcessQueueService>();
        services.AddScoped<VideoFileSystemService>();
        services.AddHostedService<FfmpegService>();
    }
}
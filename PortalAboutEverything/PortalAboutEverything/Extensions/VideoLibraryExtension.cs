using PortalAboutEverything.Data.Repositories;
using PortalAboutEverything.VideoServices.Services;

namespace PortalAboutEverything.Extensions;

public static class VideoLibraryExtension
{
    public static IServiceCollection AddVideoLibrary(this IServiceCollection services)
    {
        services.AddScoped<VideoLibraryRepository>();
        services.AddScoped<VideoService>();
        services.AddScoped<FfMpegService>();
        
        return services;
    }
}
using PortalAboutEverything.VideoLibrary.FilesystemWatcher.Services;
using PortalAboutEverything.VideoLibrary.RabbitMqBase.Services;

namespace PortalAboutEverything.VideoLibrary.FilesystemWatcher.Extensions;

public static class RabbitMqExtension
{
    public static void AddRabbitMqServices(this IServiceCollection services)
    {
        services.AddSingleton<RabbitMqCore>();
        services.AddSingleton<RabbitMqService>();
    }
}
using PortalAboutEverything.VideoLibrary.RabbitMqBase.Services;
using PortalAboutEverything.VideoLibrary.VideoProcessor.Services;

namespace PortalAboutEverything.VideoLibrary.VideoProcessor.Extensions;

public static class RabbitMqExtension
{
    public static void AddRabbitMqServices(this IServiceCollection services)
    {
        services.AddSingleton<RabbitMqCore>();
        services.AddSingleton<RabbitMqService>();
    }
}
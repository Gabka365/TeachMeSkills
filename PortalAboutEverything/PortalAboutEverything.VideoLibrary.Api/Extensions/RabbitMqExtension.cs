using PortalAboutEverything.VideoLibrary.Api.Services;
using PortalAboutEverything.VideoLibrary.RabbitMqBase.Services;

namespace PortalAboutEverything.VideoLibrary.Api.Extensions;

public static class RabbitMqExtension
{
    public static void AddRabbitMqServices(this IServiceCollection services)
    {
        services.AddSingleton<RabbitMqCore>();
        services.AddSingleton<RabbitMqService>();
    }
}
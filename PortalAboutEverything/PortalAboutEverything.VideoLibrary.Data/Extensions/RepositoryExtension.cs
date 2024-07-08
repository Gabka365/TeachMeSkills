﻿using Microsoft.Extensions.DependencyInjection;
using PortalAboutEverything.VideoLibrary.Data.Repository;

namespace PortalAboutEverything.VideoLibrary.Data.Extensions;

public static class RepositoryExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<FolderRepository>();
        services.AddScoped<VideoRepository>();
        services.AddScoped<VideoProcessRepository>();
    }
}
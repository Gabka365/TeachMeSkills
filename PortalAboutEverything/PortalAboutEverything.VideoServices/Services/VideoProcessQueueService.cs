using System.Collections.Concurrent;
using PortalAboutEverything.VideoServices.Models;

namespace PortalAboutEverything.VideoServices.Services;

public class VideoProcessQueueService
{
    public ConcurrentQueue<VideoProcessInfo> VideoToProcessQueue { get; } = new();

    public void EnqueueMany(List<VideoProcessInfo> videos)
    {
        foreach (var video in videos.Where(video =>
                     VideoToProcessQueue.All(videoInfo => videoInfo.VideoPath != video.VideoPath)))
        {
            VideoToProcessQueue.Enqueue(video);
        }
    }
}
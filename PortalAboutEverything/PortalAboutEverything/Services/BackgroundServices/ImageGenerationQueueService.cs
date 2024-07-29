namespace PortalAboutEverything.Services.BackgroundServices
{
    public class ImageGenerationQueueService
    {
        public Queue<string> DesciptonInQueue { get; set; } = new Queue<string>();
    }
}

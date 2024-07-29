
using System.Net.Mail;

namespace PortalAboutEverything.Services.BackgroundServices
{
    public class ImageGenerator : BackgroundService
    {
        private ImageGenerationQueueService _imageGenerationQueueService;

        public ImageGenerator(ImageGenerationQueueService imageGenerationQueueService)
        {
            _imageGenerationQueueService = imageGenerationQueueService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(GenerateImage);
        }

        private async Task GenerateImage()
        {
            while (true)
            {
                Console.WriteLine("ready to work");

                if (_imageGenerationQueueService.DesciptonInQueue.Any())
                {
                    var desc = _imageGenerationQueueService.DesciptonInQueue.Dequeue();
                    Console.WriteLine($"Generate image by desc: {desc}");
                }

                await Task.Delay(1000);
            }
        }
    }
}

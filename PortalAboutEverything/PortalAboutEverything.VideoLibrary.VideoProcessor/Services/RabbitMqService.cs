using System.Text;
using Newtonsoft.Json;
using PortalAboutEverything.VideoLibrary.RabbitMqBase;
using PortalAboutEverything.VideoLibrary.RabbitMqBase.Helpers;
using PortalAboutEverything.VideoLibrary.RabbitMqBase.Services;
using PortalAboutEverything.VideoLibrary.RabbitMqBase.Variables;
using PortalAboutEverything.VideoLibrary.Shared.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using IModel = RabbitMQ.Client.IModel;

namespace PortalAboutEverything.VideoLibrary.VideoProcessor.Services;

public class RabbitMqService(RabbitMqCore rabbitMqCore, ProcessorService processorService)
{
    private IModel? _channel;

    public async Task StartListening()
    {
        _channel = rabbitMqCore.GetChannel();
        var queue = rabbitMqCore.VideoProcessorQueue;

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += MessageReceived;

        _channel.BasicConsume(queue, RabbitVariables.RABBIT_MQ_AUTO_ACK, consumer);
    }

    private async void MessageReceived(object? sender, BasicDeliverEventArgs args)
    {
        var videoProcessInfo =
            JsonConvert.DeserializeObject<VideoProcess>(Encoding.UTF8.GetString(args.Body.ToArray()));

        if (videoProcessInfo is null)
        {
            return;
        }

        var response = await processorService.StartThumbnailGeneration(videoProcessInfo);

        if (response.isSuccess)
        {
            SendVideoProcessCompletedMessage(response.videoProcessInfo);
            _channel?.BasicAck(args.DeliveryTag, false);
        }
    }

    private void SendVideoProcessCompletedMessage(VideoProcess videoProcessInfo)
    {
        rabbitMqCore.GetChannel().BasicPublish(RabbitVariables.RABBIT_MQ_EXCHANGE_NAME,
            RabbitMqRoutingKeys.COMPLETE_VIDEO, body: RabbitMqHelper.GetBytesFromMessage(videoProcessInfo));
    }
}
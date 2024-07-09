using System.Text;
using Newtonsoft.Json;

namespace PortalAboutEverything.VideoLibrary.RabbitMqBase.Helpers;

public static class RabbitMqHelper
{
    public static byte[] GetBytesFromMessage(object message)
    {
        return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
    }
}
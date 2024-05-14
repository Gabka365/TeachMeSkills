using System.ComponentModel;
using System.Reflection;

namespace PortalAboutEverything.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T source)
            where T : Enum
        {
            var fi = source.GetType().GetField(source.ToString());

            var descriptionAttribute = fi.GetCustomAttribute<DescriptionAttribute>();

            if (descriptionAttribute == null)
            {
                return source.ToString();
            }

            return descriptionAttribute.Description;
        }
    }
}

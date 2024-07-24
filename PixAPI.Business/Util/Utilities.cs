using System.ComponentModel;
using System.Reflection;

namespace PixAPI.Business.Utils
{
    public static class Utilities
    {
        public static string GetEnumDescription(this Enum value)
        {
            return value.GetType()
                        .GetField(value.ToString())
                        .GetCustomAttribute<DescriptionAttribute>()?
                        .Description ?? value.ToString();
        }
    }
}

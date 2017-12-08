using EstudoArchictetureDDDVS2015Util.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoArchictetureDDDVS2015Util.Helpers.Definitions
{
    public static class DefinitionHelper
    {
        public static IEnumerable<KeyValuePair<string, string>> GetDictionaryFromEnum<T>()
          where T : IComparable, IFormattable, IConvertible
        {
            return Enum
                .GetValues(typeof(T))
                .Cast<Enum>()
                .Select(s => new KeyValuePair<string, string>(s.ToString(), s.ToDescriptionString()))
                .ToList();
        }

        public static IEnumerable<KeyValuePair<string, string>> GetDictionaryFromStringEnum<TType>() where TType : StringEnum
        {
            return StringEnum.GetValues<TType>()
                .ToDictionary(k => k.ToString(), v => v.ToDescriptionString());
        }
                
        public static DescriptionAttribute GetDescription(this Enum value)
        {
            return (DescriptionAttribute)Attribute
                .GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute));
        }

        public static string ToDescriptionString(this Enum value)
        {
            var description = value.GetDescription();

            return description != null ? description.Description : value.ToString();
        }
    }
}

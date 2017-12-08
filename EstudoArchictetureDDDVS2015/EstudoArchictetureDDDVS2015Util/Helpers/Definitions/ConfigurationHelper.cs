using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EstudoArchictetureDDDVS2015Util.Helpers.Definitions
{
    internal static class ConfigurationHelper
    {
        public static TimeSpan GetTimeSpan(this NameValueCollection collection, string name)
        {
            return TimeSpan.Parse(collection[name]);
        }

        public static int GetInt32(this NameValueCollection collection, string name)
        {
            return int.Parse(collection[name]);
        }

        public static string GetString(this NameValueCollection collection, string name)
        {
            return collection[name];
        }

        public static string GetUrl(this NameValueCollection collection, string name)
        {
            var url = collection[name];

            if (string.IsNullOrEmpty(url)) return url;

            while (url.Last() == '\\' || url.Last() == '/')
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        public static IPAddress GetIPAddress(this NameValueCollection collection, string name)
        {
            var text = collection[name];

            return string.IsNullOrWhiteSpace(text)
                ? null
                : IPAddress.Parse(text);
        }
    }
}

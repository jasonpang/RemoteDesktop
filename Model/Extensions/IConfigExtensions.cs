using System;
using System.Net;
using Nini.Config;

namespace Model.Extensions
{
    public static class IConfigExtensions
    {
        public static IPEndPoint GetIPEndPoint(this IConfig iConfig, string key)
        {
            return GetIPEndPoint(iConfig, key, null);
        }

        public static IPEndPoint GetIPEndPoint(this IConfig iConfig, string key, string defaultValue)
        {
            string ipEndPointString = iConfig.GetString(key, defaultValue);
            var ipEndPointStringParts = ipEndPointString.Split(':');

            IPEndPoint ipEndPoint = null;

            try
            {
                ipEndPoint = new IPEndPoint(IPAddress.Parse(ipEndPointStringParts[0]), Int32.Parse(ipEndPointStringParts[1]));
            }
            catch
            {
                ipEndPoint = GetIPEndPointFromHostName((ipEndPointStringParts[0]), Int32.Parse(ipEndPointStringParts[1]));
            }
            return ipEndPoint;
        }

        public static IPEndPoint GetIPEndPointFromHostName(string hostName, int port)
        {
            var addresses = System.Net.Dns.GetHostAddresses(hostName);
            if (addresses.Length == 0)
            {
                throw new ArgumentException(
                    "Unable to retrieve address from specified host name.",
                    "hostName"
                );
            }
            else if (false && addresses.Length > 1)
            {
                throw new ArgumentException(
                    "There is more that one IP address to the specified host.",
                    "hostName"
                );
            }
            return new IPEndPoint(addresses[0], port); // Port gets validated here.
        }

    }
}
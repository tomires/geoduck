using UnityEngine;

namespace Geoduck
{
    public static class Constants
    {
        public const string xmlRootNamespace = "http://www.topografix.com/GPX/1/0";
        public const string xmlNamespace = "http://www.groundspeak.com/cache/1/0/1";

        public static readonly string logbook = $"{Application.persistentDataPath}/logbook.json";
        public static readonly string cacheDirectory = $"{Application.persistentDataPath}/caches";
        public static readonly string tempDirectory = $"{Application.persistentDataPath}/temp";
        public static readonly string logFile = $"{Application.persistentDataPath}/log";
        public static string CacheFile(string gcCode) => string.Format(cacheDirectory + "/{0}.gpx", gcCode);

        public static class CacheTypes
        {
            public const string traditional = "Traditional Cache";
            public const string mystery = "Unknown Cache";
            public const string multi = "Multi-cache";
        }

        public static readonly float locationUpdateFrequency = 1f;
        public static readonly float cacheDetailsExpandedHeight = 500f;
    }
}

using UnityEngine;

public static class Constants
{
    public const string xmlRootNamespace = "http://www.topografix.com/GPX/1/0";
    public const string xmlNamespace = "http://www.groundspeak.com/cache/1/0/1";

    public static readonly string logbook = $"{Application.persistentDataPath}/logbook.json";
    public static readonly string cacheDirectory = $"{Application.persistentDataPath}/caches";
    public static readonly string tempDirectory = $"{Application.persistentDataPath}/temp";
    public static string CacheFile(string gcCode) => string.Format(cacheDirectory + "/{0}.gpx", gcCode);
}

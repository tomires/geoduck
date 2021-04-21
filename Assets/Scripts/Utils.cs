using Mapbox.Utils;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Geoduck
{
    public static class Utils
    {
        public static GpxStructure LoadGpxByPath(string path)
        {
            var serializer = new XmlSerializer(typeof(GpxStructure));
            var reader = new StreamReader(path);
            var gpx = serializer.Deserialize(reader) as GpxStructure;
            reader.Close();
            return gpx;
        }

        public static GpxStructure LoadGpxByGcCode(string gcCode)
        {
            return LoadGpxByPath(Constants.CacheFile(gcCode));
        }

        public static float GetDistance(Vector2d coords1, Vector2d coords2)
        {
            var lat1 = (float)coords1.x;
            var lon1 = (float)coords1.y;
            var lat2 = (float)coords2.x;
            var lon2 = (float)coords2.y;

            var R = 6371000f; /* Earth circumference */
            var f1 = lat1 * Mathf.PI / 180f;
            var f2 = lat2 * Mathf.PI / 180f;
            var df = (lat2 - lat1) * Mathf.PI / 180f;
            var dl = (lon2 - lon1) * Mathf.PI / 180f;
            var a = Mathf.Sin(df / 2f) * Mathf.Sin(df / 2f) +
                    Mathf.Cos(f1) * Mathf.Cos(f2) *
                    Mathf.Sin(dl / 2f) * Mathf.Sin(dl / 2f);
            var c = 2f * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1f - a));
            var distance = R * c;
            return distance;
        }

        public static string GetHumanReadableDistance(Vector2d coords1, Vector2d coords2)
        {
            var distance = GetDistance(coords1, coords2);
            if (distance > 10000f) /* -> 12 km */
                return $"{(int)(distance / 1000f)}km";
            else if (distance > 1000f) /* -> 5.2km */
                return $"{(distance / 1000f).ToString("0.0")}km";
            else /* -> 500m */
                return $"{(int)distance}m";
        }
    }
}

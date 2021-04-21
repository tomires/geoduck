using System.IO;
using System.Xml.Serialization;

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
    }
}

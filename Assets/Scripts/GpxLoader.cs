using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Geoduck
{
    public static class GpxLoader
    {
        private static string ZipFiletype => NativeFilePicker.ConvertExtensionToFileType("zip");
        private static string GpxFiletype => NativeFilePicker.ConvertExtensionToFileType("gpx");

        public static void LoadGpx()
        {
            NativeFilePicker.PickFile(ProcessFile, 
                new string[] { GpxFiletype, ZipFiletype });
        }

        private static void ProcessFile(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            string ext = path.Split('.').Last();
            bool isArchive = ext == "zip";

            if(isArchive)
            {
                Directory.CreateDirectory(Constants.tempDirectory);
                ZipUtil.Unzip(path, Constants.tempDirectory);
                Directory.GetFiles(Constants.tempDirectory).ToList().ForEach(ProcessFile);
                Directory.Delete(Constants.tempDirectory, true);
                return;
            }

            if (ext != "gpx") return;
            var cacheCode = GetCacheCode(path);
            File.Copy(path, Constants.CacheFile(cacheCode), true);
        }

        private static string GetCacheCode(string path)
        {
            var serializer = new XmlSerializer(typeof(GpxStructure));
            var reader = new StreamReader(path);
            var gpx = serializer.Deserialize(reader) as GpxStructure;
            reader.Close();
            return gpx.wpt.code;
        }
    }
}

using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.IO.Compression;

namespace Geoduck
{
    public static class GpxImporter
    {
        private static string ZipFiletype => NativeFilePicker.ConvertExtensionToFileType("zip");
        private static string GpxFiletype => NativeFilePicker.ConvertExtensionToFileType("gpx");
        private static int _filesLeftToProcess = 0;

        public static void LoadGpx()
        {
            _filesLeftToProcess = 1;
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
                ZipFile.ExtractToDirectory(path, Constants.tempDirectory);
                var files = Directory.GetFiles(Constants.tempDirectory).ToList();
                _filesLeftToProcess = files.Count;
                files.ForEach(ProcessFile);
                Directory.Delete(Constants.tempDirectory, true);
                return;
            }

            if (ext != "gpx") return;
            var cacheCode = GetCacheCode(path);
            File.Copy(path, Constants.CacheFile(cacheCode), true);

            _filesLeftToProcess--;
            if (_filesLeftToProcess == 0)
                PinSpawner.Instance.RefreshPins();
        }

        private static string GetCacheCode(string path)
        {
            var gpx = Utils.LoadGpxByPath(path);
            return gpx.wpt.code;
        }
    }
}

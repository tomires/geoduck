using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Geoduck
{
    public static class LogHistory
    {
        public static void SetLogStatus(GpxStructure gpx, LogStatus status)
        {
            var logs = LoadLogs();
            logs.RemoveAll(log => log.code == gpx.wpt.code);
            logs.Add(new UserLog(gpx.wpt.code, status));
            SaveLogs(logs);
        }

        public static LogStatus GetLogStatus(GpxStructure gpx)
        {
            foreach (var log in LoadLogs())
                if (log.code == gpx.wpt.code)
                    return log.status;
            return LogStatus.None;
        }

        private static List<UserLog> LoadLogs()
        {
            if (!File.Exists(Constants.logFile))
                SaveLogs(new List<UserLog>());

            using (var reader = new StreamReader(Constants.logFile))
                return JsonUtility.FromJson<UserLogContainer>(reader.ReadToEnd()).logs;
        }

        private static void SaveLogs(List<UserLog> logs)
        {
            var logsContainer = new UserLogContainer();
            logsContainer.logs = logs;
            var json = JsonUtility.ToJson(logsContainer);
            using (var writer = new StreamWriter(Constants.logFile))
                writer.Write(json);
        }
    }
}

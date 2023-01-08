using System;
using System.Collections.Generic;

namespace Geoduck
{
    [Serializable]
    public class UserLog
    {
        public string code;
        public LogStatus status;

        public UserLog(string code, LogStatus status)
        {
            this.code = code;
            this.status = status;
        }
    }

    [Serializable]
    public class UserLogContainer
    {
        public List<UserLog> logs;
    }
}

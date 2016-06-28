using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupService
{
    public enum BackupCondition
    {
        AlwaysSchedulePeriod,
        AlwaysAtTheCertainTime,
        SchedulePeriodIfChanged,
        AtTheCertainTimeIfChanged
    }

    public enum BackupMethod
    {
        Archive,
        Copy
    }

    public struct Config
    {
        public string           backupFromPath;
        public string           backupToPath;

        public BackupCondition  backupCondition;
        public BackupMethod     backupMethod;

        public TimeSpan         schedulePeriod;
        public DateTime         certainTime;

        public bool             usePassword;
        public string           password;
    }
}

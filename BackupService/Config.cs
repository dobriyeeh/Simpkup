using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupService
{
    public struct Config
    {
        public string BackupFrom { get; set; }

        public string BackupTo { get; set; }

        public TimeSpan ScheduleTime { get; set; }

        // true - archive, false - copy
        public bool ArchiveData { get; set; }

        public bool UsePassword { get; set; }

        public string Password { get; set; }
    }
}

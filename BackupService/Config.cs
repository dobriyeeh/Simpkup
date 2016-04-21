using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupService
{
    public struct Config
    {
        public string BackupPath { get; set; }

        public TimeSpan ScheduleTime { get; set; }

        public bool UsePassword { get; set; }

        public string Password { get; set; }
    }
}

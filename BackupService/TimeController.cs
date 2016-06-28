using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BackupService
{
    public class TimeController
    {
        private Config    _config;
        private IBackuper _backuper;

        private Timer     _timer;

        public TimeController(Config config, IBackuper backuper)
        {
            _config   = config;
            _backuper = backuper;

            _timer    = new Timer(_ => Task.Run(() => _backuper.Update()));
        }

        public void Start()
        {
            TimeSpan dueTime   = _config.schedulePeriod;
            TimeSpan duePeriod = _config.schedulePeriod;

            switch (_config.backupCondition)
            {
                case BackupCondition.AlwaysAtTheCertainTime:
                case BackupCondition.AtTheCertainTimeIfChanged:
                    duePeriod   = _config.schedulePeriod;
                    dueTime     = _config.schedulePeriod;
                    break;
            }

            _timer.Change(dueTime, duePeriod);
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupService
{
    public class BackupRule : IBackupRule
    {
        private readonly DataPlace  _dataPlace;
        private readonly Config     _config;

        private DirectoryIdentity   _initalIdentity;

        public bool IsTimeOverdue    => (DateTime.Now - _initalIdentity.SnapshotTime) > _config.ScheduleTime;

        public bool AreFilesChanged  => !_initalIdentity.Equals(_dataPlace.ActualIdentity);

        public bool IsItTimeToBackup => IsTimeOverdue || AreFilesChanged;
        
        public BackupRule(DataPlace dataPlace, Config config)
        {
            _dataPlace  = dataPlace;
            _config     = config;

            Reset();
        }

        public void Reset()
        {
            _initalIdentity = _dataPlace.ActualIdentity;
        }
    }
}

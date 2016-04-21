using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupService
{
    public class BackupRule : IBackupRule
    {
        private DirectoryIdentity _directoryIdentity;

        private readonly DataPlace _dataPlace;
        private readonly Config _config;

        public bool IsTimeOverdue { get; }

        public bool AreFilesChanged { get; }

        public bool IsItTimeToBackup => IsTimeOverdue || AreFilesChanged;
        
        public BackupRule(DataPlace dataPlace, Config config)
        {
            _dataPlace = dataPlace;
            _config = config;
        }

    }
}

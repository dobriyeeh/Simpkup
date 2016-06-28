using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupService
{
    public class Backuper : IBackuper
    {
        private readonly IBackupAction _backupAction;
        private readonly IBackupRule   _backupRule;

        public Backuper(IBackupAction backupAction, IBackupRule backupRule)
        {
            _backupAction = backupAction;
            _backupRule = backupRule;
        }

        public void Update()
        {
            if (_backupRule.IsItTimeToBackup)
            {
                _backupAction.Backup();
                //TODO update of time checking
                //TODO add specific time
                //_backupRule.re
            }
        }

    }
}

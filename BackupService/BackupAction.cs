using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupService
{
    class BackupAction : IBackupAction
    {
        private readonly DataPlace _dataPlace;
        private readonly Config _config;

        public BackupAction(DataPlace dataPlace, Config config)
        {
            _dataPlace = dataPlace;
            _config = config;
        }

        public void Backup()
        {
            var dataAction = new DataAction(_dataPlace);

            switch (_config.backupMethod)
            {
                case BackupMethod.Archive:
                    dataAction.ArchiveTo(_config.backupFromPath);
                    break;

                case BackupMethod.Copy:
                    dataAction.CopyTo(_config.backupFromPath);
                    break;
            }
        }
    }
}

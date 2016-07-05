using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupService
{
    public class BackupAction : IBackupAction
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
                    if (_config.usePassword)
                        dataAction.ArchiveTo(_config.backupToPath, _config.password);
                    else
                        dataAction.ArchiveTo(_config.backupToPath);
                    break;

                case BackupMethod.Copy:
                    dataAction.CopyTo(_config.backupFromPath);
                    break;
            }
        }
    }
}

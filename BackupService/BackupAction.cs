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
            _dataPlace.ArchiveTo(_config.BackupPath);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupService
{
    public class InformationKeeper
    {
        private readonly Config _config;
        private DataPlace       _dataPlace;
        private TimeController  _timeController;
        private Backuper        _backuper;

        public InformationKeeper(Config config)
        {
            _config             = config;

            _dataPlace          = new DataPlace(_config.backupFromPath);
            var backupAction    = new BackupAction(_dataPlace, _config);
            var backupRule      = new BackupRule(_dataPlace, _config);

            _backuper           = new Backuper(backupAction, backupRule);

            _timeController     = new TimeController(_config, _backuper);
        }

        public void Start()
        {
            _timeController.Start();
        }

        public void Stop()
        {
            _timeController.Stop();
        }
    }
}

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

        private DirectoryIdentity   _pastIdentity;

        private bool AreFilesChanged()
        {
            var  actualIdentity = _dataPlace.ActualIdentity;
            bool result         = !_pastIdentity.Equals(_dataPlace.ActualIdentity);
            _pastIdentity       = actualIdentity;

            return result;
        }

        public bool IsItTimeToBackup
        {
            get
            {
                switch (_config.backupCondition)
                {
                    case BackupCondition.AlwaysSchedulePeriod:
                    case BackupCondition.AlwaysAtTheCertainTime:
                        return true;

                    case BackupCondition.SchedulePeriodIfChanged:
                    case BackupCondition.AtTheCertainTimeIfChanged:
                        return AreFilesChanged();
                }

                throw new Exception("condition is missed");
            }
        }
        
        public BackupRule(DataPlace dataPlace, Config config)
        {
            _dataPlace              = dataPlace;
            _config                 = config;

            _pastIdentity           = _dataPlace.ActualIdentity;
        }
    }
}

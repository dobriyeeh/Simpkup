using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using Moq;
using BackupService;
using System.IO;

namespace BackuperTest
{
    [TestFixture]
    class BackuperAndDataKeeperFixture
    {
        private TestedData  _testedData;
        private DataPlace   _dataPlace;
        private Config      _config;

        private const int    DefaultWait = 500;
        private const int    MoreTime    = (int)(DefaultWait * 1.25);

        [SetUp]
        public void Setup()
        {
            _testedData         = new TestedData();
            _dataPlace          = new DataPlace(_testedData.DirPath);
            _config             = new Config()
            {
                backupFromPath  = _testedData.DirPath,
                backupToPath    = _testedData.DirDestination,
                backupCondition = BackupCondition.SchedulePeriodIfChanged,
                backupMethod    = BackupMethod.Archive,
                schedulePeriod  = TimeSpan.FromMilliseconds(DefaultWait),
            };
        }

        [Test]
        public void TestRules()
        {
            var backupRule = new BackupRule(_dataPlace, _config);
            var actionMock = new Mock<IBackupAction>();
            
            var backuper = new Backuper(actionMock.Object, backupRule);

            _testedData.ChangeFile();
            Thread.Sleep(MoreTime);
            backuper.Update();

            Thread.Sleep(MoreTime);
            backuper.Update();

            actionMock.Verify(act => act.Backup(), Times.Once);
        }

        [Test]
        public void TestEncryptedArchives()
        {
            var newConfig           = _config;
            newConfig.usePassword   = true;
            newConfig.password      = "qwerty";
            newConfig.backupToPath += "\\test.zip";


            var backupRule          = new BackupRule(_dataPlace, newConfig);
            var backupAction        = new BackupAction(_dataPlace, newConfig);

            var backuper            = new Backuper(backupAction, backupRule);

            backupAction.Backup();

            var verificator         = new DataVerificator(_testedData);
            Assert.Catch<InvalidDataException>(() => verificator.testArchive(newConfig.backupToPath));
            Assert.IsTrue(verificator.testEncryptedArchive(newConfig.backupToPath, newConfig.password));
        }


        public void TestDataKeeper()
        {
            var keeper = new InformationKeeper(_config);
            keeper.Start();
            Thread.Sleep(MoreTime);

            keeper.Stop();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using Moq;
using BackupService;

namespace BackuperTest
{
    [TestFixture]
    class BackuperAndDataKeeperFixture
    {
        private TestedData  _testedData;
        private DataPlace   _dataPlace;
        private Config      _config;

        [SetUp]
        public void Setup()
        {
            _testedData         = new TestedData();
            _dataPlace          = new DataPlace(_testedData.DirPath);
            _config             = new Config()
            {
                BackupFrom      = _testedData.DirPath,
                BackupTo        = _testedData.DirDestination,
                ArchiveData     = true,
                ScheduleTime    = TimeSpan.FromSeconds(1),
                UsePassword     = false,
                Password        = null
            };
        }

        [Test]
        public void TestSimpleRules()
        {
            var backupRule = new BackupRule(_dataPlace, _config);
            _testedData.ChangeFile();

            Assert.IsTrue(backupRule.AreFilesChanged);
            Assert.IsFalse(backupRule.IsTimeOverdue);
            Thread.Sleep(1000);
            Assert.IsTrue(backupRule.IsTimeOverdue);
        }


        [Test]
        public void TestRules()
        {
            var backupRule = new BackupRule(_dataPlace, _config);
            var actionMock = new Mock<IBackupAction>();
            
            var backuper = new Backuper(actionMock.Object, backupRule);

            _testedData.ChangeFile();

            backuper.Update();

            actionMock.Verify(act => act.Backup());
        }

        public void TestDataKeeper()
        {
            var keeper = new InformationKeeper(_config);
            keeper.Start();
            //Thread.Sleep()
            keeper.Stop();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using BackupService;
using NUnit.Framework;
using Moq;

namespace BackuperTest
{
    [TestFixture]
    class TimeControllerFixture
    {
        [Test]
        public void TestTimeContoroller()
        {
            var testData        = new TestedData();

            var config          = new Config()
            {
                schedulePeriod  = TimeSpan.FromMilliseconds(500),
                backupCondition = BackupCondition.SchedulePeriodIfChanged
            };

            var dataPlace       = new DataPlace(testData.DirPath);
            var backupRule      = new BackupRule(dataPlace, config);
            var backuperMock    = new Mock<IBackuper>();

            var timeController  = new TimeController(config, backuperMock.Object);
            timeController.Start();

            Thread.Sleep(1250);

            timeController.Stop();

            backuperMock.Verify(backuper => backuper.Update(), Times.Exactly(2));
        }
    }
}

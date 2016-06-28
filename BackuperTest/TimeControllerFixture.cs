﻿using System;
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
        public void TestCalculationTimeToEvent()
        {
            var reactionTime1 = new ReactionTime
            {
                hour   = 5,
                minute = 25
            };

            var reactionTime2 = new ReactionTime
            {
                hour   = 15,
                minute = -1
            };

            var leftToEvent1 = reactionTime1.TimeLeftBeforeEvent(new DateTime(2016, 1, 1, 6, 30, 0));
            Assert.AreEqual(leftToEvent1, new TimeSpan(1, 5, 0));
            
            var leftToEvent2 = reactionTime1.TimeLeftBeforeEvent(new DateTime(2016, 1, 1, 6, 0, 0));
            Assert.AreEqual(leftToEvent2, new TimeSpan(0, 35, 0));

            var leftToEvent3 = reactionTime1.TimeLeftBeforeEvent(new DateTime(2016, 1, 1, 4, 25, 0));
            var leftToEvent4 = reactionTime1.TimeLeftBeforeEvent(new DateTime(2016, 1, 1, 4, 25, 0));
            var leftToEvent5 = reactionTime2.TimeLeftBeforeEvent(new DateTime(2016, 1, 1, 4, 25, 0));
            var leftToEvent6 = reactionTime2.TimeLeftBeforeEvent(new DateTime(2016, 1, 1, 16, 25, 0));

        }

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

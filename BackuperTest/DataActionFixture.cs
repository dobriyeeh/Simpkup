using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;
using BackupService;
using Moq;

namespace BackuperTest
{
    [TestFixture]
    class DataActionFixture
    {
        private TestedData  _testedData;
        private DataPlace   _dataPlace;

        public string TestDirPath => _testedData.DirPath;

        [SetUp]
        public void Setup()
        {
            _testedData = new TestedData();
            _dataPlace  = new DataPlace(TestDirPath);
        }

        [Test]
        public void PositiveTestArchive()
        {
            var    dataAction   = new DataAction(_dataPlace);
            string archivePath  = _testedData.DirDestination + "\\mytest.zip";
            dataAction.ArchiveTo(archivePath);

            var    verificator  = new DataVerificator(_testedData);
            Assert.IsTrue(verificator.testArchive(archivePath));
        }

        [Test]
        public void NegativeTestArchive()
        {
            var    dataAction  = new DataAction(_dataPlace);
            string archivePath = "Q:\\UNBELIEVABLE\\PATH";
            Assert.Catch<IOException>(() => dataAction.ArchiveTo(archivePath));
        }

        [Test]
        public void PositiveTestCopy()
        {
            var               dataAction          = new DataAction(_dataPlace);
            string            copyPath            = _testedData.DirDestination + "\\TestCopy1";
            dataAction.CopyTo(copyPath);

            DirectoryIdentity sourceIdentity      = new DirectoryIdentity(_dataPlace.Path);
            DirectoryIdentity destinationIdentity = new DirectoryIdentity(copyPath);

            Assert.AreEqual(sourceIdentity, destinationIdentity);
        }

        [Test]
        public void NegativeTestCopy()
        {
            var    dataAction = new DataAction(_dataPlace);
            string copyPath   = "Q:\\UNBELIEVABLE\\PATH";
            Assert.Catch<IOException>(() => dataAction.CopyTo(copyPath));
        }

        //[Test]
        public void PerformanceArchive()
        {
            var dataAction = new DataAction(@"C:\!Projects\MBClub\Backuper\BackuperTest\testdata");
            dataAction.ArchiveTo(_testedData.DirDestination + "\\PerformanceTest.zip");
        }

    }
}

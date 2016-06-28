using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BackupService;
using NUnit.Framework;
using System.IO.Compression;
using System.Security.Cryptography;

namespace BackuperTest
{
    [TestFixture]
    public class IdentityFixture
    {
        private TestedData _testedData;

        public string TestDirPath => _testedData.DirPath;

        [SetUp]
        public void PrepareFiles()
        {
            _testedData = new TestedData();
        }


        [Test]
        public void TestPreparing()
        {
            string[] filesInDir = Directory.GetFiles(TestDirPath, "*", SearchOption.AllDirectories);
            Assert.AreEqual(filesInDir.Length, 3);
        }

        [Test]
        public void TestIdentity()
        {
            var identity1 = new DirectoryIdentity(TestDirPath);
            var identity2 = new DirectoryIdentity(TestDirPath);

            Assert.AreEqual(identity1, identity2);

            File.WriteAllText(TestDirPath + "\\Muqla.tmp", "B Test content");

            var newIdentity = new DirectoryIdentity(TestDirPath);
            Assert.AreNotEqual(identity1, newIdentity);
        }

        [Test]
        public void PerformanceIdentity()
        {
            var newIdentity = new DirectoryIdentity(_testedData.PerformanceDataPath);
        }
    }
}

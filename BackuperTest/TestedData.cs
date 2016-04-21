using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BackupService;
using NUnit.Framework;

namespace BackuperTest
{
    class TestedData
    {
        public TestedData()
        {
            PrepareFiles();
        }

        public string DirPath
        {
            get
            {
                string path = TestContext.CurrentContext.Test.Name;
                string testDir = TestContext.CurrentContext.TestDirectory + "\\" + path;
                return testDir;
            }
        }

        public string DirDestination => TestContext.CurrentContext.TestDirectory + "\\Destination";

        private string[] PrepareFiles(string dirName)
        {
            var files = new List<string>();

            string rootDir = TestContext.CurrentContext.TestDirectory;

            string newDir = rootDir + "\\" + dirName;
            Directory.CreateDirectory(newDir);

            files.Add(newDir + "\\A.txt");
            File.WriteAllText(files.Last(), "A Test content");
            files.Add(newDir + "\\B.txt");
            File.WriteAllText(files.Last(), "B Test content");

            Directory.CreateDirectory(newDir + "\\TestDir");

            files.Add(newDir + "\\TestDir\\C.txt");
            File.WriteAllText(files.Last(), "C Test content");

            return files.ToArray();
        }

        private void PrepareFiles()
        {
            if (Directory.Exists(DirPath))
                Directory.Delete(DirPath, true);

            if (Directory.Exists(DirDestination))
                Directory.Delete(DirDestination, true);

            Directory.CreateDirectory(DirDestination);

            var files = PrepareFiles(TestContext.CurrentContext.Test.Name);
        }

        public void Clean()
        {
            if (Directory.Exists(DirPath))
                Directory.Delete(DirPath, true);

            if (Directory.Exists(DirDestination))
                Directory.Delete(DirDestination, true);
        }
    }
}

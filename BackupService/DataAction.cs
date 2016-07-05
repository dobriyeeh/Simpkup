using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using DotZip = Ionic.Zip;

namespace BackupService
{
    public class DataAction
    {
        public DataPlace Data { get; }

        public DataAction(DataPlace data)
        {
            Data = data;
        }

        public DataAction(string dataPath)
        {
            Data = new DataPlace(dataPath);
        }


        public void ArchiveTo(string pathTo)
        {
            ArchiveTo(pathTo, null);
        }

        public void ArchiveTo(string pathTo, string password)
        {
            using (var zip = new DotZip.ZipFile())
            {
                if (!string.IsNullOrEmpty(password))
                {
                    zip.Password   = password;
                    zip.Encryption = DotZip.EncryptionAlgorithm.WinZipAes256;
                }

                string rootDir = new DirectoryInfo(Data.Path).Name;

                var files = Directory.GetFiles(Data.Path, "*", SearchOption.AllDirectories);
                foreach (var currFile in files)
                {
                    string currDir = Path.GetDirectoryName(currFile);
                    string relativePath = currDir.Length > Data.Path.Length ? currDir.Substring(Data.Path.Length + 1) : "";
                    if (string.IsNullOrEmpty(relativePath))
                        relativePath = rootDir;
                    else
                        relativePath = rootDir + "\\" + relativePath;

                    zip.AddFile(currFile, relativePath);
                }

                zip.Save(pathTo);
            }
        }

        public void CopyTo(string pathTo)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(Data.Path, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(Data.Path, pathTo));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(Data.Path, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(Data.Path, pathTo), true);
        }
    }
}

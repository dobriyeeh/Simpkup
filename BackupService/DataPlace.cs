using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace BackupService
{
    public class DataPlace
    {
        public DataPlace(string dirPath)
        {
            Path = dirPath;
        }

        public string Path {  get;  }

        public DirectoryIdentity ActualIdentity => new DirectoryIdentity(Path);

        public void ArchiveTo(string pathTo)
        {
            ZipFile.CreateFromDirectory(Path, pathTo, CompressionLevel.Optimal, true);
        }

    }
}

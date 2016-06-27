using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace BackupService
{
    public class DirectoryIdentity
    {
        public DirectoryIdentity(string dirPath)
        {
            SnapshotTime = DateTime.Now;
            Identity = HashUtils.CalculateDirectoryMd5(dirPath);
        }

        public string Identity { get; } 

        public DateTime SnapshotTime { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is DirectoryIdentity))
                return false;

            return Identity.Equals(((DirectoryIdentity)obj).Identity);
        }

        public override int GetHashCode()
        {
            return Identity.GetHashCode();
        }

    }
}

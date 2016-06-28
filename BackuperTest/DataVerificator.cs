using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using BackupService;
using NUnit.Framework;

namespace BackuperTest
{
    // class for verification
    class DataVerificator
    {
        private readonly TestedData _testedData;

        public DataVerificator(TestedData testedData)
        {
            _testedData = testedData;
        }

        public bool testArchive(string path)
        {
            if (!File.Exists(path))
                return false;

            string extractPath = Path.GetTempPath() + Guid.NewGuid().ToString();
            bool   result      = false;

            try
            {
                ZipFile.ExtractToDirectory(path, extractPath);
                var subfolders = Directory.GetDirectories(extractPath);
                if (subfolders.Length != 1)
                    return false;
                
                var originalIdentity   = new DirectoryIdentity(_testedData.DirPath).Identity;
                var extractionIdentity = new DirectoryIdentity(subfolders.First()).Identity;
                result                 = originalIdentity.Equals(extractionIdentity);
            }
            finally
            {
                Directory.Delete(extractPath, true);
            }

            return true;
        }
    }
}

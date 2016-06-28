using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

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
            ZipFile.CreateFromDirectory(Data.Path, pathTo, CompressionLevel.Optimal, true);
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

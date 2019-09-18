using System.Collections.Generic;
using System.Linq;
using FileManager.Models;
using System.IO;
using FileManager.Assets;

namespace FileManager
{
    public class BackupManager
    {
        public void CreateBackup(string path, string backupFolder)
        {
            var structure = FileHelper.GetStructure(path);
            FileHelper.CopyFolder(structure.Folders.FirstOrDefault(), backupFolder, path);
        }
    }
}

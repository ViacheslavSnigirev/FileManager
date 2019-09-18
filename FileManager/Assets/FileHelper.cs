using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager;
using FileManager.Models;
using System.IO;

namespace FileManager.Assets
{
    public static class FileHelper
    {
        public static void CopyFolder(Folder folder, string backupFolder, string path)
        {
            var pathToCopy = folder.FolderPath.Substring(folder.FolderPath.IndexOf(path) + path.Length);
            Directory.CreateDirectory(string.Join("\\", backupFolder, pathToCopy));
            folder.Files.ForEach(f => CopyFile(f, backupFolder, path));
            folder.Folders.ForEach(f => CopyFolder(f, backupFolder, path));
        }

        private static void CopyFile(SingleFile file, string backupFolder, string path)
        {
            var pathToCopy = file.FilePath.Substring(file.FilePath.IndexOf(path) + path.Length);
            var fileInfo = new FileInfo(file.FilePath);

            fileInfo.CopyTo(string.Join("\\", backupFolder, pathToCopy));
        }

        public static FolderStructure GetStructure(string path)
        {
            var mainFolder = new List<Folder>();
            mainFolder.Add(GetFolder(path));

            return new FolderStructure()
            {
                Folders = mainFolder
            };
        }

        private static Folder GetFolder(string path)
        {
            var childFolders = new List<Folder>();
            childFolders.AddRange(Directory.GetDirectories(path).Select(p => GetFolder(p)));

            var childFiles = new List<SingleFile>();
            childFiles.AddRange(Directory.GetFiles(path).Select(p => GetFile(p)));

            return new Folder()
            {
                Folders = childFolders,
                Files = childFiles,
                FolderPath = path
            };
        }

        private static SingleFile GetFile(string filePath)
        {
            return new SingleFile()
            {
                FilePath = filePath
            };
        }

        private static IEnumerable<string> GetFileNames(Folder folder)
        {
            return folder.Files.Select(x => x.FilePath.Substring(folder.FolderPath.Length));
        }

        private static void ReplaceFiles(List<Folder> folders, string gamePath)
        {
            foreach (var folder in folders)
            {
                var listOfChangingFIles = GetFileNames(folder).Select(x => Directory.GetFiles(gamePath).Select(y => y.Substring(Directory.GetCurrentDirectory().Length)).Contains(x));
            }
        }
    }
}

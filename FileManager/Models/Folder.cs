using System.Collections.Generic;

namespace FileManager.Models
{
    public class Folder
    {
        public string FolderPath { get; set; }

        public List<SingleFile> Files { get; set; }

        public List<Folder> Folders { get; set; }
    }
}

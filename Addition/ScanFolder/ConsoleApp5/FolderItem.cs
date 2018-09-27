using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp5
{
    class FolderItem
    {
        public StringCollection files = new StringCollection();

        
        // уровень вложенности 
        public int level { get; set; }
        private string tab;
        private string dirTab;
        public string path { get; set; }
        private string name;
        public string FormatName{ get { return (this.level > 0) ? dirTab + this.name : this.path; } }
        public FolderItem[] SubFolders
        {
            get
            {
                string[] SubFolderNames = Directory.GetDirectories(this.path);
                List<FolderItem> Folders = new List<FolderItem>();
                foreach (var fname in SubFolderNames)
                {
                    if (HasReadAccess(fname))
                        Folders.Add(new FolderItem(level + 1, fname));
                }
                return Folders.ToArray();
            }
        }

        public FolderItem(int level, string path)
        {
            this.path = path;
            this.level = level;
            this.tab = new String('.', level+1);            
            this.dirTab =  (level > 0)? string.Concat(Enumerable.Repeat("..\\", level)) : "";
            this.name = new StringBuilder(path).Replace(Path.GetDirectoryName(path) + '\\', "").ToString();
        }
      
        public bool ScanFiles()
        {
            files.Clear();
            string[] fils;
            try
            {
                fils  = Directory.GetFiles(this.path);
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            //Console.WriteLine((this.level > 0)?dirTab+this.name:this.path);

            foreach (var fname in fils)
            {
                if (File.GetCreationTimeUtc(fname) < DateTime.UtcNow.AddDays(-15))
                {
                    //Console.WriteLine("{0} {1}", tab, Path.GetFileName(fname));
                    files.Add(String.Format("{0} {1}", tab, Path.GetFileName(fname)));                        
                }
            }
            return files.Count > 0;
        }

        private bool HasReadAccess(string aPath)
        {
            try
            {
                Directory.GetFiles(aPath);
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            return true;
        }

    }
    

}

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            string RootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Stack<FolderItem> WorkFolders = new Stack<FolderItem>();
            StringCollection buf = new StringCollection()
            {
                RootDirectory
            };


            FolderItem fl = new FolderItem(0,RootDirectory);
            WorkFolders.Push(fl);
            while(WorkFolders.Count != 0)
            {
                fl = WorkFolders.Pop();
                if (fl.ScanFiles())
                {                   
                    buf.Add(new StringBuilder(fl.path).Replace(Path.GetDirectoryName(RootDirectory), "").ToString());
                    buf.AddRange(fl.Files);
                }
                foreach(FolderItem f in fl.SubFolders)
                {
                    WorkFolders.Push(f);
                }                
            }
            foreach(var str in buf)
            {
                Console.WriteLine(str);
            }

            
            Console.ReadKey();
        }

    }
}

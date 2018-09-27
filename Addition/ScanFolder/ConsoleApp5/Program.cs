using System;
using System.IO.Compression;
using System.IO;
using System.Collections.Generic ;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ConsoleApp5
{
    class Program
    {
        static string StructDirFileName = "dirs.dat";
        static string CompressedFileName = Path.GetFileNameWithoutExtension(StructDirFileName) + ".gz";

        static void Main(string[] args)
        {
            
            string RootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Console.WriteLine($"Scaning directoory {RootDirectory}...");

            Stack<FolderItem> WorkFolders = new Stack<FolderItem>();
            
            using (FileStream reportFileName = new FileStream(StructDirFileName,FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(reportFileName))
                {
                    sw.WriteLine(RootDirectory);
                    FolderItem fl = new FolderItem(0, RootDirectory);
                    WorkFolders.Push(fl);
                    while (WorkFolders.Count != 0)
                    {
                        fl = WorkFolders.Pop();
                        if (fl.ScanFiles())
                        {
                            //buf.Add(fl.path.Replace(Path.GetDirectoryName(RootDirectory), ""));
                            //buf.AddRange(fl.Files);
                            sw.WriteLine(fl.path.Replace(Path.GetDirectoryName(RootDirectory), ""));
                            foreach (var fileName in fl.files)
                                sw.WriteLine(fileName);
                        }
                        foreach (FolderItem f in fl.SubFolders)
                        {
                            WorkFolders.Push(f);
                        }
                    }
                 
                }
            }
            CompressingFile(StructDirFileName, CompressedFileName);

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
        public static void CompressingFile(string src, string dst)
        {
            if (!File.Exists(src))
            {
                return;
            }

            using (FileStream sourceFile = new FileStream(src, FileMode.Open))
            {
                using (FileStream compressedFile = new FileStream(dst, FileMode.OpenOrCreate))
                {
                    using (GZipStream gs = new GZipStream(compressedFile, CompressionMode.Compress))
                    {
                        sourceFile.CopyTo(gs);
                    }
                }
            }
        }
        
        public static void MoveToDesktop(string  GZipFileName,string fileTo)
        {
            if (!File.Exists(GZipFileName))
                return;
            File.Move(GZipFileName, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + GZipFileName);                
        }
    }

}

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
            try
            {
                using (FileStream reportFileName = new FileStream(StructDirFileName, FileMode.OpenOrCreate))
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
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }

            
            CompressingFile(StructDirFileName, CompressedFileName);
            MoveToDesktop(CompressedFileName);
            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }

        #region Compress data from src file and save it into dst compressed file
        public static void CompressingFile(string src, string dst)        
        {
            if (!File.Exists(src))
            {
                return;
            }
            try
            {
                using (FileStream sourceFile = new FileStream(src, FileMode.Open))
                {
                    try
                    {
                        using (FileStream compressedFile = new FileStream(dst, FileMode.OpenOrCreate))
                        {
                            using (GZipStream gs = new GZipStream(compressedFile, CompressionMode.Compress))
                            {
                                sourceFile.CopyTo(gs);
                            }
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine($"Невозможно получить доступ к файлу {dst}, возможно он используется другтм процессом.");
                    }
                }                
            }
            catch(IOException e)
            {
                Console.WriteLine($"Невозможно получить доступ к файлу {src}, возможно он используется другтм процессом.");
            }
        }
        #endregion

        // Move Compressed file te desktop
        public static void MoveToDesktop(string  GZipFileName)
        {
            string dstFileName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + GZipFileName;
            if (!File.Exists(GZipFileName))
            {               
                return;
            }
            if (File.Exists(dstFileName))
            {
                Console.WriteLine($"Файл уже {GZipFileName} существует в папке {Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\"}");
                if (!ConfirmYN("Заменить ?"))
                    return;
            }

            try
            {
                File.Move(GZipFileName, dstFileName);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool ConfirmYN(string aMessage)
        {
            Console.WriteLine();
            Console.WriteLine($"{aMessage} [Y/N](N):");
            return Console.ReadKey().KeyChar.ToString().ToLower() == "y";            
        }

    }

}

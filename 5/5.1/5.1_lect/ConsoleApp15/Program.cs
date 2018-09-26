using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15
{
    class Program
    {
        static void Main(string[] args)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach(var drive in drives)
            {
                Console.WriteLine($"Name {drive.Name}");
                Console.WriteLine($"Type {drive.DriveType}");
                if (drive.IsReady)
                {
                    try
                    {
                        Console.WriteLine($"DriveFormat {drive.DriveFormat}");
                        Console.WriteLine($"TotalSize {drive.TotalSize}");
                        Console.WriteLine($"AvailableFreeSpace {drive.AvailableFreeSpace}");
                        Console.WriteLine($"TotalFreeSpace {drive.TotalFreeSpace}");
                        Console.WriteLine($"VolumeLabel {drive.VolumeLabel}");
                    }
                    catch (IOException) { }
                }
                Console.WriteLine($"=================================================");
            }
            Console.WriteLine($"=============    DIRECTORY    ===================");


            var directoryPath = "C:\\";
            if (Directory.Exists(directoryPath))
            {
                Console.WriteLine("Subcatalogs: " + directoryPath);
                string[] dirs = Directory.GetDirectories(directoryPath);
                foreach(var dir in dirs){
                    Console.WriteLine(dir);
                }
            }
            Console.WriteLine("=================================================");
            Console.WriteLine("=======   FILES      ============================");
            directoryPath = "D:\\";
            string[] files = Directory.GetFiles(directoryPath);
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
            Console.WriteLine("=================================================");
            Console.WriteLine("============   Create Directory =================");
            directoryPath = "D:\\Temppp";
            var myFolder = "Module5/Lecture1";
            DirectoryInfo di = new DirectoryInfo(directoryPath);
            if (!di.Exists)
            {
                di.Create();
            }
            di.CreateSubdirectory(myFolder);
            


            Console.WriteLine("============   Move folder      =================");
            var destFolder = directoryPath + '/' + myFolder;
            var srcFolder = directoryPath + '/' + "Module6";
            di = new DirectoryInfo(destFolder);
            if (di.Exists && !Directory.Exists(srcFolder))
            {
                di.MoveTo(srcFolder);
            }

            Console.WriteLine("============   Delete Folder    =================");
            Console.WriteLine("Написать самому                 =================");


            Console.WriteLine("=================================================");
            Console.WriteLine("============   Работа с файлами =================");

            // File
            // FileINfo
            Console.WriteLine("============   Create file      =================");
            var path = "D:\\Temppp/Text.txt";
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
            {
                fi.Create();
            }
            if (fi.Exists)
            {
                Console.WriteLine($"file name: {fi.Name}");
                Console.WriteLine($"CreationTime: {fi.CreationTime}");
                Console.WriteLine($"Length: {fi.Length}");
            }
            /*if (fi.Exists)
            {
                fi.Delete();
                //File.Delete(path);
            }*/

            var newPath = "D:\\Temppp/Module5/Text.txt";
            if (fi.Exists)
            {
                Console.WriteLine("Move to :" + newPath);
                if (!File.Exists(newPath))
                {
                    fi.MoveTo(newPath);
                    Console.WriteLine("File was moved");
                }
            }

            var newFile = "D:\\Temppp/TextCopy.txt";
            if (fi.Exists)
            {
                Console.WriteLine("Copy to :" + path);
                if (!File.Exists(newFile))
                    fi.CopyTo(newFile);
                Console.WriteLine("File was copied");
            }

            Console.WriteLine("=================================================");
            Console.WriteLine("============   Work witn stream   ===============");

            var input = Console.ReadLine();
            var filePath1 = "d:\\temppp/output.txt";            
            Console.WriteLine("============   WRITE              ===============");
            FileStream fs = new FileStream(filePath1, FileMode.OpenOrCreate);
            byte[] array = System.Text.Encoding.Default.GetBytes(input);
            fs.Write(array, 0, array.Length);
            fs.Close();

            Console.WriteLine("============   READ               ===============");
            fs = File.OpenRead(filePath1);
            array = new byte[fs.Length];
            fs.Read(array, 0, array.Length);
            var res = System.Text.Encoding.Default.GetString(array);
            Console.WriteLine(res);
            fs.Close();

            var filePath2 = "d:\\temppp/output_using.txt";
            Console.WriteLine("============   WRITE with USING   ===============");
            using (FileStream fs2 = new FileStream(filePath2, FileMode.OpenOrCreate))
            {
                array = System.Text.Encoding.Default.GetBytes(input);
                fs2.Write(array, 0, array.Length);
            }
            Console.WriteLine("============   READ with  USING   ===============");
            using (fs = File.OpenRead(filePath2))
            {                
                array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                res = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine(res);
            }

            Console.WriteLine("============  STREAM   read  ===============");
            input = "d:\\autorun.inf";
            using(StreamReader sr = new StreamReader(input))
            {
                Console.WriteLine(sr.ReadToEnd());
            }
            Console.WriteLine("============  STREAM write    ===============");
            using (StreamWriter sw = new StreamWriter("d:\\Temppp/autorun.inf", true, System.Text.Encoding.Default))
            {
                sw.WriteLine("New Line");
                sw.WriteLine(123);
            }
            Console.WriteLine("============  STREAM read line  =============");
            using (StreamReader sr = new StreamReader("d:\\Temppp / autorun.inf"))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

            Console.WriteLine("============  STREAM read char  =============");
            using (StreamReader sr2 = new StreamReader("d:\\Temppp / autorun.inf"))
            {
                charp[] array = new char[8];
            }


            Console.Write("Press  any key... ");
            Console.ReadKey();
        }
    }
}

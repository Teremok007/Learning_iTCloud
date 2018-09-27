using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Specialized;

namespace CSharp_Net_module1_7_1_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            // 3) create collection of computers;
            // set path to file and file name
            List<Computer> computers = new List<Computer>();
            computers.Add(new Computer() { Cores = 1, Frequency = 800, Hdd = 200, Memory = 2 });
            computers.Add(new Computer() { Cores = 2, Frequency = 1500, Hdd = 998, Memory = 8 });
            computers.Add(new Computer() { Cores = 4, Frequency = 1800, Hdd = 1000, Memory = 4 });
            computers.Add(new Computer() { Cores = 2, Frequency = 2000, Hdd = 512, Memory = 16 });
            computers.Add(new Computer() { Cores = 2, Frequency = 1500, Hdd = 256, Memory = 16 });
            computers.Add(new Computer() { Cores = 4, Frequency = 2900, Hdd = 700, Memory = 16 });
            computers.Add(new Computer() { Cores = 8, Frequency = 3200, Hdd = 2000, Memory = 64 });



            // 4) save data and read it in the seamplest way (with WriteData() and ReadData() methods)
            InOutOperation inouter = new InOutOperation() { CurrentFile = "StorageComputer.txt" };
            inouter.WriteData(computers);
            Computer[] cmps = inouter.ReadData();
            foreach(Computer item in cmps)
            {
                Console.WriteLine(item.ToString());
            }
            // 5) save data and read it with WriteZip() and ReadZip() methods
            // Note: create another file for these operations
            inouter.WriteZip();
            StringCollection compInfo = inouter.ReadZip();
            Console.WriteLine("======= Read from ZIP file ===================== ");
            foreach (var cinfo in compInfo)
            {
                Console.WriteLine(cinfo);
            }
            Console.WriteLine("================================================ ");
            // 6) read info about computers asynchronously (from the 1st file)

            // While asynchronous method will be running, Main() method must print ‘*’ 

            // use 
            // collection of Task with info about computers as type to get returned data from method ReadAsync()
            // use property Result of collection of Task to get access to info about computers

            // Note:
            // print all info about computers after reading from files


            // ADVANCED:
            // 8) save data to memory stream and from memory to file
            // declare file stream and set it to null
            // call method WriteToMemory() with info about computers as parameter
            // save returned stream to file stream

            // call method WriteToFileFromMemory() with parameter of file stream
            // open file with saved data and compare it with input info

            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.ReadKey();
        }
    }
}


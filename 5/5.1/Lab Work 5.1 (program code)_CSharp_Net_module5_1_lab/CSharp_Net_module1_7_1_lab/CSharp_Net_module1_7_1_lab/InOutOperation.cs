using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Collections.Specialized;
using System.Threading;

namespace CSharp_Net_module1_7_1_lab
{
    class InOutOperation
    {
        // 1) declare properties  CurrentPath - path to file (without name of file), CurrentDirectory - name of current directory,
        // CurrentFile - name of current file
        public string CurrentPath { get; set; }
        public string CurrentDirectory { get; set; }
        public string CurrentFile { get; set; }


        // 2) declare public methods:
        //ChangeLocation() - change of CurrentPath; 
        // method takes new file path as parameter, creates new directories (if it is necessary)

        public void ChangeLocation(string aNewFilePath)
        {
            CurrentPath = CreateDirectory(aNewFilePath);            
            Directory.SetCurrentDirectory(CurrentPath);
        }

        // CreateDirectory() - create new directory in current location
        public string CreateDirectory(string newDir)
        {
            if (!File.Exists(newDir))
            {
                try
                {
                    DirectoryInfo df = Directory.CreateDirectory(newDir);
                    return df.FullName;
                }
                catch (IOException)
                {
                    
                }                
            }
            return newDir;
        }
        
        // WriteData() – save data to file
        // method takes data (info about computers) as parameter
        public void WriteData(List<Computer> computers)
        {
            
            using (FileStream fs = new FileStream(CurrentFile, FileMode.OpenOrCreate))
            {
                BinaryWriter bw = new BinaryWriter(fs);
                foreach (Computer comp in computers)
                {
                    bw.Write(comp.Cores);
                    bw.Write(comp.Frequency);
                    bw.Write(comp.Memory);
                    bw.Write(comp.Hdd);
                }
            }
        }
        // ReadData() – read data from file
        // method returns info about computers after reading it from file
        public Computer[] ReadData()
        {
            List<Computer> computers = new List<Computer>();
            if (File.Exists(CurrentFile))
            {
                using (BinaryReader br = new BinaryReader(File.OpenRead(CurrentFile)))
                {
                    
                    while(br.PeekChar() != -1)
                    {
                        Computer computer = new Computer();
                        computer.Cores = br.ReadInt32();
                        computer.Frequency = br.ReadDouble();
                        computer.Memory = br.ReadInt32();
                        computer.Hdd = br.ReadInt32();
                        computers.Add(computer);
                    }
                }
            }
            return computers.ToArray();
        }
        // WriteZip() – save data to zip file
        // method takes data (info about computers) as parameter
        public void WriteZip()
        {
            string compressedFile = Path.GetFileNameWithoutExtension(CurrentFile) + ".gz";
            using (FileStream source_fs = File.OpenRead(CurrentFile))
            {
                using (FileStream compressed_fs = new FileStream(compressedFile, FileMode.OpenOrCreate))
                {
                    using (GZipStream compressionStream = new GZipStream(compressed_fs, CompressionMode.Compress))
                    {
                        source_fs.CopyTo(compressionStream);
                    }
                }

            }
        }
        // ReadZip() – read data from file
        // method returns info about computers after reading it from file
        public StringCollection ReadZip()
        {
            StringCollection computersInfo = new StringCollection();
            
            string CompressedFile = Path.GetFileNameWithoutExtension(CurrentFile) + ".gz";
            string TemporaryFile = Path.GetTempFileName();
            
            using (FileStream source_fs = File.OpenRead(CompressedFile))
            {
                using (FileStream tempFile = File.Create(TemporaryFile))
                {
                    using (GZipStream compressionStream = new GZipStream(source_fs, CompressionMode.Decompress))
                    {
                        compressionStream.CopyTo(tempFile);
                        tempFile.Flush();
                        tempFile.Seek(0, SeekOrigin.Begin);
                        using (BinaryReader br = new BinaryReader(tempFile))
                        {
                            while (br.PeekChar() != -1)
                            {
                                Computer computer = new Computer();
                                computer.Cores = br.ReadInt32();
                                computer.Frequency = br.ReadDouble();
                                computer.Memory = br.ReadInt32();
                                computer.Hdd = br.ReadInt32();
                                computersInfo.Add(computer.ToString());
                            }
                        }
                    }                    
                }
                if (File.Exists(TemporaryFile))
                {
                    File.Delete(TemporaryFile);
                }

            }
            return computersInfo;
        }
        // ReadAsync() – read data from file asynchronously
        // method is async
        // method returns Task with info about computers
        // use ReadLineAsync() method to read data from file
        // Note: don't forget about await

        // 7)
        // ADVANCED:
        // WriteToMemoryStream() – save data to memory stream
        // method takes data (info about computers) as parameter
        // info about computers is saved to Memory Stream

        // use  method GetBytes() from class UnicodeEncoding to save array of bytes from string data 
        // create new file stream
        // use method WriteTo() to save memory stream to file stream 
        // method returns file stream

        // WriteToFileFromMemoryStream() – save data to file from memory stream and read it
        // method takes file stream as parameter
        // save data to file      


        // Note: 
        // - use '//' in path string or @ before it (for correct path handling without escape sequence)
        // - use 'using' keyword to organize correct working with files
        // - don't forget to check path before any file or directory operations
        // - don't forget to check existed directory and file before creating
        // use File class to check files, Directory class to check directories, Path to check path

        public InOutOperation()
        {
            CurrentDirectory = Directory.GetCurrentDirectory();
        }
    }
}

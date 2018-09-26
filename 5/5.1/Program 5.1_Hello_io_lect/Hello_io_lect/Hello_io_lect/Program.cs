using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Hello_io_lect
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            try
            {
                do
                {
                    //do something
                    Console.WriteLine(@"Please,  type the number:                       
                        1.  FileStream, GZipStream                
                        2.  FileStream, Byte array
                        3.  StreamWriter
                        4.  StreamReader
                        5.  Binary Writer & Reader
                        6.  MemoryStream
                        7.  Async FileStreamb
                        ");
                    try
                    {
                        Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                        a = int.Parse(Console.ReadLine());
                        switch (a)
                        {
                            case 1:
                                Console.WriteLine("FileStream, GZipStream");
                                FlStream();
                                break;
                            case 2:
                                Console.WriteLine("FileStream, Byte array");
                                FlStream_BArr();
                                break;
                            case 3:
                                Console.WriteLine("StreamWriter");
                                StrWr();
                                break;
                            case 4:
                                Console.WriteLine("StreamReader");
                                StrRd();
                                break;
                            case 5:
                                Console.WriteLine("Binary Writer & Reader");
                               BnrWrRdr();
                                break;
                            case 6:
                                Console.WriteLine("MemoryStream");
                                MyMemory();
                                break;
                            case 7:
                                Console.WriteLine("Async FileStream");
                                Aswr();
                                break;
                            default:
                                Console.WriteLine("Exit");
                                break;
                        }

                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine("Error");
                    }
                    finally
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Press Spacebar to exit; press any key to continue");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                while (Console.ReadKey().Key != ConsoleKey.Spacebar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #region FlStream
        static void FlStream()
        {
            try
            {
                Zip_File("System.Web.Mvc.dll", "System.Web.Mvc.dll.gz");
                UnZip_File("System.Web.Mvc.dll.gz", "System.Web.Mvc.dll.tst");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
 
        }

        static void Zip_File(string in_Fl,
                             string out_Fl)
        {
            FileStream src = File.OpenRead(in_Fl);
            FileStream dst = File.Create(out_Fl);
            // Create compressed stream
            GZipStream Zip_Stream =
              new GZipStream(dst, CompressionMode.Compress);
            // Write  data
            int One_Byte = src.ReadByte();
            while (One_Byte != -1)
            {
                Zip_Stream.WriteByte((byte)One_Byte);
                One_Byte = src.ReadByte();
            }
            // Clear
            src.Close();
            Zip_Stream.Close();
            dst.Close();
        }

        static void UnZip_File(string in_Fl, string out_Fl)
        {
            FileStream src = File.OpenRead(in_Fl);
            FileStream dst = File.Create(out_Fl);
            // Create compressed stream
            GZipStream Zip_Stream =
              new GZipStream(src, CompressionMode.Decompress);
            // Write  data
            int One_Byte = Zip_Stream.ReadByte();
            while (One_Byte != -1)
            {
                dst.WriteByte((byte)One_Byte);
                One_Byte = Zip_Stream.ReadByte();
            }
            // Clear
            src.Close();
            Zip_Stream.Close();
            dst.Close();
        }
        #endregion

        #region FlStream
        static void FlStream_BArr()
        {
            Console.WriteLine("FileStreams - Byte Arrays");
            try
            {
                // Get a FileStream 
                using (FileStream Fl_Stream = File.Open("Hello_io_message.txt",
                  FileMode.Create))
                {
                    // Encode a string as an bytes array .
                    string msg = "Hello I/O!";
                    byte[] msg_BArr = Encoding.Default.GetBytes(msg);
                    // Write array to file
                    Fl_Stream.Write(msg_BArr, 0, msg_BArr.Length);
                    // Reset internal position of stream
                    Fl_Stream.Position = 0;
                    // Read the types from file and display to standart output
                    Console.Write("Array of bytes: ");
                    byte[] BArr_FromFile = new byte[msg_BArr.Length];
                    for (int i = 0; i < msg_BArr.Length; i++)
                    {
                        BArr_FromFile[i] = (byte)Fl_Stream.ReadByte();
                        Console.Write(BArr_FromFile[i]);
                    }
                    // Display decoded messages.
                    Console.WriteLine(" Decoded Message: ");
                    Console.WriteLine(Encoding.Default.GetString(BArr_FromFile));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();       
        }
        #endregion

        #region StreamWriter_Reader
        static void StrWr()
        {

	        using (StreamWriter Str_wr =
	            new StreamWriter("Hello_io_str_mess.txt"))
	        {
	            Str_wr.Write("Hello ");
	            Str_wr.WriteLine("_IO_");
	            Str_wr.WriteLine("StreamWriter");
	
            }
            // Append line to the file
            using (StreamWriter Str_wr1 =
                new StreamWriter("Hello_io_str_mess.txt", true))
            {
                Str_wr1.WriteLine(" !");
                Str_wr1.Close();
            }
            
        }

        static void StrRd()
        {
            try
            {
               using( FileStream fl_str1 = new FileStream("Hello_io_str_mess.txt", FileMode.Open) )
                {
                   using (StreamReader rdr = new StreamReader(fl_str1))
                   {
                    Console.WriteLine(rdr.ReadToEnd());
                    rdr.Close();
                   }
               }

               using (StreamReader fl_str2 = new StreamReader("Hello_io_str_mess.txt", System.Text.Encoding.Default))
               {
                   char[] array = new char[4];
                   // read 4 chars
                   fl_str2.Read(array, 0, 4);

                   Console.WriteLine(array);
               }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion

        #region Binary
        static void BnrWrRdr()
        {
            try
            {
                // Open a binary writer 
                FileInfo fl_inf = new FileInfo("Hello_io_binary.dat");
                using (BinaryWriter bwr = new BinaryWriter(fl_inf.OpenWrite()))
                {
                    Console.WriteLine("Base stream is: {0}", bwr.BaseStream);

                    double aDouble = 2015.6;
                    int anInt = 20156;
                    string aString = "C, s, h";

                    // Write 
                    bwr.Write(aDouble);
                    bwr.Write(anInt);
                    bwr.Write(aString);
                }

                Console.WriteLine("Ok!");

                // Read from the stream.
                using (BinaryReader brd = new BinaryReader(fl_inf.OpenRead()))
                {
                    Console.WriteLine(brd.ReadDouble());
                    Console.WriteLine(brd.ReadInt32());
                    Console.WriteLine(brd.ReadString());
                }

                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion

        #region Memory
        static void MyMemory()
        {
            List<byte[]> mylist = new List<byte[]>();
            for (int i = 0; true; i++)
            {
                try
                {
                    mylist.Add(new byte[i * i * 10]);
                }
                catch (OutOfMemoryException e)
                {
                    Console.Write(string.Format("MyList {0} ", i));
                }
            }
        }
        #endregion

        #region Async

        static void Aswr()
        {
            try
            {
                Wrt().Wait();
                Console.Write("Ok! ");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        static Task Wrt()
        {
            string filePath = "Hello_IO_async.txt";
            string text = "Hello _IO_async\r\n";

            return Wr_Async(filePath, text);
        }

        static async Task Wr_Async(string fl_pth, string txt)
        {
            byte[] en_txt = Encoding.Unicode.GetBytes(txt);

            using (FileStream src = new FileStream(fl_pth,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await src.WriteAsync(en_txt, 0, en_txt.Length);
            };
        }
        #endregion
    }
}

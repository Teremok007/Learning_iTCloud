using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                do
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(" Home Work from Sergey  Yeshchenko for Vitaliy ");
                    Console.WriteLine(" --------------------------------------------- ");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(@"Task:
      Опишите тип с массивом интов и релизуйте IEnumerable
      При проходе циклом, ваш класс должен возвращать только простые числа");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(" --------------------------------------------- ");
                    Console.WriteLine();

                    Console.WriteLine("Введите массив целых чисел разделенные пробелом:");

                    String buf = Console.ReadLine();
                    char[] separators = new char[] { ' ', ';' };

                    string[] StrNums = buf.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    int[] values = new int[StrNums.Length];
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        for (int i = 0; i < StrNums.Length; i++)
                            values[i] = Int32.Parse(StrNums[i]);
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Элемент имеет значение null");
                        continue;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Элемент массива имеет неправильный формат");
                        continue;
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Элемент меньше значения System.Int32.MinValue или больше");
                        continue;
                    }
                    finally
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }

                    IntEnumerator ie = new IntEnumerator(values);
                    Console.WriteLine("----- Простые числа из массива ----");
                    foreach (var item in ie)
                    {
                        Console.Write($"{item} ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("----- Выведем ещё раз -----");
                    foreach (var item in ie)
                    {
                        Console.Write($"{item} ");
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Press any key for continue or 'q'('Q') for Quit.");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                } while (Console.ReadKey().KeyChar.ToString().ToLower() != "q");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
            }            
        }
    }
}

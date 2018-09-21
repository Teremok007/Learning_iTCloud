using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        public  enum OutType
        {
            Default,
            SimpleNumbers,
            EvenNumbers,
            OddNumbers,
            AllNumbers            
        }
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
                    OutType OutputType;
                    do
                    {
                        OutputType = GetOutputType();
                        string Caption = "";
                        switch (OutputType)
                        {
                            case OutType.SimpleNumbers:
                                ie.condition = IsSimple;
                                Caption = "Простые числа из массива";
                                break;
                            case OutType.EvenNumbers:
                                ie.condition = Even;
                                Caption = "Только четные";
                                break;
                            case OutType.OddNumbers:
                                ie.condition = Odd;
                                Caption = "Нечетные числа";
                                break;
                            case OutType.AllNumbers:
                                ie.condition = null;
                                Caption = "Все числа в массиве";
                                break;
                        }
                        Console.WriteLine();
                        Console.WriteLine($"----- {Caption} ----");
                        Console.WriteLine();
                        foreach (int item in ie)
                            Console.Write($"{item} ");

                    } while (OutputType != OutType.Default);
                    Console.WriteLine("----- Простые числа из массива ----");
                    foreach (int item in ie)
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

        public static bool Even(int x)
        {
            return x % 2 == 0;
        }
        public static bool Odd(int x)
        {
            return x % 2 == 1;
        }

        public static bool IsSimple(int x)
        {
            double sqrtX = Math.Sqrt(x);
            for (int i = 2; i <= sqrtX; i++)
                if (x % i == 0) return false;
            return true;
        }

        public static OutType GetOutputType()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            try
            {
                Console.WriteLine(@"--- Вывод на экран
        1. Простые числа
        2. Четные
        3. Не четные
        4. вывести весь массив
           Выход");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine();
                Console.Write("Выберите пункт меню: ");

                int YourChoose = 0;
                int.TryParse(Console.ReadLine(), out YourChoose);
                switch (YourChoose)
                {
                    case 1:
                        return OutType.SimpleNumbers;

                    case 2:
                        return OutType.EvenNumbers;
                        
                    case 3:
                        return OutType.OddNumbers;
                    case 4:
                        return OutType.AllNumbers;                        

                    default:
                        return OutType.Default;
                }
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            
        }
    }
}

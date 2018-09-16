using System;

namespace Buttle
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Money myVailet = new Money(10000, 10000, 10000);
            Console.WriteLine("Мой кошелёк: {0}", myVailet.ToString());
            Console.WriteLine(myVailet);

            Exchenger ex = new Exchenger(new Money(10, 5, 1));
            Console.WriteLine("Отношения валют: {0}", ex.ToString());

            if (ex.GoldToCarbon(ref myVailet, 100))
            {
                Console.WriteLine("Поменял {0} золото на уголь. В кошельке осталось: ", 100);
                Console.WriteLine(myVailet);
            }

            Army enemy = new Army();
            Console.WriteLine("Решил создать армию: ", enemy);

            SoldierShop sshop = new SoldierShop(5,15, 1);
            Console.WriteLine("В округе есть магазин где продают солдат по цене: {0}", sshop);

            enemy.Add(sshop.Buy(ref myVailet, 1));
            Console.WriteLine("Сначала купил одного солдата, после этого мой кошелек: {0}", myVailet);
            Console.WriteLine("Моя армия: {0}", enemy);

            enemy.Add(sshop.Buy(ref myVailet, 10));
            Console.WriteLine("Потом купил {0} солдат. Кошелек попустел {1}", 10, myVailet);
            Console.WriteLine("Моя армия: {0}", enemy);
            */
            // моя армия
            Army enemy = new Army();
            // мой кошелёк
            Money myVailet = new Money(10000, 10000, 10000);
            //магазин для покупки солдатов
            SoldierShop sshop = new SoldierShop(5, 15, 1);
            // магазин для покупки генералов
            GeneralShop gshop = new GeneralShop(50, 30, 3);
            // магазин для покупки телег
            WiseShop wshop = new WiseShop(15, 0, 20);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            try
            {

                uint InputedMenuItemCode = 0;
                string inpStr;
                do
                {
                    // вывод данных об армии и кошельке
                    PrintArmyStat(enemy);
                    PrintVailetStat(myVailet);
                    // вывод пунктов меню
                    Console.WriteLine(@"Выберите пункт меню
                1. Купить солдат
                2. Купить генерала
                3. Купить телегу
                Q. Выход.");

                    inpStr = Console.ReadLine();
                    if (
                            (inpStr.ToLower() != "q") &&
                            (!uint.TryParse(inpStr, out InputedMenuItemCode))
                        )
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Ошибка!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Недопустимый пункт меню.");
                        continue;
                    }

                    switch (InputedMenuItemCode)
                    {
                        case 1:
                            BuyUnit(sshop, enemy,ref  myVailet);
                            break;
                        case 2:
                            BuyUnit(gshop, enemy, ref myVailet);
                            break;
                        case 3:
                            BuyUnit(wshop, enemy, ref myVailet);
                            break;
                        default:
                            break;

                    }

                } while (inpStr != "q");
            }
            catch (Exception)
            {

            }

            Console.ReadKey();
        }
        // ппокупка юнитов (смотря в каком магазине покупаем)
        private static void BuyUnit(BaseShop shop, Army arm, ref Money m)
        {
            Console.WriteLine($"Вы находесь в '{shop.About}'");
            Console.WriteLine($"Акции магазина '{shop.Sale}'");
            Console.WriteLine($"Цена за еденицу товара: '{shop.ToString()}'");


            uint count = InputInt();
            if (count == 0) return;
            arm.Add(shop.Buy(ref m, count));
        }
        // вывод информации про Армию
        private static void PrintArmyStat(Army army)
        {
            int safeCursorSize = Console.CursorSize;
            ConsoleColor cafeConsoleColor = Console.ForegroundColor;
            try
            {
                Console.CursorSize = 5;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Army:");
                Console.WriteLine($"Unit(s): {army.UnitCount}");
                Console.WriteLine($"\tGeneral(s): {army.GeneralCount}");
                Console.WriteLine($"\tSoldier(s): {army.SoldierCount}");
                Console.WriteLine($"\tWise(s): {army.WiseCount}");
                Console.WriteLine($"Atack: {army.Atack}");
                Console.WriteLine();
            }
            finally
            {
                Console.CursorSize = safeCursorSize ;
                Console.ForegroundColor = cafeConsoleColor;
            }
        }
        // вывод информации о кошельке
        private static void PrintVailetStat(Money vailet)
        {
            int safeCursorSize = Console.CursorSize;
            ConsoleColor safeConsoleColor = Console.ForegroundColor;
            try
            {
                Console.CursorSize = 5;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Cash:");
                Console.WriteLine($"\tGold(s): {vailet.Gold}");
                Console.WriteLine($"\tFood(s): {vailet.Food}");
                Console.WriteLine($"\tCarbon(s): {vailet.Carbon}");
                Console.WriteLine();
            }
            finally
            {
                Console.CursorSize = safeCursorSize;
                Console.ForegroundColor = safeConsoleColor;
            }
        }
        // Ввод значений
        private static uint InputInt()
        {
            string buf;
            uint count;
            do
            {
                Console.Write("Введите количество:");                
                buf = Console.ReadLine();
                if (
                    (buf.ToLower() != "q") &&
                    uint.TryParse(buf, out count)
                   )
                {
                    return count;
                }
            } while (buf.ToLower() != "q");
            return 0;
        }
        // Выыод на экран сообщений об ошибки message - описание ошибки
        private void PrintError(String message)
        {
            int safeCursorSize = Console.CursorSize;
            ConsoleColor cafeConsoleColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"Ошибка:" );
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(message);
                Console.WriteLine();
            }
            finally
            {
                Console.CursorSize = safeCursorSize;
                Console.ForegroundColor = cafeConsoleColor;
            }
        }
    }
}

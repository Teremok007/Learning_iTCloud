using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_4_1_lab
{
    class Program
    {
        delegate int factorial(int i);
        static void Main(string[] args)
        {
            // 9) declare object of OnlineShop 
            OnlineShop shop = new OnlineShop();
            // 10) declare several objects of Customer
            Customer cSergey = new Customer("Sergey");
            Customer cAndrey = new Customer("Andrey");
            Customer cVitaliy = new Customer("Vitaliy");
            Customer cAnna = new Customer("Anna");
            Customer cOlga = new Customer("Olga");

            // 11) subscribe method GotNewGoods() of every Customer instance 
            // to event NewGoodsInfo of object of OnlineShop
            shop.NewGoodsInfo += cSergey.GotNewGoods;
            shop.NewGoodsInfo += cAndrey.GotNewGoods;
            shop.NewGoodsInfo += cVitaliy.GotNewGoods;
            shop.NewGoodsInfo += cAnna.GotNewGoods;
            shop.NewGoodsInfo += cOlga.GotNewGoods;

            // 12) invoke method NewGoods() of object of OnlineShop
            // discuss results

            shop.NewGoods("Tom Soyer");
            Console.ReadKey();

        }
    }
}


using System;
using Calculator;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string expr;
            double calculate;
            expr = "2^(1+2)+3";
            calculate = new Calc(expr).Result;
            Console.WriteLine("{0} = {1}", expr, calculate);

            expr = "2^3+3";
            calculate = new Calc(expr).Result;
            Console.WriteLine("{0} = {1}", expr, calculate);

            expr = "1+2";
            calculate = new Calc(expr).Result;
            Console.WriteLine("{0} = {1}", expr, calculate);

            expr = "2+2*2";
            calculate = new Calc(expr).Result;
            Console.WriteLine("{0} = {1}", expr, calculate);

            expr = "(3+3)*3";
            calculate = new Calc(expr).Result;
            Console.WriteLine("{0} = {1}", expr, calculate);

            expr = "(2-(3+3))*3";
            calculate = new Calc(expr).Result;
            Console.WriteLine("{0} = {1}", expr, calculate);

            expr = "(2+1)*(3+2)";
            calculate = new Calc(expr).Result;
            Console.WriteLine("{0} = {1}", expr, calculate);

            expr = "2**3";
            calculate = new Calc(expr).Result;
            Console.WriteLine("{0} = {1}", expr, calculate);

            Console.WriteLine();
            Console.WriteLine("Enter your expression to test the my calculator (q - quit) :");
            while (String.Compare(expr = Console.ReadLine(), "q", true) != 0)
            {
                try
                {
                    ValidatorCalcExpr.ValidateAll(expr);
                    calculate = new Calc(expr).Result;
                    Console.WriteLine("{0} = {1}", expr, calculate);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }

                Console.WriteLine();
                Console.WriteLine("Enter your expression to test the my calculator (q - quit) :");
            } 

//            Console.ReadKey();
        }
    }
}

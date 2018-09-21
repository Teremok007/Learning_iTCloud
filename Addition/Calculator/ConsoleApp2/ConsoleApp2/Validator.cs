using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    // исключение если кол-во левых скобок не равно кол-ву правых скобок
    class ErrorCountLRPNotEqual : Exception
    {
        public ErrorCountLRPNotEqual() : base() { }
        public ErrorCountLRPNotEqual(string mes) : base(mes) { }
    }
    // исключение если недопустимый символ
    class ErrorInvalidTocken : Exception
    {
        public ErrorInvalidTocken(string expr, int pos) : base(String.Format("{0}.\nНедопустимый символ в позиции {1}", expr, pos))
        {
        }
    }

    public class ValidatorCalcExpr
    {
        // проверка на пустую строку
        public static void NotEmpty(string expr)
        {
            if (String.IsNullOrEmpty(expr))
                throw new ArgumentNullException();
        }
        // проверка на пустую строку
        public static void DivisionByZero(string expr)
        {
            if (expr.IndexOf("/0") >= 0)
                throw new DivideByZeroException("Ошибка!!! Деление на 0.");
        }
        // проверка количесства левых и правых скобок
        public static void LPEquallyRP(string expr)
        {
            int LeftIdx = -1, RightIdx = -1;

            while (!(
                  ((LeftIdx = expr.IndexOf('(', LeftIdx+1)) >= 0) ^
                  ((RightIdx = expr.IndexOf(')', RightIdx+1)) > 0)
                  ))
            {
                if (LeftIdx == RightIdx)
                    return;
                if (LeftIdx > RightIdx)
                    throw new ErrorCountLRPNotEqual(String.Format("Неправильное расположение ')' в ползиции {0}", RightIdx+1));
            }

            if (LeftIdx >= 0)
              throw new ErrorCountLRPNotEqual("Ожидается ')'");

            if (RightIdx >= 0)
              throw new ErrorCountLRPNotEqual("Ожидается '('");
        }
        // проверка на допустимость символов
        public static void ValidToken(string expr)
        {
            for (int i = 0; i < expr.Length; i++)
            {
                switch (expr[i])
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '^':
                    case '(':
                    case ')':
                    case ' ':
                        continue;
                    default:
                        throw new ErrorInvalidTocken(expr, i + 1);
                }
            }

        }

        // выполнить все проверки 
        public static void ValidateAll(string expr)
        {
            ValidatorCalcExpr.NotEmpty(expr);
            ValidatorCalcExpr.LPEquallyRP(expr);
            ValidatorCalcExpr.ValidToken(expr);
            ValidatorCalcExpr.DivisionByZero(expr);            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    class Calc
    {
        class General
        {
            protected static string expr;
            protected double left;
            public double result { get { return left; } set { left = value; } }
            protected virtual void Parse(out double val)
            {
                try
                {
                    while (expr.Length > 0)
                    {
                        switch (expr[0])
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
                                left = GetOperand();
                                break;
                            case '+':
                                expr = expr.Substring(1, expr.Length - 1);
                                left += GetResult(new Expression());
                                break;
                            case '-':
                                expr = expr.Substring(1, expr.Length - 1);
                                left += -1 * GetResult(new Expression());
                                break;
                            case '*':
                                left *= GetResult(new MulDiv(expr.Substring(1, expr.Length - 1)));
                                break;
                            case '/':
                                left /= GetResult(new MulDiv(expr.Substring(1, expr.Length - 1)));
                                break;
                            case '^':
                                left = Math.Pow(left, (new Power(expr.Substring(1, expr.Length - 1)).GetResult()));
                                break;
                            case '(':
                                left = GetResult(new Bracket(expr.Substring(1, expr.Length - 1)));
                                break;
                            default:
                                return;
                        }
                    }
                }
                finally
                {
                    val = left;
                }
            }
            public General() : base() { }
            public General(String s)
            {
                expr = s;
            }
            public virtual double GetResult(General gen = null)
            {
                General g;
                g = gen ?? this;
                g.Exec();
                return g.result;
            }
            protected virtual void Exec()
            {
                Parse(out left);
            }
            protected double GetOperand()
            {
                double operand = 0;
                int i  = expr.IndexOfAny(getToken(), 0);
                int sign = 1;
                while ((i == 0) && (expr[i] == '-'))
                {
                    sign *= -1;
                    expr = expr.Substring(1, expr.Length - 1);
                    i = expr.IndexOfAny(getToken(), 0);
                }
                int len = (i < 0) ? expr.Length : i;

                Double.TryParse(expr.Substring(0, len), out operand);
                expr = expr.Substring(len, expr.Length - len);
                if ((len > 0) &&(expr.Length > 0) && (expr[0] == '('))
                    expr = String.Concat('*', expr);
                operand *= sign;
                return operand;
            }
            protected virtual char[] getToken()
            {
                return new char[] { '(', ')', '+', '-', '/', '*', '^' };
            }
        }

        class Expression : General
        {
            protected override void Parse(out double val)
            {
                left = GetOperand();
                try
                {
                    while (expr.Length > 0)
                    {
                        switch (expr[0])
                        {
                            case '^':
                                left = Math.Pow(left, (new Power(expr.Substring(0, 1))).GetResult());
                                break;
                            case '*':
                                left *= GetResult(new MulDiv(expr.Substring(1, expr.Length - 1)));
                                break;
                            case '/':
                                left /= GetResult(new MulDiv(expr.Substring(1, expr.Length - 1)));
                                break;
                            case '(':
                                left = GetResult(new Bracket(expr.Substring(1, expr.Length - 1)));
                                break;
                            default:
                                return;
                        }
                    }
                }
                finally
                {
                    val = left;
                }
            }
        }
        class Bracket : General
        {
            public Bracket(String s) : base(s) { }
            protected override void Parse(out double val)
            {
                try
                {
                    while (expr.Length > 0)
                    {
                        switch (expr[0])
                        {
                            case ')':
                                expr = expr.Substring(1, expr.Length - 1);
                                if ((expr.Length > 0) && (expr.IndexOfAny(getToken(), 0) == -1))
                                        expr = String.Concat("*",expr);
                                return;
                            default:
                                left = (new General(expr)).GetResult(null);
                                break;
                        }
                    }
                }
                finally
                {
                    val = left;
                }
            }
            public override double GetResult(General gen = null)
            {
                this.Exec();
                return this.result;
            }
        }

        class MulDiv : General
        {
            public MulDiv(String s) : base(s) { }
            protected override void Parse(out double val)
            {
                left = GetOperand();
                try
                {
                    while (expr.Length > 0)
                    {
                        switch (expr[0])
                        {
                            case '*':
                                left *= GetResult(new MulDiv(expr.Substring(1, expr.Length - 1)));
                                break;
                            case '/':
                                left /= GetResult(new MulDiv(expr.Substring(1, expr.Length - 1)));
                                break;
                            case '^':
                                left = Math.Pow(left, (new Power(expr.Substring(1, expr.Length - 1))).GetResult());
                                break;

                            case '(':
                                left = GetResult(new Bracket(expr.Substring(1, expr.Length - 1)));
                                break;
                            default:
                                return;
                        }
                    }
                }
                finally
                {
                    val = left;
                }
            }
        }
        class Power : General
        {
            public Power(string s) { expr = s; }
            protected override void Parse(out double val)
            {
                left = GetOperand();
                try
                {
                    while (expr.Length > 0)
                    {
                        switch (expr[0])
                        {
                            case '^':
                                left = Math.Pow(left, (new Power(expr.Substring(1, expr.Length - 1))).GetResult());
                                break;

                            case '(':
                                left = GetResult(new Bracket(expr.Substring(1, expr.Length - 1)));
                                break;
                            default:
                                return;
                        }
                    }
                }
                finally
                {
                    val = left;
                }

            }
        }

        public double Result { get; set; }
        public Calc(string s)
        {

            StringBuilder buf = new StringBuilder(s);
            buf.Replace(" ", "");
            buf.Replace(")(", ")*(");
            General g = new General(buf.ToString());
            Result = g.GetResult(null);
        }
    }
}

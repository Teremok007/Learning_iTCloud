using System;
using System.Collections.Generic;
using System.Text;

namespace Buttle
{
    interface IMoney
    {
        uint Gold { get; set; }
        uint Food { get; set; }
        uint Carbon { get; set; }
    }

    class Money : IMoney, ICloneable

    {
        public uint Gold { get; set; }
        public uint Food { get; set; }
        public uint Carbon { get; set; }

        public Money(uint g, uint f, uint c)
        {
            Gold = g;
            Food = f;
            Carbon = c;
        }
        public Money() : this(0, 0, 0) { }

        public static Money operator +(Money one, Money tow)
        {
            if ((one == null) || (tow == null))
                throw new NullReferenceException();

            return new Money(one.Gold + tow.Gold, one.Food - tow.Food, one.Carbon - tow.Carbon);
        }
        public static Money operator -(Money one, Money tow)
        {
            if ((one == null) || (tow == null))
                throw new NullReferenceException();

            return new Money(one.Gold - tow.Gold, one.Food - tow.Food, one.Carbon - tow.Carbon);
        }
        public static Money operator *(Money one, Money tow)
        {
            if ((one == null) || (tow == null))
                throw new NullReferenceException();
            return new Money(one.Gold * tow.Gold, one.Food * tow.Food, one.Carbon * tow.Carbon);
        }
        public static Money operator *(Money one, uint k)
        {
            if (one == null)
                throw new NullReferenceException();
            return new Money(one.Gold * k, one.Food * k, one.Carbon * k);
        }
        public static Money operator *(uint k, Money one)
        {
            if ((one == null))
                throw new NullReferenceException();
            return new Money(one.Gold * k, one.Food * k, one.Carbon * k);
        }
        public static bool operator <(Money one, Money tow)
        {
            if ((one == null) || (tow == null))
                throw new NullReferenceException();

            return (one.Gold < tow.Gold) ||
                   (one.Food < tow.Food) || 
                   (one.Carbon < tow.Carbon);
        }
        public static bool operator >(Money one, Money tow)
        {
            if ((one == null) || (tow == null))
                throw new NullReferenceException();

            return (one.Gold > tow.Gold) ||
                   (one.Food > tow.Food) ||
                   (one.Carbon > tow.Carbon);
        }
        public static bool operator <=(Money one, Money tow)
        {
            if ((one == null) || (tow == null))
                throw new NullReferenceException();

            return (one < tow) || (one == tow);
        }
        public static bool operator >=(Money one, Money tow)
        {
            if ((one == null) || (tow == null))
                throw new NullReferenceException();

            return (one > tow) || (one == tow);
        }

        public bool Equals(Money m)
        {
            if (m == null)
                return false;
            return (Gold == m.Gold) &&
                   (Food == m.Food) &&
                   (Carbon == m.Carbon);
        }
        public object Clone()
        {
            return new Money(this.Gold, this.Food, this.Carbon);
        }

        public override string ToString()
        {
            return String.Format("Gold:{0},  Food:{1}, Carbon:{2}", Gold, Food, Carbon);
        }
    }
}

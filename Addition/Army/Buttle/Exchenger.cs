using System;
using System.Collections.Generic;
using System.Text;

namespace Buttle
{
    class Exchenger
    {
        private Money Course;
        public Exchenger(Money money)
        {
            Course = new Money(money.Gold, money.Food, money.Carbon);
        }
        public Exchenger(uint g, uint f, uint c) : this(new Money(g, f, c)) { }

        public Boolean GoldToFood(ref Money v, uint count)
        {
            if (count > v.Gold)
                return false;
            v.Gold -= count;
            v.Food = (uint)(count / Course.Gold)*Course.Food;
            return true;
        }
        public Boolean GoldToCarbon(ref Money v, uint count)
        {
            if (count > v.Gold)
                return false;
            v.Gold -= count;
            v.Carbon += (uint) (count / Course.Gold)* Course.Carbon;
            return true;
        }

        public override string ToString()
        {
            return String.Format("Оношение З:ПРОД:УГОЛЬ как {0}:{1}:{2}", Course.Gold, Course.Food, Course.Carbon);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Buttle
{
    interface IShop
    {
        AUnit[] Buy(ref Money v, uint count);
        string About { get; }
        string Sale { get; }
    }

    abstract class BaseShop : IShop
    {

        protected Money price;

        public abstract string About { get; }
        public abstract string Sale { get; }

        public BaseShop(Money money)
        {
            price = (Money)money.Clone();
        }
        public BaseShop() : this(new Money()) { }
        public BaseShop(uint g, uint f, uint c) : this(new Money(g, f, c)) { }
        public virtual AUnit[] Buy(ref Money v, uint count)
        {
            Money safeVailet = (Money)v.Clone();
            try
            {
                if (!EnoughMoney(v, count))
                    throw new ErrorNotEnoughMoney("Not enough money");
                v -= price * count;
                // отдел маркетинга сказал, что каждый 3-й солдат бесплатный, если покупаю сразц оптом
                uint bonus = getBonus(count);
                return CreateUnits(count + bonus);
            }
            catch (ErrorNotEnoughMoney)
            {
                throw;
            }
            catch (Exception)
            {
                v = safeVailet;
                return null;
            }
        }

        protected bool EnoughMoney(Money v, uint count)
        {
            return v >= (price * count);
        }
        protected abstract AUnit[] CreateUnits(uint count);
        protected abstract uint getBonus(uint count);
        public override string ToString()
        {
            return price.ToString();
        }

    }

    internal class SoldierShop : BaseShop
    {
        public override string About =>"Магазин продажи солдат.";
        public override string Sale => "При каждой покупке 3-й солдат в подарок.";
        public override AUnit[] Buy(ref Money v, uint count)
        {
            return (Soldier[])base.Buy(ref v, count);
        }
        protected override AUnit[] CreateUnits(uint count)
        {
            Soldier[] soldaten = new Soldier[count];
            for (int i = 0; i < count; i++)
            {
                soldaten[i] = new Soldier();
            }
            return soldaten;
        }
        protected override uint getBonus(uint count)
        {
            return (uint)(count / 3);
        }
        public SoldierShop() : base() { }
        public SoldierShop(Money money) : base(money) { }
        public SoldierShop(uint g, uint f, uint c) : base(g, f, c) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    class GeneralShop : BaseShop
    {
        public override string About => "Лучший магазин продажи генералов";
        public override string Sale => "При каждой покупке 7-й генерал в подарок.";
        public override AUnit[] Buy(ref Money v, uint count)
        {
            return (General[])base.Buy(ref v, count);
        }
        protected override AUnit[] CreateUnits(uint count)
        {
            General[] general = new General[count];
            for (int i = 0; i < count; i++)
            {
                general[i] = new General();
            }
            return general;
        }
        protected override uint getBonus(uint count)
        {
            return (uint)(count / 7);
        }
        public GeneralShop() : base() { }
        public GeneralShop(Money money) : base(money) { }
        public GeneralShop(uint g, uint f, uint c) : base(g, f, c) { }

    }
    class WiseShop : BaseShop
    {
        public override string About => "Лучший магазин продажи телег.";
        public override string Sale => "При каждой покупке 10-я телега в подарок.";
        public override AUnit[] Buy(ref Money v, uint count)
        {
            return (Wise[])base.Buy(ref v, count);
        }
        protected override AUnit[] CreateUnits(uint count)
        {
            Wise[] wises = new Wise[count];
            for (int i = 0; i < count; i++)
            {
                wises[i] = new Wise();
            }
            return wises;
        }
        protected override uint getBonus(uint count)
        {
            return (uint)(count / 10);
        }
        public WiseShop() : base() { }
        public WiseShop(Money money) : base(money) { }
        public WiseShop(uint g, uint f, uint c) : base(g, f, c) { }
    }
}

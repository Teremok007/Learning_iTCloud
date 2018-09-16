using System;
using System.Collections.Generic;
using System.Text;

namespace Buttle
{

    class ErrorNotEnoughMoney : Exception {
        public ErrorNotEnoughMoney(string s) : base(s) { }
    }
    class Army
    {
        // состав армии
        private List<AUnit> units;
        public Army()
        {
            units = new List<AUnit>();
        }
        // пополнение пополнение армии одной армейской еденицей
        public void Add(AUnit unit)
        {
            units.Add(unit);
        }
        // пополенение армии армейскими еденицами
        public uint Add(AUnit[] newunits)
        {            
            for (int i = 0; i < newunits.Length; i++)
                units.Add((AUnit)newunits[i]);
            return (uint)newunits.Length;
            
        }
        // количество аремйский едениц в составе армии
        public int UnitCount => units.Count;
        // количество солдат в армии
        public int SoldierCount
        {
            get
            {
                int count = 0;
                foreach (AUnit u in units)
                    if (u is Soldier)
                        count++;
                return count;
            }
        }
        // количество генералов в армии
        public int GeneralCount
        {
            get
            {
                int count = 0;
                foreach (AUnit u in units)
                    if (u is General)
                        count++;
                return count;
            }
        }
        // количество телег в армии
        public int WiseCount
        {
            get
            {
                int count = 0;
                foreach (AUnit u in units)
                    if (u is Wise)
                        count++;
                return count;
            }
        }
        // увольнение или списание с армии каких-либо едениц
        public void Remove(AUnit unit)
        {
            units.Remove(unit);
        }
        // мощность армии
        public uint Atack
        {
            get {
                uint atack = 0;
                foreach (AUnit u in units)
                {
                    atack += u.Atack;
                }
                return atack;
            }
        }
        // возвращает строку с кратким составом армии
        public override string ToString()
        {
            return String.Format("Unit(s): {0}, Atack: {1}", units.Count, Atack);
        }
 
    }

    internal class AUnit
    {        
        private uint speed;
        // количество  защиты
        public uint Guard;
        // боевая мощь 
        public uint Atack;
        // количество жизни (насколько живой модуль)
        public uint Life;
        // скорость передвижения
        public uint Speed { get {
                if (Life < 20) // если жизни нет. какое может быть движение? - объект присмерти
                    return 0;
                if (Life < 50) // еле живой объект. скорость меньше вдвое
                    return (uint)(speed / 2);
                return speed;
            } set { speed = value; } }
        // увольться из Армии
        public void Sack()
        {
            army.Remove(this);
        }
        // атака на  другого юнита 
        public void Hit(AUnit u)
        {
            
            if ((Life < 20) // если данный юнит присмерти, то он не может наносить поражение другому юниту
                ||
               (this.army == u.army)) // если оба юнита принадлежат к одной армии, то они не могут нанести друг-другу поаржение
                return;
            // атакуемый юнит поражается на уровень нашей атаки            
            u.injured(Atack);
        }
        // получить поражение
        public void injured(uint EnemyAtack)
        {
            if (Life <= 0)
                return;
            if (Guard > EnemyAtack)
            {
                Guard -= Math.Min((uint)EnemyAtack / 4, Guard);
                return;
            }
            Life = (uint)Math.Max(0, Life + Guard - EnemyAtack);
            Guard = Math.Max(0, Guard - (uint)EnemyAtack / 2);
            
            if (Life <= 0)// юнит убит
                army.Remove(this);
        }
        
        public void EnterInArmy(Army a)
        {
            army = a;
        }
        // конструктор
        public AUnit()
        {
            initialize();
        }
        // краткое описание
        public override string ToString()
        {
            return String.Format("Life: {0}\t Guard: {1}\t Atack: {2}\t Speed: {3}", Life, Guard, Atack, Speed);
        }
        // инициализация (например при инстанцировании данного модуля)
        protected virtual void initialize()
        {
            Life = 100;
            Guard = 0;
            Speed = 0;
            Atack = 0;
        }
        // принадлежность к армии
        Army army;
    }
    // класс солдат
    internal class Soldier : AUnit
    {
        protected override void initialize()
        {
            this.Guard = 10;
            this.Speed = 5;
            this.Atack = 1;
        }
        public Soldier() : base() { }
    }
    // класс генерал
    internal class General : AUnit
    {
        protected override void initialize()
        {
            this.Guard = 30;
            this.Speed = 3;
            this.Atack = 7;
        }
        public General() : base() { }    
    }
    // класс Телега
    internal class Wise : AUnit
    {
        protected override void initialize()
        {
            this.Guard = 3;
            this.Speed = 20;
            this.Atack = 0;
        }
        public Wise():base() { }
    }
}

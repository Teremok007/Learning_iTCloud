using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public delegate bool Conditions(int arg);
    public class IntEnumerator :  IEnumerator
    {
        private int[] values;
        private int position = -1;

        public IntEnumerator(int[] values)
        {
            this.values = values;

        }

        // IEnumerator
        public object Current 
        {
            get
            {
                if ((position >= values.Length) || (position < 0))
                    throw new IndexOutOfRangeException();
                return values[position];
            }
        }
        public bool MoveNext()
        {
            while ((++position < values.Length) && ((condition == null)? false: !condition.Invoke(values[position]))){ }
            return (position < values.Length);
        }
        public void Reset()
        {
            position = -1;
        }
        // IEnumerable
        public IEnumerator GetEnumerator()
        {
            this.Reset();
            return this;
        }
        public Conditions condition;        
    }
}

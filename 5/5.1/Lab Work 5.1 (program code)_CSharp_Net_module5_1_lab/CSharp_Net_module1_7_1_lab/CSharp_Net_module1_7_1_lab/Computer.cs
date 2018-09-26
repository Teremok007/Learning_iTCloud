using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_7_1_lab
{
    class Computer
    {
        public int Cores { get; set; }
        public double Frequency { get; set; }
        public int Memory { get; set; }
        public int Hdd { get; set; }
        public override string ToString()
        {
            return String.Format("Cores: {0}, Frequency:{1}, Memory:{2}, Hdd:{3}", Cores, Frequency, Memory, Hdd);
        }
    }
}

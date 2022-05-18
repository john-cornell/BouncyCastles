using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastles.Utilities
{
    public static class MLPMaths
    {
        static Random _random = new Random((int)DateTime.Now.Ticks);

        public static double RandomRange(double minimum, double maximum)
        {            
            return _random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}

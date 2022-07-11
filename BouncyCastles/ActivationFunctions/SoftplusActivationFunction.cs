using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastles.ActivationFunctions
{
    public class SoftplusActivationFunction : ActivationFunction
    {
        public SoftplusActivationFunction() :
            base((input) => 
            {
                double value = Math.Log(1 + Math.Exp(-Math.Abs(input))) + Math.Max(input, 0);

                //double value = Math.Log(1 + Math.Exp(input));
                return value;//(Double.IsInfinity(value)) ? input : value;


            },
                ActivationFunctionType.SoftPlus)
        { }
    }
}

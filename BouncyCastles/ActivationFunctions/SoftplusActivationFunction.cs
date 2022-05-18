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
            base((input) => Math.Log10(1 + Math.Exp(input)),
                ActivationFunctionType.SoftPlus)
        { }
    }
}

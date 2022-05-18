using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace BouncyCastles.ActivationFunctions
{
    public class SigmoidActivationFunction : ActivationFunction
    {
        public SigmoidActivationFunction() : base(
            (input) => 1 / (1 + Math.Exp(-input)),
            ActivationFunctionType.Sigmoid) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastles.ActivationFunctions
{
    public class ReluActivationFunction : ActivationFunction
    {
        public ReluActivationFunction() : base(
            (input) => Math.Max(0, input),
            ActivationFunctionType.Relu)
        { }
    }
}

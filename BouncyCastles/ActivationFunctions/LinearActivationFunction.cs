using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastles.ActivationFunctions
{
    public class LinearActivationFunction : ActivationFunction
    {
        public LinearActivationFunction() : base(
            (input) => input,
            ActivationFunctionType.Linear)
        { }
    }
}

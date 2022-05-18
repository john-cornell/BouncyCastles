using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastles.ActivationFunctions
{
    public class CustomActivationFunction : ActivationFunction
    {
        public string Tag { get; set; }        

        public CustomActivationFunction(string tag, Func<double, double> function) : base(function, ActivationFunctionType.Custom)
        {
            Tag = tag;
        }        
    }
}

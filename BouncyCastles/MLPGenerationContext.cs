using BouncyCastles.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastles
{
    public class MLPGenerationContext
    {
        public List<int> NodeSizes { get; set; } = new List<int> { 3, 5, 5, 5, 1 };
        public List<ActivationFunction> ActivationFunctions { get; set; } = new List<ActivationFunction>
        {
            ActivationFunction.Get(ActivationFunction.ActivationFunctionType.SoftPlus),
            ActivationFunction.Get(ActivationFunction.ActivationFunctionType.SoftPlus),
            ActivationFunction.Get(ActivationFunction.ActivationFunctionType.SoftPlus),
            ActivationFunction.Get(ActivationFunction.ActivationFunctionType.Relu)
        };

        public double NodeWeightMin { get; set; } = -1;
        public double NodeWeightMax { get; set; } = 1;
        public double NodeBiasMin { get; set; } = -1;
        public double NodeBiasMax { get; set; } = -1;

        /// <summary>
        /// Both hacks, I know, but nicely contained. Demoware.
        /// </summary>
        public int CurrentBuildNodeIndex { get; set; } = 0;

    }
}

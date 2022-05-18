using BouncyCastles.ActivationFunctions;
using BouncyCastles.Utilities;
using System;

namespace BouncyCastles
{
    public class Node
    {
        public Node(MLPGenerationContext context)
        {
            Generate(context);
        }

        public double Evaluate(double input)
        {
            return ActivationFunction.Evaluate((input * Weight) + Bias);
        }

        private void Generate(MLPGenerationContext context)
        {
            Weight = MLPMaths.RandomRange(context.NodeWeightMin, context.NodeWeightMax);
            Bias = MLPMaths.RandomRange(context.NodeBiasMin, context.NodeBiasMax);
        }

        public ActivationFunction ActivationFunction { get; set; }
        public double Weight { get; set; }
        public double Bias { get; set; }
    }
}
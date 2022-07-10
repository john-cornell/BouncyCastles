using BouncyCastles.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastles
{
    public class Node
    {
        public List<double> Weights { get; set; }

        int _layerLevel;        
        double _bias;
        double _value;

        public Node(int layerLevel)
        {
            _layerLevel = layerLevel;
            Weights = new List<double>();

            //Default - will not effect data 
            ActivationFunction = ActivationFunction.Get(ActivationFunction.ActivationFunctionType.Linear);
        }        

        public ActivationFunction ActivationFunction { get; set; }

        public double Value
        {
            set
            {
                if (_layerLevel != 0)
                    throw new InvalidOperationException("Only input nodes can set values directly");
                _value = value;
            }
            get { return _value; }
        }

        public double Bias { get => _bias; set => _bias = value; }
        public void AddWeight(double weight)
        {
            Weights.Add(weight);
        }

        public void Process(List<Node> inputNodes)
        {

            if (inputNodes.Count != Weights.Count)
                throw new InvalidOperationException("Weight count doesn't match input node count");

            if (_layerLevel > 0)
            {
                double weightedInputs = Enumerable.Range(0, Weights.Count)
                    .Sum(i => inputNodes[i].Value * Weights[i]);

                _value = ActivationFunction.Evaluate(_bias + weightedInputs);
            }
        }
    }
}

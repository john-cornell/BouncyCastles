using BouncyCastles;
using BouncyCastles.ActivationFunctions;
using BouncyCastles.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNISTImageRecognitionTraining
{
    public class Instance
    {
        NeuralNetwork _network;
        double _accuracytotal;

        public Instance(object genes = null)
        {
            _network = NeuralNetwork.Create(784, 500, 50, 10);

            if (genes == null)
            {
                Randomise();
            }
        }

        private void Randomise()
        {
            foreach (Layer layer in _network.Layers)
            {
                foreach (Node node in layer.Nodes)
                {
                    for (int i = 0; i < node.Weights.Count; i++)
                    {
                        node.Weights[i] = MLPMaths.RandomRange(-1, 1);
                    }
                    node.Bias = MLPMaths.RandomRange(-1, 1);
                    node.ActivationFunction = ActivationFunction.GetRandom();
                }
            }
        }

        public double Process(ImageData data)
        {
            _accuracytotal = 0d;

            foreach(NumericItem item in data.Items)
            {
                _accuracytotal += _network.Process(item.Data.Select(i => (double)i).ToArray())[item.Label];
            }

            return _accuracytotal / (double) data.Items.Count();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastles
{
    public class NeuralNetwork
    {
        public List<Layer> _layers;

        public NeuralNetwork()
        {
            _layers = new List<Layer>();
        }

        public Layer AddLayer()
        {
            Layer layer = new Layer(_layers.Count);
            _layers.Add(layer);            

            return layer;
        }

        public double[] Process(double[] inputData)
        {
            LoadInputs(inputData);

            for(int i = 1; i < _layers.Count; i++)
            {
                Layer layer = _layers[i];
                layer.Process(_layers[i - 1].Nodes);
            }

            return _layers.Last().Nodes.Select(node => node.Value).ToArray();
        }

        private void LoadInputs(double[] inputData)
        {
            if (inputData.Length != _layers[0].Nodes.Count)
                throw new InvalidOperationException("Input data length does not match size of network input layer");

            for (int i = 0; i < inputData.Length; i++)
            {
                _layers[0].Nodes[i].Value = inputData[i];
            }
        }
    }
}

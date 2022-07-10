using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastles
{
    public class NeuralNetwork
    {
        public List<Layer> Layers { get; set; }

        public NeuralNetwork()
        {
            Layers = new List<Layer>();
        }

        public static NeuralNetwork Create(params int[] layers)
        {
            NeuralNetwork neuralNetwork = new NeuralNetwork();

            foreach (int nodeCount in layers)
            {
                Layer layer = neuralNetwork.AddLayer();
                List<Node> nodes = layer.AddNodes(nodeCount).ToList();

                if (layer.LayerLevel == 0) continue;

                foreach(Node node in nodes)
                {
                    foreach(Node inputNode in neuralNetwork.Layers[layer.LayerLevel-1].Nodes)
                    {
                        node.AddWeight(1);
                    }
                }
            }

            return neuralNetwork;
        }

        public Layer AddLayer()
        {
            Layer layer = new Layer(Layers.Count);
            Layers.Add(layer);            

            return layer;
        }

        public double[] Process(double[] inputData)
        {
            LoadInputs(inputData);

            for(int i = 1; i < Layers.Count; i++)
            {
                Layer layer = Layers[i];
                layer.Process(Layers[i - 1].Nodes);
            }

            return Layers.Last().Nodes.Select(node => node.Value).ToArray();
        }

        private void LoadInputs(double[] inputData)
        {
            if (inputData.Length != Layers[0].Nodes.Count)
                throw new InvalidOperationException("Input data length does not match size of network input layer");

            for (int i = 0; i < inputData.Length; i++)
            {
                Layers[0].Nodes[i].Value = inputData[i];
            }
        }
    }
}

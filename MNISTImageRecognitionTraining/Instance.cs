using BouncyCastles;
using BouncyCastles.ActivationFunctions;
using BouncyCastles.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BouncyCastles.ActivationFunctions.ActivationFunction;

namespace MNISTImageRecognitionTraining
{
    public class Instance
    {
        public string Name { get; set; }
        double[] _genesCache = null;

        NeuralNetwork _network;
        public double Accuracy { get; private set; }
        public int NodeCount => _network.Layers.SelectMany(l => l.Nodes).Count();
        public Instance(double[] genes = null)
        {
            _network = NeuralNetwork.Create(784, 128, 10);

            if (genes == null)
            {
                Randomise();
            }
            else
            {
                Genes = genes;
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

        public double[] Process(double[] data)=> _network.Process(data);        

        public double Train(ImageData data)
        {
            Accuracy = 0d;

            foreach (NumericItem item in data.Items)
            {
                double[] result = _network.Process(item.Data.Select(i => (double)i).ToArray());

                if (item.Label == 1)
                {
                    if (result.Max() == result[item.Label])
                    {
                        if (result.Count(r => r == result[item.Label]) == 1)
                            Accuracy += 1d;
                    }
                    else
                    {
                        Accuracy -= 1d;
                    }
                }
                
                else if (result.Max() != result[1])
                {
                    if (item.Label == 1)
                        Accuracy -= 1d;
                    else
                        Accuracy += 1d;
                }                
            }

            return Accuracy;
        }

        private IEnumerable<double> ToGeneEnumerable()
        {
            //Need to do sections in order so they don't get mucked up
            foreach (Layer layer in _network.Layers)
            {
                foreach (Node node in layer.Nodes)
                {
                    yield return (double)node.ActivationFunction.FunctionType;
                }
            }

            foreach (Layer layer in _network.Layers)
            {
                foreach (Node node in layer.Nodes)
                {
                    yield return node.Bias;
                }
            }

            foreach (Layer layer in _network.Layers)
            {
                foreach (Node node in layer.Nodes)
                {
                    foreach (double weight in node.Weights) yield return weight;
                }
            }
        }

        public double[] Genes
        {
            get
            {
                if (_genesCache == null) _genesCache = ToGeneEnumerable().ToArray();
                return _genesCache;
            }
            set
            {

                try
                {
                    int position = 0;

                    foreach (Layer layer in _network.Layers)
                    {
                        foreach (Node node in layer.Nodes)
                        {
                            node.ActivationFunction = ActivationFunction.Get((ActivationFunctionType)NextGene(value, ref position));
                        }
                    }

                    foreach (Layer layer in _network.Layers)
                    {
                        foreach (Node node in layer.Nodes)
                        {
                            node.Bias = NextGene(value, ref position);
                        }
                    }
                    foreach (Layer layer in _network.Layers)
                    {
                        foreach (Node node in layer.Nodes)
                        {
                            for (int i = 0; i < node.Weights.Count; i++)
                            {
                                node.Weights[i] = NextGene(value, ref position);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error loading genes");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private double NextGene(double[] genes, ref int position)
        {
            double gene = genes[position];
            position++;
            return gene;
        }
    }
}

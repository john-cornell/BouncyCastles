using BouncyCastles;
using BouncyCastles.ActivationFunctions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastlesTests
{
    [TestFixture]
    public class TestNetwork
    {
        [Test]
        public void When_known_network_processed_output_should_be_expected()
        {
            NeuralNetwork network = new NeuralNetwork();

            Layer layer0 = network.AddLayer();
            Layer layer1 = network.AddLayer();
            Layer layer2 = network.AddLayer();

            Node node1_1 = layer1.AddNode();
            Node node1_2 = layer1.AddNode();

            Node node2_1 = layer2.AddNode();

            //Input node - no weights, bias or activation functions are called so we can leave this as default values
            layer0.AddNode();

            node1_1.AddWeight(-34.4);
            node1_1.Bias = 2.14;
            node1_1.ActivationFunction = ActivationFunction.Get(ActivationFunction.ActivationFunctionType.SoftPlus);

            node1_2.AddWeight(-2.52);
            node1_2.Bias = 1.29;
            node1_2.ActivationFunction = ActivationFunction.Get(ActivationFunction.ActivationFunctionType.SoftPlus);

            //Node 2 uses default activation function, linear, no effect
            node2_1.AddWeight(-1.3);
            node2_1.AddWeight(2.28);
            node2_1.Bias = -0.58;

            for (decimal dm = 0; dm <= 1; dm += 0.02m)
            {
                double d = (double)dm;

                double[] output = network.Process(new double[] { d });

                Assert.AreEqual(output[0], _nnExpected[d], 0.000000001);
            }
        }

        Dictionary<double, double> _nnExpected = new Dictionary<double, double>
        {
            {0,-0.0110009201657636},
            {0.02,0.664942106447793},
            {0.04,1.24706249799763},
            {0.06,1.69838532037623},
            {0.08,2.00008249665047},
            {0.1,2.16458632619144},
            {0.12,2.22672685123191},
            {0.14,2.22390956884439},
            {0.16,2.18425992883984},
            {0.18,2.12528391194104},
            {0.2,2.05682252265939},
            {0.22,1.98412176445969},
            {0.24,1.90990603230697},
            {0.26,1.83557492985673},
            {0.28,1.76184680811808},
            {0.3,1.68909318529708},
            {0.32,1.61750961769917},
            {0.34,1.54720226080466},
            {0.36,1.47823152021931},
            {0.38,1.41063400261127},
            {0.4,1.34443352922898},
            {0.42,1.27964664734426},
            {0.44,1.21628537816337},
            {0.46,1.15435857944339},
            {0.48,1.09387261610621},
            {0.5,1.03483168754421},
            {0.52,0.977237986996622},
            {0.54,0.921091781201044},
            {0.56,0.866391454657111},
            {0.58,0.813133540739993},
            {0.6,0.761312750744821},
            {0.62,0.710922006287742},
            {0.64,0.66195247760148},
            {0.66,0.614393628769918},
            {0.68,0.568233270157658},
            {0.7,0.523457617861017},
            {0.72,0.48005135976298},
            {0.74,0.437997727629541},
            {0.76,0.397278574595042},
            {0.78,0.357874457328035},
            {0.8,0.31976472213556},
            {0.82,0.282927594247072},
            {0.84,0.247340269516201},
            {0.86,0.212979007787228},
            {0.88,0.179819227192064},
            {0.9,0.147835598671351},
            {0.92,0.11700214004893},
            {0.94,0.08729230903104},
            {0.96,0.058679094549098},
            {0.98,0.031135105916508},
            {1,0.00463265932454671}
        };


    }

}


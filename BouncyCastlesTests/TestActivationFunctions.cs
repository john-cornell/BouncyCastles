using BouncyCastles.ActivationFunctions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BouncyCastlesTests
{
    [TestFixture]
    public class TestActivationFunctions
    {
        int tolerance = 10000000;

        [Test]
        public void WhenRelu_GivenValueLessThanZero_ShouldReturnZero()
        {
            List<double> values = new List<double>
            {
                -0.1, -23.4, -9, -2331.034
            };

            var relu = ActivationFunction.Get(ActivationFunction.ActivationFunctionType.Relu);

            var output = values.Select(v => relu.Evaluate(v)).ToList();

            Assert.AreEqual(true, output.All(v => v == 0));
        }

        [Test]
        public void WhenRelu_GivenValueGreaterThanZero_ShouldReturnValue()
        {
            List<double> values = new List<double>
            {
                0.1, 23.4, 9, 2331.034
            };

            var relu = ActivationFunction.Get(ActivationFunction.ActivationFunctionType.Relu);

            var output = values.Select(v => relu.Evaluate(v)).ToList();

            Assert.IsTrue(Enumerable.SequenceEqual(values, output));
        }

        [Test]
        public void WhenSigmoidCalled_ShouldReturnCorrectValue()
        {
            var expected = new List<double> {
                0.006692851,
                0.010986943,
                0.01798621,
                0.029312231,
                0.047425873,
                0.07585818,
                0.119202922,
                0.182425524,
                0.268941421,
                0.377540669,
                0.5,
                0.622459331,
                0.731058579,
                0.817574476,
                0.880797078,
                0.92414182,
                0.952574127,
                0.970687769,
                0.98201379,
                0.989013057,
                0.993307149};

            var sigmoid = ActivationFunction.Get(ActivationFunction.ActivationFunctionType.Sigmoid);

            var actual =
                Enumerable.Range(0, 21).Select(i => -5 + ((double)i * 0.5d)).Select(v => sigmoid.Evaluate(v)).ToList();

            //Within a given precision for Excel vs .NET double * 10000000
            //There's better logarithmic ways of doing this, but this tolerance is enough to show I got equation right
            Assert.IsTrue(Enumerable.SequenceEqual(
                expected.Select(v => (int)(v * tolerance)),
                actual.Select(v => (int)(v * tolerance))
                ));
        }

        [Test]
        public void WhenSoftplusCalled_ShouldReturnCorrectValue()
        {
            var expected = new List<double> {
                0.006715348,
                0.011047745,
                0.018149928,
                0.029750418,
                0.048587352,
                0.078889734,
                0.126928011,
                0.201413278,
                0.313261688,
                0.474076984,
                0.693147181,
                0.974076984,
                1.313261688,
                1.701413278,
                2.126928011,
                2.578889734,
                3.048587352,
                3.529750418,
                4.018149928,
                4.511047745,
                5.006715348

            };

            var softplus = ActivationFunction.Get(ActivationFunction.ActivationFunctionType.SoftPlus);

            var actual =
                Enumerable.Range(0, 21).Select(i => -5 + ((double)i * 0.5d)).Select(v => softplus.Evaluate(v)).ToList();

            //Within a given precision for Excel vs .NET double * 10000000
            //There's better logarithmic ways of doing this, but this tolerance is enough to show I got equation right
            Assert.IsTrue(Enumerable.SequenceEqual(
                expected.Select(v => (int)(v * tolerance)),
                actual.Select(v => (int)(v * tolerance))
                ));
        }
    }
}

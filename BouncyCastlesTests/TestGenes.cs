using BouncyCastles;
using MNISTImageRecognitionTraining;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyCastlesTests
{
    [TestFixture]
    public class TestGenes
    {
        [Test]
        public void WhenCreatingGene_ShouldRecreate()
        {
            Instance instance = new Instance();
            double[] genes = instance.Genes;            

            Instance instance2 = new Instance(instance.NetworkSize, genes);
            double[] genes2 = instance2.Genes;

            Assert.IsTrue(Enumerable.SequenceEqual(genes, genes2));
        }
    }
}

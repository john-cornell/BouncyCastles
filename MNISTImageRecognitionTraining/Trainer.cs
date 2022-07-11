using BouncyCastles.ActivationFunctions;
using BouncyCastles.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNISTImageRecognitionTraining
{
    public class InstanceTrainer
    {
        Instance _instance;
        int _epoch;
        int _i;

        public InstanceTrainer(Instance instance, int e, int i)
        {
            _instance = instance;
            _epoch = e;
            _i = i;
        }

        public async Task Train(ImageData imageData)
        {
            Console.Write($"{_epoch}: {_i} :");
            _instance.Train(imageData);

            Console.WriteLine(_instance.Accuracy);
        }
    }


    internal class Trainer
    {
        string Id = Guid.NewGuid().ToString();
        int _minPopulation;
        List<Instance> _instances;
        int _epochs;
        public Trainer(int populationCount = 20, int epochs = 1000)
        {
            _instances = new List<Instance>();
            _epochs = epochs;
            _minPopulation = populationCount;

            Enumerable
                .Range(0, populationCount)
                .ToList()
                .ForEach(i => _instances.Add(new Instance()));
        }

        public async Task Train(ImageData data)
        {
            double max = 0;

            for (int e = 0; e < _epochs; e++)
            {

                List<InstanceTrainer> trainers = Enumerable.Range(0, _instances.Count).Select(i => new InstanceTrainer(_instances[i], e, i)).ToList();

                await Task.WhenAll(trainers.Select(trainer => trainer.Train(data)));

                Instance[] top = GetMax(_instances, double.MaxValue);
                Instance[] nextTop = GetMax(_instances, top[0].Accuracy);

                if (top[0].Accuracy > max)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    foreach (double gene in top[0].Genes)
                    {
                        stringBuilder.Append(gene);
                        stringBuilder.Append(",");
                    }                    

                    Console.WriteLine($"Improvement {max} -> {top[0].Accuracy}");
                    File.WriteAllText($"{Id}-Epoch-{e}_{top[0].Accuracy}.nn", stringBuilder.ToString());
                    max = top[0].Accuracy;
                }

                _instances = GetNewInstances(top, nextTop).ToList();
                Console.WriteLine($"New population count: {_instances.Count}");
            }
        }

        private IEnumerable<Instance> GetNewInstances(Instance[] top, Instance[] nextTop)
        {
            int items = (top.Length + nextTop.Length) * 3;

            foreach (Instance instance in top)
            {
                yield return instance;
                yield return Mutate(instance);
                yield return Mutate(instance);
            }

            foreach (Instance instance in nextTop)
            {
                yield return instance;
                yield return Mutate(instance);
                yield return Mutate(instance);
            }

            foreach (Instance instance in top)
            {
                double[] instanceGenes = instance.Genes;

                foreach (Instance instance2 in nextTop)
                {
                    items += 6;
                    (Instance, Instance) instances = CrossOver(instanceGenes, instance2.Genes);

                    yield return instances.Item1;
                    yield return instances.Item2;

                    yield return Mutate(instances.Item1);
                    yield return Mutate(instances.Item2);
                    yield return Mutate(instances.Item1);
                    yield return Mutate(instances.Item2);
                }
            }

            int newItems = 0;

            while (items <= _minPopulation || newItems < 5)
            {
                newItems++;
                items += 1;

                yield return new Instance();
            }
        }

        private Instance Mutate(Instance item)
        {
            double[] newGenes = item.Genes;

            double percent = MLPMaths.RandomRange(0.001d, 3d);

            int number = (int)Math.Round(percent * (double)item.Genes.Length);
            int nodeNumber = item.NodeCount;

            for (int i = 0; i < number; i++)
            {
                int position = MLPMaths.Random(0, item.Genes.Length);

                if (position < nodeNumber) //Activation Functions store first
                {
                    newGenes[position] = (double)ActivationFunction.GetRandom().FunctionType;
                }
                else
                {
                    newGenes[position] = MLPMaths.RandomRange(-1, 1);
                }
            }

            return new Instance(newGenes);
        }

        (Instance, Instance) CrossOver(double[] geneA, double[] geneB)
        {
            int pos = MLPMaths.Random(0, geneA.Length);

            double[] copyA = new double[geneA.Length];
            double[] copyB = new double[geneB.Length];

            Array.Copy(geneA, copyA, geneA.Length);
            Array.Copy(geneB, copyB, geneB.Length);

            for (int i = pos; i < geneA.Length; i++)
            {
                copyB[i] = geneA[i];
                copyA[i] = geneB[i];
            }

            return (new Instance(copyA), new Instance(copyB));
        }

        private Instance[] GetMax(List<Instance> instances, double previousMax)
        {
            double max = instances.Where(i => i.Accuracy < previousMax).Max(i => i.Accuracy);

            return instances.Where(i => i.Accuracy == max).ToArray();
        }
    }
}

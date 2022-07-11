using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNISTImageRecognitionTraining
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading data");
            //https://github.com/teavanist/MNIST-JPG for data

            //var data = new Loader().GetData(@"C:\FB\archive\trainingSet\trainingSet");

            var data = new Loader().GetData(@"C:\FizzBash 2022\archive\trainingSample\trainingSample");
            //var data = new Loader().GetData(@"C:\SBTemp");
            Console.WriteLine("Training");

            Trainer trainer = new Trainer();

            trainer.Train(data);
        }
    }
}

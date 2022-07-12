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

            var data = Loader.Instance.ImageDataDefault;

            Console.WriteLine("Training");

            Trainer trainer = new Trainer();

            trainer.Train(data);
        }
    }
}

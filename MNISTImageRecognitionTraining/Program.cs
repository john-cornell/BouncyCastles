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
            var data = new Loader().GetData(@"C:\FB\archive\trainingSet\trainingSet");
        }
    }
}

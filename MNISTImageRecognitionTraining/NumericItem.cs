using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNISTImageRecognitionTraining
{
    public class NumericItem
    {
        public NumericItem(string name, int label)
        {
            Name = name;
            Label = label;
        }

        public int[] Data { get; set; }
        public int Label { get; set; }
        public string Name { get; set; }

    }
}

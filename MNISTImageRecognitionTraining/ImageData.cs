using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNISTImageRecognitionTraining
{
    public class ImageData
    {
        public Dictionary<string, NumericItem> Data { get; set; }

        public ImageData(Dictionary<string, NumericItem> data)
        {
            Data = data;
        }

        public List<NumericItem> this [int item]
        { 
            get
            {                
                return Data.Where(d => d.Value.Label == item).Select(i => i.Value).ToList();
            }
        }
        
        public IEnumerable<NumericItem> Items => Data.Values.ToList();
    }
}

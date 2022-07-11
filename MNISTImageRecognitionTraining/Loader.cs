using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNISTImageRecognitionTraining
{
    public class Loader
    {
        public ImageData GetData (string directory)
        {
            Dictionary<string, NumericItem> data = new Dictionary<string, NumericItem>();

            DirectoryInfo info = new DirectoryInfo(directory);

            if (!info.Exists)
            {
                Console.WriteLine($"Unable to locate {directory}");
                return null;
            }

            foreach (DirectoryInfo numberedDirectory in info.GetDirectories())
            {
                if (numberedDirectory.Name.Length == 1 &&
                    int.TryParse(numberedDirectory.Name, out int label))
                {
                    Console.WriteLine(numberedDirectory.Name);

                    foreach (FileInfo file in numberedDirectory.EnumerateFiles("*.jpg"))
                    {
                        data[file.Name] = new NumericItem(file.Name, label);
                        var item = data[file.Name];

                        item.Data = GetImageFileData(file.FullName);
                    }
                }
            }

            return new ImageData(data);
        }

        public int[] GetImageFileData(string path)
        {
            Bitmap bitmap = Bitmap.FromFile(path) as Bitmap;
            List<int> values = new List<int>();

            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    System.Drawing.Color pixel = bitmap.GetPixel(i, j);
                    values.Add(pixel.R);//Greyscale, RGB values are identical
                }
            }
            return values.ToArray();
        }

        public async Task<Instance> GetInstance(string filename, ImageData imageData)
        {            
            string data = File.ReadAllText(filename);
           
            double[] genes = data.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => Double.Parse(i)).ToArray();

           Instance instance = new Instance(genes);

           instance.Train(imageData);
           instance.Name = $"{new FileInfo(filename).Name} ({instance.Accuracy}) {(Math.Round(instance.Accuracy/(double) imageData.Data.Count, 2) * 100d)}%";

            return instance;
        }
    }
}

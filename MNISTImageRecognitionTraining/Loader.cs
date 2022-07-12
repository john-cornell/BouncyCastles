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
        public static Loader Instance = new Loader(true);

        bool _fullGreyscale;

        private Loader(bool fullGreyscale)
        {
            _fullGreyscale=fullGreyscale;
            ImageDataDefault = GetData(@"C:\FizzBash 2022\archive\trainingSample\trainingSample");
        }

        public ImageData ImageDataDefault { get; private set; }

        private ImageData GetData (string directory)
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

                        item.Data = GetImageFileData(file.FullName, true);
                    }
                }
            }

            return new ImageData(data);
        }

        public int[] GetImageFileData(string path, bool fullGreyScale)
        {
            Bitmap bitmap = Bitmap.FromFile(path) as Bitmap;
            List<int> values = new List<int>();

            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    System.Drawing.Color pixel = bitmap.GetPixel(i, j);

                    int colour = fullGreyScale ? pixel.R : pixel.R > 12 ? 255 : 0;

                    values.Add(colour);//Greyscale, RGB values are identical so only looking at R
                }
            }
            return values.ToArray();
        }

        public async Task<Instance> GetInstance(string filename, ImageData imageData)
        {            
            string data = File.ReadAllText(filename);
            
            string[] dataParts = data.Split(new char[] { ']' }, StringSplitOptions.RemoveEmptyEntries);
            
            int[] layers = dataParts[0].Substring(1).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => Int32.Parse(i)).ToArray();

            double[] genes = dataParts[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => Double.Parse(i)).ToArray();

           Instance instance = new Instance(layers, genes);

           instance.Train(imageData);
           instance.Name = $"{new FileInfo(filename).Name} ({instance.Accuracy}) {(Math.Round(instance.Accuracy/(double) imageData.Data.Count, 2) * 100d)}%";

            return instance;
        }
    }
}

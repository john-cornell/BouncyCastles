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
        public Dictionary<string, NumericItem> GetData (string directory)
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

                        Bitmap bitmap = Bitmap.FromFile(file.FullName) as Bitmap;
                        List<int> values = new List<int>();

                        for (int i = 0; i < 28; i++)
                        {
                            for (int j = 0; j < 28; j++)
                            {
                                System.Drawing.Color pixel = bitmap.GetPixel(i, j);
                                values.Add(pixel.R);//Greyscale, RGB values are identical
                            }
                        }
                        item.Data = values.ToArray();
                    }
                }
            }

            return data;
        }
    }
}

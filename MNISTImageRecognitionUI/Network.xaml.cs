using Microsoft.Win32;
using MNISTImageRecognitionTraining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MNISTImageRecognition
{
    /// <summary>
    /// Interaction logic for Network.xaml
    /// </summary>
    public partial class Network : Window
    {
        OpenFileDialog _dlg = new OpenFileDialog();
        ImageData _imageData;
        public Network(ImageData imageData)
        {
            InitializeComponent();
            _imageData = imageData;           
            _dlg.InitialDirectory = @"C:\FizzBash 2022\";
            _dlg.Filter = "Trained Data files (*.nn)|*.nn|All Files (*.*)|*.*";
            _dlg.RestoreDirectory = true;
            _dlg.Multiselect = false; 
        }

        public Instance SelectedInstance { get; set; }

        private async void BrowseButton_Click(object sender, RoutedEventArgs e)
        { 

            if (_dlg.ShowDialog() ?? false)
            {
                try
                {
                    Loader loader = Loader.Instance;

                    InstanceName.Content = "LOADING ...";

                    SelectedInstance = await loader.GetInstance(_dlg.FileName, _imageData);

                    Test.Visibility = Visibility.Visible;

                    InstanceName.Content = $"{_dlg.FileName} ({SelectedInstance.Accuracy})" ;
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Error Loading Instance {ex.Message}/r/n/r/n{ex.StackTrace}");
                }
            }
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            var imageData = Loader.Instance.ImageDataDefault;

            SelectedInstance.Train(imageData);

            MessageBox.Show(SelectedInstance.Accuracy.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

using Microsoft.Win32;
using MNISTImageRecognitionTraining;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MNISTImageRecognition
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog _dlg = new OpenFileDialog();
        Instance _instance;
        ImageData _imageData;
        Loader _loader;
        public MainWindow()
        {
            InitializeComponent();
            _loader = new Loader();
            _imageData = _loader.GetData(@"C:\FizzBash 2022\archive\trainingSample\trainingSample");    
            
            _dlg.InitialDirectory = @"C:\FizzBash 2022\";
            _dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            _dlg.RestoreDirectory = true;
            _dlg.Multiselect = false;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {                       
            if (_dlg.ShowDialog() ?? false)
            {
                One.Visibility = Visibility.Collapsed;
                NotOne.Visibility = Visibility.Collapsed;

                string selectedFileName = _dlg.FileName;
                FileNameLabel.Content = selectedFileName;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(selectedFileName);
                image.EndInit();
                SelectedImage.Source = image;

                double[] imageData = _loader
                    .GetImageFileData(_dlg.FileName)
                    .Select(i => (double)i)
                    .ToArray();

                double[] output = _instance.Process(imageData);

                bool iThinkItsAOne = output[1] == output.Max();

                One.Visibility = iThinkItsAOne ? Visibility.Visible : Visibility.Collapsed;
                NotOne.Visibility = iThinkItsAOne ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private void NetworkButton_Click(object sender, RoutedEventArgs e)
        {
            Network network = new Network(_imageData);

            network.ShowDialog();

            if (network.SelectedInstance != null)
            {
                BrowseButton.IsEnabled = true;
                Warning.Visibility = Visibility.Collapsed;
                Title = $"Using {network.SelectedInstance.Name}";
                _instance = network.SelectedInstance;                
            }
        }
    }
}
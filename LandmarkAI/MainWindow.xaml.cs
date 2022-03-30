using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LandmarkAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Imagens no formato:(*.png; *.jpg)|*.png; *.jpg;*.jpeg";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            predictionListView.ItemsSource = null;

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;

                selectedImage.Source = new BitmapImage(new Uri(filename));

                MakePredictionAsync(filename);

            };
        }

        private async void MakePredictionAsync(string filename)
        {
            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/19a4b2a9-dc6d-4e4b-81cb-2d6ea8b4cc63/classify/iterations/Model1/image";

            string prediciton_key = "fc69133cfc3344108f94f91003dd431b";

            string content_type = "application/octet-stream";

            var file = File.ReadAllBytes(filename);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Prediction-Key", prediciton_key);

                using (var content = new ByteArrayContent(file))
                {
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(content_type);
                    var response = await client.PostAsync(url, content);

                    var responseString = await response.Content.ReadAsStringAsync();

                    List<Prediction> predictions = (JsonConvert.DeserializeObject<CustomView>(responseString)).predictions;

                    predictionListView.ItemsSource = predictions;
                }


            };
        }
    }


}

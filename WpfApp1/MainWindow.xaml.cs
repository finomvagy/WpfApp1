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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;

namespace WpfApp1
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
        async void CreateTextBlock()
        {
            kacsak.Children.Clear();
    
            HttpClient client = new HttpClient();
            string url = "http://127.0.0.1:3000/kacsa";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string stringResponse = await response.Content.ReadAsStringAsync();
                List<KacsaClass> kacsalist = JsonConvert.DeserializeObject<List<KacsaClass>>(stringResponse);
                foreach (KacsaClass item in kacsalist)
                {
                    TextBlock oneBlock = new TextBlock();
                    oneBlock.Text = $"KACSA NEVE: {item.name}, KACSA HOSSZA: {item.length}";
                    kacsak.Children.Add(oneBlock);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
            
        async void AddKacsa(object s, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = "http://127.0.0.1:3000/kacsa";
                var jsonObject = new
                {
                    name = nev.Text,
                    length = darab.Text
                };
                string jsondata = JsonConvert.SerializeObject(jsonObject);
                StringContent data = new StringContent(jsondata, Encoding.UTF8, "application/json");
                HttpResponseMessage respons = await client.PostAsync(url, data);
                respons.EnsureSuccessStatusCode();
                CreateTextBlock();
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }


            MessageBox.Show($"kacsa hozzáadása: {nev.Text}, kacsa hossza : {darab.Text}");
        }
    }
}

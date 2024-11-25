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
        async void AddKacsa(object s, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = "localhost:3000/kacsa";
                var jsonObject = new
                {
                    name = nev.Text,
                    length = darab.Text
                };
                string jsondata = JsonConvert.SerializeObject(jsonObject);
                StringContent data = new StringContent(jsondata, Encoding.UTF8, "application/json");
                HttpResponseMessage respons = await client.PostAsync(url, data);
                respons.EnsureSuccessStatusCode();
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }


            MessageBox.Show($"kacsa hozzáadása: {nev.Text}, kacsa hossza : {darab.Text}");
        }
    }
}

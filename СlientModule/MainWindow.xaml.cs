using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace СlientModule
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient;
        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://prb.sylas.ru/TransferSimulator/");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = Spisok.SelectedItem as ComboBoxItem;
            string selectedText = selectedItem.Content.ToString();
            GetData(selectedText);
        }
        public async Task GetData(string selectvalue)
        {
            var response = await _httpClient.GetAsync(selectvalue);
            string content = await response.Content.ReadAsStringAsync();
            using (JsonDocument doc = JsonDocument.Parse(content))
            {
                
                if (doc.RootElement.TryGetProperty("value", out JsonElement valueElement))
                {
                    Result.Content = valueElement.GetString();
                }
                else
                {
                    Result.Content = "Ключ 'value' не найден";
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using ApiControl;
using Models;
using Newtonsoft.Json;
namespace Client
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
        }
        public static async Task<Login> CheckLogin(string name, string password)
        {
            try
            {
                object user = new { name, password };
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("https://localhost:44379/api/login", content);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Login jsonObject = JsonConvert.DeserializeObject<Login>(result);
                    return jsonObject;
                }
                else
                {
                    return new Login();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string passwd = Password.Password;
            Login admin = await CheckLogin(login, passwd);
            if(admin.Token != null)
            {
                Admin window = new Admin(admin);
                Close();
                window.Show();
            }
            else
            {
                MessageBox.Show("Invalid input or user not exist");
            }
        }

        private void Views(object sender, RoutedEventArgs e)
        {
            View view = new View();
            Close();
            view.Show();
        }
    }
}

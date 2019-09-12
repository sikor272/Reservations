using ApiControl;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Logika interakcji dla klasy EditSubjects.xaml
    /// </summary>
    public partial class EditSubjects : Window
    {
        static Login login;
        static int Id;
        public EditSubjects(Login user)
        {
            InitializeComponent();
            ApiHelper.Token = user.Token;
            ApiHelper.InitializeClient();
            login = user;
            Initialize();
        }
        public static async Task<Subjects[]> LoadSubjects()
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:44379/api/subjects");

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Subjects[] jsonObject = JsonConvert.DeserializeObject<Subjects[]>(result);
                    return jsonObject.ToArray();
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async void Initialize()
        {
            try
            {
                Subjects[] subjects = await LoadSubjects();
                DataTable table = new DataTable();
                table.Columns.Add("Id", typeof(int));
                table.Columns.Add("Nazwa", typeof(string));
                foreach (DataColumn column in table.Columns)
                {
                    column.ReadOnly = true;
                }
                object[] wiersz = new object[2];
                foreach (Subjects item in subjects)
                {
                    wiersz[0] = item.Id;
                    wiersz[1] = item.Name;
                    table.Rows.Add(wiersz);
                    
                }
                Dane.ItemsSource = table.DefaultView;
                napis.Text = "Edycja przedmiotów";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd startowy!" + ex.Message);
            }
        }
        private void Hide(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Id")
            {
                e.Cancel = true;
            }
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            Insert.IsEnabled = true;
            Update.IsEnabled = false;
            Delete.IsEnabled = false;
            Name.Text = "";
            Id = -1;
            Initialize();
        }
        public static async Task<bool> AddSubject(string name)
        {
            object data = new
            {
                name
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("https://localhost:44379/api/subjects/", content);
                int code = (int)response.StatusCode;
                if (code >= 200 && code < 300)
                {
                    return true;
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task<bool> ModifySubject(int id, string name)
        {
            object data = new
            {
                name
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync("https://localhost:44379/api/subjects/" + id.ToString(), content);
                int code = (int)response.StatusCode;
                if (code >= 200 && code < 300)
                {
                    return true;
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task<bool> DeleteSubject(int id)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync("https://localhost:44379/api/subjects/" + id.ToString());
                int code = (int)response.StatusCode;
                if (code >= 200 && code < 300)
                {
                    return true;
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private async void Create(object sender, RoutedEventArgs e)
        {

            if (Name.Text == "")
            {
                MessageBox.Show("Proszę wpisać nazwę!");
                return;
            }
            try
            {
                if (await AddSubject(Name.Text))
                {
                    MessageBox.Show("Pomyślnie dodano!");
                    Initialize();
                    Clear(sender, e);
                }
                else
                {
                    MessageBox.Show("Błąd dodawania!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd dodawania!");
            }
        }

        private async void Modify(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "")
            {
                MessageBox.Show("Proszę wpisać nazwę!");
                return;
            }
            try
            {
                if (await ModifySubject(Id, Name.Text))
                {
                    MessageBox.Show("Pomyślnie zmieniono!");
                    Initialize();
                    Clear(sender, e);
                }
                else
                {
                    MessageBox.Show("Błąd zmiany!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd zmiany!");
            }
        }

        private async void Delet(object sender, RoutedEventArgs e)
        {
            try
            {
                if (await DeleteSubject(Id))
                {
                    MessageBox.Show("Pomyślnie usunięto!");
                    Initialize();
                    Clear(sender, e);
                }
                else
                {
                    MessageBox.Show("Błąd usuwania!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd usuwania!");
            }
        }

        private void Selected(object sender, SelectionChangedEventArgs e)
        {
            DataGrid table = sender as DataGrid;
            DataRowView row = table.SelectedItem as DataRowView;
            if (row != null)
            {
                int.TryParse(row["Id"].ToString(), out int id);
                Name.Text = row["Nazwa"].ToString();
                Id = id;
                Insert.IsEnabled = false;
                Update.IsEnabled = true;
                Delete.IsEnabled = true;
                napis.Text = "Edycja: " + Name.Text;

            }
        }
    }
}


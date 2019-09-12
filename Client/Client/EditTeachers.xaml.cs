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
    /// Logika interakcji dla klasy EditTeachers.xaml
    /// </summary>
    public partial class EditTeachers : Window
    {
        static Login login;
        static int Id;
        public EditTeachers(Login user)
        {
            InitializeComponent();
            ApiHelper.Token = user.Token;
            ApiHelper.InitializeClient();
            login = user;
            Initialize();
        }
        public static async Task<Teachers[]> LoadTeachers()
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:44379/api/teachers");

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Teachers[] jsonObject = JsonConvert.DeserializeObject<Teachers[]>(result);
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
                Teachers[] teachers = await LoadTeachers();
                DataTable table = new DataTable();
                table.Columns.Add("Id", typeof(int));
                table.Columns.Add("Tytuł", typeof(string));
                table.Columns.Add("Imie", typeof(string));
                table.Columns.Add("Nazwisko", typeof(string));
                foreach (DataColumn column in table.Columns)
                {
                    column.ReadOnly = true;
                }
                object[] wiersz = new object[4];
                foreach (Teachers item in teachers)
                {
                    wiersz[0] = item.Id;
                    wiersz[1] = item.Title;
                    wiersz[2] = item.Name;
                    wiersz[3] = item.Surname;
                    table.Rows.Add(wiersz);

                }
                Dane.ItemsSource = table.DefaultView;
                napis.Text = "Edycja prowadzących";
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
            Surname.Text = "";
            Title.Text = "";
            Id = -1;
            Initialize();
        }
        public static async Task<bool> AddTeacher(string name, string surname, string title)
        {
            object data = new
            {
                name,
                surname,
                title
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("https://localhost:44379/api/teachers/", content);
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
        public static async Task<bool> ModifyTeacher(int id, string name, string surname, string title)
        {
            object data = new
            {
                name,
                surname,
                title
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync("https://localhost:44379/api/teachers/" + id.ToString(), content);
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
        public static async Task<bool> DeleteTeacher(int id)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync("https://localhost:44379/api/teachers/" + id.ToString());
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

            if (Name.Text == "" || Surname.Text == "" || Title.Text == "")
            {
                MessageBox.Show("Proszę wpisać imię, nazwisko oraz tytuł!");
                return;
            }
            try
            {
                if (await AddTeacher(Name.Text, Surname.Text, Title.Text))
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
            if (Name.Text == "" || Surname.Text == "" || Title.Text == "")
            {
                MessageBox.Show("Proszę wpisać imię, nazwisko oraz tytuł!");
                return;
            }
            try
            {
                if (await ModifyTeacher(Id, Name.Text, Surname.Text, Title.Text))
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
                if (await DeleteTeacher(Id))
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
                Name.Text = row["Imie"].ToString();
                Surname.Text = row["Nazwisko"].ToString();
                Title.Text = row["Tytuł"].ToString();
                Id = id;
                Insert.IsEnabled = false;
                Update.IsEnabled = true;
                Delete.IsEnabled = true;
                napis.Text = "Edycja: " + Title.Text + " " + Name.Text + " " + Surname.Text;

            }
        }
    }
}

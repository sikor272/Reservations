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
    /// Logika interakcji dla klasy EditReservations.xaml
    /// </summary>
    public partial class EditReservations : Window
    {
        static Login login;
        static int Id;
        public EditReservations(Login user)
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
        public static async Task<Subjects> SubjectById(string id)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:44379/api/subjects/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Subjects jsonObject = JsonConvert.DeserializeObject<Subjects>(result);
                    return jsonObject;
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
        public static async Task<Rooms[]> LoadRooms()
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:44379/api/rooms");

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Rooms[] jsonObject = JsonConvert.DeserializeObject<Rooms[]>(result);
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
        public static async Task<Rooms> RoomById(string id)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:44379/api/rooms/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Rooms jsonObject = JsonConvert.DeserializeObject<Rooms>(result);
                    return jsonObject;
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
        public static async Task<Teachers> TeacherById(string id)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:44379/api/teachers/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Teachers jsonObject = JsonConvert.DeserializeObject<Teachers>(result);
                    return jsonObject;
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
        public static async Task<Reservations[]> LoadReservations()
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:44379/api/reservations");

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Reservations[] jsonObject = JsonConvert.DeserializeObject<Reservations[]>(result);
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
                Reservations[] reservations = await LoadReservations();
                DataTable table = new DataTable();
                table.Columns.Add("Id", typeof(int));
                table.Columns.Add("Teacher_id", typeof(int));
                table.Columns.Add("Prowadzący", typeof(string));
                table.Columns.Add("Subject_id", typeof(int));
                table.Columns.Add("Przedmiot", typeof(string));
                table.Columns.Add("Room_id", typeof(int));
                table.Columns.Add("Sala", typeof(string));
                table.Columns.Add("Data", typeof(string));
                table.Columns.Add("Początek", typeof(int));
                table.Columns.Add("Koniec", typeof(int));
                foreach (DataColumn column in table.Columns)
                {
                    column.ReadOnly = true;
                }
                object[] wiersz = new object[10];
                foreach (Reservations item in reservations)
                {
                    wiersz[0] = item.Id;
                    wiersz[1] = item.Teacher_id;
                    wiersz[2] = (await TeacherById(item.Teacher_id.ToString())).ToString();
                    wiersz[3] = item.Subject_id;
                    wiersz[4] = (await SubjectById(item.Subject_id.ToString())).ToString();
                    wiersz[5] = item.Room_id;
                    wiersz[6] = (await RoomById(item.Room_id.ToString())).ToString();
                    wiersz[7] = item.Date.ToString("dd-MM-yyyy");
                    wiersz[8] = item.Begin;
                    wiersz[9] = item.End;
                    table.Rows.Add(wiersz);

                }
                Dane.ItemsSource = table.DefaultView;
                napis.Text = "Edycja rezerwacji";

                Subjects[] subjects = await LoadSubjects();
                Subject.Items.Clear();
                foreach (Subjects item in subjects)
                {
                    Subject.Items.Add(item.ComboItem());
                }
                Subject.SelectedIndex = 0;

                Rooms[] rooms = await LoadRooms();
                Room.Items.Clear();
                foreach (Rooms item in rooms)
                {
                    Room.Items.Add(item.ComboItem());
                }
                Room.SelectedIndex = 0;

                Teachers[] teachers = await LoadTeachers();
                Teacher.Items.Clear();
                foreach (Teachers item in teachers)
                {
                    Teacher.Items.Add(item.ComboItem());
                }
                Teacher.SelectedIndex = 0;
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
            if (propertyDescriptor.DisplayName == "Id" || propertyDescriptor.DisplayName == "Teacher_id" ||
                propertyDescriptor.DisplayName == "Subject_id" || propertyDescriptor.DisplayName == "Room_id")
            {
                e.Cancel = true;
            }
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            Insert.IsEnabled = true;
            Update.IsEnabled = false;
            Delete.IsEnabled = false;
            Begin.Text = "";
            End.Text = "";
            Data.Text = "";
            Id = -1;
            Initialize();
        }
        public static async Task<bool> AddReservation(int teacher_id, int subject_id, int room_id,  string date, int begin, int end)
        {
            object data = new
            {
                teacher_id,
                subject_id,
                room_id,
                date,
                begin,
                end
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("https://localhost:44379/api/reservations/", content);
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
        public static async Task<bool> ModifyReservation(int id, int teacher_id, int subject_id, int room_id, string date, int begin, int end)
        {
            object data = new
            {
                teacher_id,
                subject_id,
                room_id,
                date,
                begin,
                end
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync("https://localhost:44379/api/reservations/" + id.ToString(), content);
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
        public static async Task<bool> DeleteReservation(int id)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync("https://localhost:44379/api/reservations/" + id.ToString());
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

            if (Begin.Text == "" || End.Text == "" || Data.Text == "")
            {
                MessageBox.Show("Proszę wybrać datę oraz wpisać godzinę rozpoczęcia i zakończenia!");
                return;
            }
            try
            {
                
                int.TryParse(Begin.Text, out int begin);
                int.TryParse(End.Text, out int end);
                if (await AddReservation(((Models.ComboBoxItem)Teacher.SelectedValue).Value,
                                            ((Models.ComboBoxItem)Subject.SelectedValue).Value,
                                            ((Models.ComboBoxItem)Room.SelectedValue).Value,
                                            ((DateTime)Data.SelectedDate).ToString("yyyy-MM-dd"), begin, end))
                {
                    MessageBox.Show("Pomyślnie dodano!");
                    Initialize();
                    Clear(sender, e);
                }
                else
                {
                    MessageBox.Show("Sala, prowadzący lub dany przedmiot już trwa w tym czasie!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd dodawania!");
            }
        }

        private async void Modify(object sender, RoutedEventArgs e)
        {
            if(Begin.Text == "" || End.Text == "" || Data.Text == "")
            {
                MessageBox.Show("Proszę wybrać datę oraz wpisać godzinę rozpoczęcia i zakończenia!");
                return;
            }
            try
            {
                int.TryParse(Begin.Text, out int begin);
                int.TryParse(End.Text, out int end);
                if (await ModifyReservation(Id, ((Models.ComboBoxItem)Teacher.SelectedValue).Value,
                                            ((Models.ComboBoxItem)Subject.SelectedValue).Value,
                                            ((Models.ComboBoxItem)Room.SelectedValue).Value,
                                            ((DateTime)Data.SelectedDate).ToString("yyyy-MM-dd"), begin, end))
                {
                    MessageBox.Show("Pomyślnie zmieniono!");
                    Initialize();
                    Clear(sender, e);
                }
                else
                {
                    MessageBox.Show("Sala, prowadzący lub dany przedmiot już trwa w tym czasie!");
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
                if (await DeleteReservation(Id))
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
                int.TryParse(row["Teacher_id"].ToString(), out int T_id);
                int.TryParse(row["Subject_id"].ToString(), out int S_id);
                int.TryParse(row["Room_id"].ToString(), out int R_id);
                Begin.Text = row["Początek"].ToString();
                End.Text = row["Koniec"].ToString();
                Data.SelectedDate = DateTime.Parse(row["Data"].ToString());

                foreach (Models.ComboBoxItem item in Room.Items)
                {
                    if (item.Value == R_id)
                    {
                        Room.SelectedValue = item;
                        break;
                    }
                }
                foreach (Models.ComboBoxItem item in Teacher.Items)
                {
                    if (item.Value == T_id)
                    {
                        Teacher.SelectedValue = item;
                        break;
                    }
                }
                foreach (Models.ComboBoxItem item in Subject.Items)
                {
                    if (item.Value == R_id)
                    {
                        Subject.SelectedValue = item;
                        break;
                    }
                }
                Id = id;
                Insert.IsEnabled = false;
                Update.IsEnabled = true;
                Delete.IsEnabled = true;
                napis.Text = "Edycja: " + Subject.SelectedValue + " " + Teacher.SelectedValue + " " + Room.SelectedValue;

            }
        }
    }
}

using ApiControl;
using Models;
using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Logika interakcji dla klasy View.xaml
    /// </summary>
    public partial class View : Window
    {
        public View()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            Initialize();
            InitializeGrid();
        }
        public void InitializeGrid()
        {
            Plan.Children.Clear();
            for (int i = 0; i < 13; i++)
            {
                RowDefinition wiersz = new RowDefinition();
                wiersz.Height = GridLength.Auto;
                Plan.RowDefinitions.Add(wiersz);
            }
            ColumnDefinition kolumna = new ColumnDefinition();
            kolumna.Width = GridLength.Auto;
            Plan.ColumnDefinitions.Add(kolumna);
            Plan.Children.Add((new TextBlockItem("Godzina || Data", 0, 0)).Element());
            for (int i = 8, j = 1; i < 20; i++, j++)
            {
                Plan.Children.Add((new TextBlockItem(i.ToString() + ".00 - " + (i + 1).ToString() + ".00", 0, j)).Element());
            }
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
        public static async Task<Reservations[]> LoadReservationsTeacher(string id)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:44379/api/reservations/teacher/" + id);

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
        public static async Task<Reservations[]> LoadReservationsSubject(string id)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:44379/api/reservations/subject/" + id);

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
        public static async Task<Reservations[]> LoadReservationsRoom(string id)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:44379/api/reservations/room/" + id);

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
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Close();
            main.Show();
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            View next = new View();
            next.Show();
        }

        private async void SubjectReservation(object sender, RoutedEventArgs e)
        {
            try
            {
                Reservations[] subjects = await LoadReservationsSubject(((Models.ComboBoxItem)Subject.SelectedValue).Value.ToString());
                info.Text = "Plan dla przedmiotu: " + Subject.SelectedValue;
                UpdateGrid(subjects);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void TeacherReservation(object sender, RoutedEventArgs e)
        {
            try
            {
                Reservations[] teachers = await LoadReservationsTeacher(((Models.ComboBoxItem)Teacher.SelectedValue).Value.ToString());
                info.Text = "Plan dla nauczyciela: " + Teacher.SelectedValue;
                UpdateGrid(teachers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void RoomReservation(object sender, RoutedEventArgs e)
        {
            try
            {
                Reservations[] rooms = await LoadReservationsRoom(((Models.ComboBoxItem)Room.SelectedValue).Value.ToString());
                info.Text = "Plan dla sali: " + Room.SelectedValue;
                UpdateGrid(rooms);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private async void UpdateGrid(Reservations[] reservations)
        {
            InitializeGrid();
            string date = "01-01-2000";
            int count_date = 0;
            foreach(Reservations item in reservations)
            {
                if(item.Date.ToString("dd-MM-yyyy") != date)
                {
                    date = item.Date.ToString("dd-MM-yyyy");
                    count_date++;
                    ColumnDefinition kolumna = new ColumnDefinition();
                    kolumna.Width = GridLength.Auto;
                    Plan.ColumnDefinitions.Add(kolumna);
                    Plan.Children.Add((new TextBlockItem(date, count_date, 0)).Element());
                }
                Teachers teacher = await TeacherById(item.Teacher_id.ToString());
                Subjects subject = await SubjectById(item.Subject_id.ToString());
                Rooms room = await RoomById(item.Room_id.ToString());
                Button przycisk = new Button
                {
                    Content = subject.ToString() + '\n' +
                                teacher.ToString() + '\n' +
                                   '[' + room.ToString() + ']',
                    FontSize = 10,
                    Uid = item.Id.ToString(),
                    Margin = new Thickness(2)
                };
                Grid.SetColumn(przycisk, count_date);
                Grid.SetRow(przycisk, item.Begin - 7);
                Grid.SetRowSpan(przycisk, item.End - item.Begin);
                Plan.Children.Add(przycisk);
            }
        }
    }
}

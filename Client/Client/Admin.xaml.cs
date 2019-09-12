using ApiControl;
using Models;
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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Logika interakcji dla klasy Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        static Login login;
        public Admin(Login user)
        {
            InitializeComponent();
            ApiHelper.Token = user.Token;
            ApiHelper.InitializeClient();
            login = user;
        }

        private void ReservationEdit(object sender, RoutedEventArgs e)
        {
            EditReservations main = new EditReservations(login);
            main.Show();
        }

        private void TeacherEdit(object sender, RoutedEventArgs e)
        {
            EditTeachers main = new EditTeachers(login);
            main.Show();
        }

        private void RoomEdit(object sender, RoutedEventArgs e)
        {
            EditRooms main = new EditRooms(login);
            main.Show();
        }

        private void SubjectEdit(object sender, RoutedEventArgs e)
        {
            EditSubjects main = new EditSubjects(login);
            main.Show();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Close();
            main.Show();
        }
    }
}

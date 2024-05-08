using konyvtar.BusinessLogic;
using konyvtar.Model;
using konyvtar.ViewModel;
using Microsoft.EntityFrameworkCore;
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

namespace konyvtar.View
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        EmployeeVM VM;
        public EmployeePage(Employee employee, string mode)
        {
            InitializeComponent();
            VM = FindResource("VM") as EmployeeVM;
            VM.InitLogic(new BookLogic(), new UserLogic());
            VM.Employee = employee;
            if (mode == "user")
            {
                ChangeTB.Text = "Könyvek";
                ChangeImg.Source = new BitmapImage(new Uri(@"/View/NewBook.png", UriKind.Relative));
                BookCommands.Visibility = Visibility.Collapsed;
                UserCommands.Visibility = Visibility.Visible;
                UsersListbox.Visibility = Visibility.Visible;
                BooksListbox.Visibility = Visibility.Collapsed;
            }
        }

        //Kijelentkezés
        private void LogOut(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EmployeeLogInPage();
            ErrorWindow w = new ErrorWindow("Sikeres kijelentkezés!", true);
            w.ShowDialog();
        }

        //Oldal megváltoztatása
        private void Change(object sender, RoutedEventArgs e)
        {
            if (ChangeTB.Text == "Ügyfelek")
            {
                ChangeTB.Text = "Könyvek";
                ChangeImg.Source = new BitmapImage(new Uri(@"/View/NewBook.png", UriKind.Relative));
                BookCommands.Visibility = Visibility.Collapsed;
                UserCommands.Visibility = Visibility.Visible;
                UsersListbox.Visibility = Visibility.Visible;
                BooksListbox.Visibility = Visibility.Collapsed;
            }
            else
            {
                ChangeTB.Text = "Ügyfelek";
                ChangeImg.Source = new BitmapImage(new Uri(@"/View/New.png", UriKind.Relative));
                BookCommands.Visibility = Visibility.Visible;
                UserCommands.Visibility = Visibility.Collapsed;
                UsersListbox.Visibility = Visibility.Collapsed;
                BooksListbox.Visibility = Visibility.Visible;
            }
        }
    }
}

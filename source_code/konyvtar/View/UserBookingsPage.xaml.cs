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
    /// Interaction logic for UserBookingsPage.xaml
    /// </summary>
    public partial class UserBookingsPage : Page
    {
        UserBookingsVM VM;

        public UserBookingsPage(User user, Employee employee)
        {
            InitializeComponent();
            VM = FindResource("VM") as UserBookingsVM;
            VM.User = user;
            VM.Employee = employee;
            VM.Init(new BookingsLogic());
        }

        //Vissza gomb
        private void Back(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EmployeePage(VM.Employee, "user");
        }
    }
}

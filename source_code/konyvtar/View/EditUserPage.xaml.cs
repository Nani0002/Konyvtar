using konyvtar.Model;
using konyvtar.ViewModel;
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
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        Employee employee;
        EditUserVM VM;
        bool isNew;
        ErrorWindow w;
        public EditUserPage(User user,Employee employee)
        {
            InitializeComponent();
            VM = FindResource("VM") as EditUserVM;
            if(user.Name == null ||user.Name == "")
            {
                //Új ügyfél beállítása
                isNew = true;
                VM.User = user;
                DOBTextBox.Text = "";
            }
            else
            {
                //Meglévő ügyfél beállítása
                VM.User = user;
                NameLabel.Visibility = Visibility.Hidden;
                NameImage.Visibility = Visibility.Hidden;
                PhoneLabel.Visibility = Visibility.Hidden;
                PhoneImage.Visibility = Visibility.Hidden;
                EmailLabel.Visibility = Visibility.Hidden;
                EmailImage.Visibility = Visibility.Hidden;
                AdressLabel.Visibility = Visibility.Hidden;
                AdressImage.Visibility = Visibility.Hidden;
                IDCardLabel.Visibility = Visibility.Hidden;
                IDCardImage.Visibility = Visibility.Hidden;
                DOBLabel.Visibility = Visibility.Hidden;
                DOBImage.Visibility = Visibility.Hidden;
            }
        }

        //Mentés Gomb
        private void AddUser(object sender, RoutedEventArgs e)
        {
            //Ellenőrzés
            if (VM.User.Name == null)
            {
                w = new ErrorWindow("Név megadása kötelező!", true);
                w.ShowDialog();
                return;
            }
            if (VM.User.Email == null)
            {
                w = new ErrorWindow("Email cím megadása kötelező!", true);
                w.ShowDialog();
                return;
            }
            if (VM.User.Email.Split('@').Length < 2 || VM.User.Email.Split('@')[1].Split('.').Length < 2)
            {
                w = new ErrorWindow("Hibás email cím formátum!", true);
                w.ShowDialog();
                return;
            }
            if (VM.User.Phone == null)
            {
                w = new ErrorWindow("Telefonszám megadása kötelező", true);
                w.ShowDialog();
                return;
            }
            if (VM.User.Phone[0] != '+')
            {
                w = new ErrorWindow("A telefonszám +-al kell kezdődjön!", true);
                w.ShowDialog();
                return;
            }
            if (VM.User.Address == null)
            {
                w = new ErrorWindow("Lakcím megadása kötelező!", true);
                w.ShowDialog();
                return;
            }
            if (VM.User.Idcard == null)
            {
                w = new ErrorWindow("Igazolványszám megadása kötelező!", true);
                w.ShowDialog();
                return;
            }
            //Mentés
            konyvtarContext ctx = new konyvtarContext();
            if (isNew)
            {
                ctx.Users.Add(VM.User);
            }
            else
            {
                User u = (from user in ctx.Users
                          where user.Id == VM.User.Id
                          select user).ToList().Single();
                u.Name = VM.User.Name;
                u.Phone = VM.User.Phone;
                u.Email = VM.User.Email;
                u.Address = VM.User.Address;
                u.Idcard = VM.User.Idcard;
                u.Dob = VM.User.Dob;
                u.Bookings = VM.User.Bookings;
            }
            ctx.SaveChanges();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EmployeePage(employee, "user");
        }

        //Vissza Gomb
        private void Cancel(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EmployeePage(employee, "user");
        }

        //Adatbeviteli mezők hátterének megváltoztatása
        private void FormGotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Name == "NTextBox")
            {
                NameLabel.Visibility = Visibility.Hidden;
                NameImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "PTextBox")
            {
                PhoneLabel.Visibility = Visibility.Hidden;
                PhoneImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "ETextBox")
            {
                EmailLabel.Visibility = Visibility.Hidden;
                EmailImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "ATextBox")
            {
                AdressLabel.Visibility = Visibility.Hidden;
                AdressImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "IDTextBox")
            {
                IDCardLabel.Visibility = Visibility.Hidden;
                IDCardImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "DOBTextBox")
            {
                DOBLabel.Visibility = Visibility.Hidden;
                DOBImage.Visibility = Visibility.Hidden;
            }
        }

        private void FormLostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Name == "NTextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    NameLabel.Visibility = Visibility.Visible;
                    NameImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "PTextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    PhoneLabel.Visibility = Visibility.Visible;
                    PhoneImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "ETextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    EmailLabel.Visibility = Visibility.Visible;
                    EmailImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "ATextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    AdressLabel.Visibility = Visibility.Visible;
                    AdressImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "IDTextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    IDCardLabel.Visibility = Visibility.Visible;
                    IDCardImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "DOBTextBox")
            {
                if (((TextBox)sender).Text == "")
                { 
                   DOBLabel.Visibility = Visibility.Visible;
                   DOBImage.Visibility = Visibility.Visible;
                }
            }
        }        
    }
}

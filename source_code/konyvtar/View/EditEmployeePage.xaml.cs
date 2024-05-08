using konyvtar.Model;
using konyvtar.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for EditEmployeePage.xaml
    /// </summary>
    public partial class EditEmployeePage : Page
    {
        EditEmployeeVM VM;
        Employee admin;
        bool isNew = false;
        ErrorWindow w;

        public EditEmployeePage(Employee employee, Employee admin)
        {
            InitializeComponent();
            VM = FindResource("VM") as EditEmployeeVM;
            if (employee.Name == null || employee.Name == "")
            {
                //Új alkalmazott beállítása
                isNew = true;
            }
            else
            {
                //Meglévő alkalmazott beállítása
                employee.Password = Decrypt(employee.Password);
                UsernameImage.Visibility = Visibility.Hidden;
                UsernameLabel.Visibility = Visibility.Hidden;
                PasswordImage.Visibility = Visibility.Hidden;
                PasswordLabel.Visibility = Visibility.Hidden;
            }
            VM.Employee = employee;
            this.admin = admin;
        }

        //Mentés Gomb
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            //Ellenőrzés
            if (VM.Employee.Name == null || VM.Employee.Name == "")
            {
                w = new ErrorWindow("Név megadása kötelező!", true);
                w.ShowDialog();
                return;
            }
            if (VM.Employee.Password == null || VM.Employee.Password == "")
            {
                w = new ErrorWindow("Jelszó megadása kötelező!", true);
                w.ShowDialog();
                return;
            }
            if (VM.Employee.Privilige != 1 && VM.Employee.Privilige != 0)
            {
                w = new ErrorWindow("Hozzáférési jog megadása kötelező!", true);
                w.ShowDialog();
                return;
            }
            konyvtarContext ctx = new konyvtarContext();
            //Mentés
            if (isNew)
            {
                VM.Employee.Password = Encrypt(VM.Employee.Password);
                ctx.Employees.Add(VM.Employee);
            }
            else
            {
                if (VM.Employee.Id == admin.Id && VM.Employee.Privilige != 1)
                {
                    w = new ErrorWindow("Az éppen bejelentkezett alkalmazott\njoga nem megváltoztatható!", true);
                    w.ShowDialog();
                    VM.Employee.Privilige = 1;
                    return;
                }
                VM.Employee.Password = Encrypt(VM.Employee.Password);
                Employee q = (from employee in ctx.Employees
                              where employee.Id == VM.Employee.Id
                              select employee).ToList().Single();
                q.Name = VM.Employee.Name;
                q.Privilige = VM.Employee.Privilige;
                q.Password = VM.Employee.Password;
            }
            ctx.SaveChanges();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new AdminPage(admin);
        }

        //Vissza Gomb
        private void Cancel(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new AdminPage(admin);
        }

        //Adatbeviteli mezők hátterének megváltoztatása
        private void FormGotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Name == "PWTextBox")
            {
                PasswordLabel.Visibility = Visibility.Hidden;
                PasswordImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "UNTextBox")
            {
                UsernameLabel.Visibility = Visibility.Hidden;
                UsernameImage.Visibility = Visibility.Hidden;
            }
        }

        private void FormLostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Name == "PWTextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    PasswordLabel.Visibility = Visibility.Visible;
                    PasswordImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "UNTextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    UsernameLabel.Visibility = Visibility.Visible;
                    UsernameImage.Visibility = Visibility.Visible;
                }
            }
        }


        //Titkosítás
        static string Key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";

        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}

using konyvtar.BusinessLogic;
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
using System.Security.Cryptography;

namespace konyvtar.View
{
    /// <summary>
    /// Interaction logic for EmployeeLogInPage.xaml
    /// </summary>
    public partial class EmployeeLogInPage : Page
    {
        EmployeeLogInVM VM;
        public EmployeeLogInPage()
        {
            InitializeComponent();
            VM = FindResource("VM") as EmployeeLogInVM;
            VM.InitLogic(new LogInLogic());
        }

        //Jelszó megváltozásának beállítása
        private void PB_KeyUp(object sender, KeyEventArgs e)
        {
            VM.Password = ((PasswordBox)sender).Password;
            VM.Password = Encrypt(VM.Password);
        }

        //Adatbeviteli mezők hátterének megváltoztatása
        private void LoginGotFocus(object sender, RoutedEventArgs e)
        {
            if(sender is PasswordBox)
            {
                PasswordLabel.Visibility = Visibility.Hidden;
                PasswordImage.Visibility = Visibility.Hidden;
            }
            else if(sender is TextBox)
            {
                UsernameLabel.Visibility = Visibility.Hidden;
                UsernameImage.Visibility = Visibility.Hidden;
            }
        }

        private void LoginLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox)
            {
                if((sender as PasswordBox).Password == "")
                {
                    PasswordLabel.Visibility = Visibility.Visible;
                    PasswordImage.Visibility = Visibility.Visible;
                }
            }
            else if (sender is TextBox)
            {
                if ((sender as TextBox).Text == "")
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

    }
}

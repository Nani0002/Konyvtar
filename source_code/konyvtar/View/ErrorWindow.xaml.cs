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

namespace konyvtar.View
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(string message, bool ok)
        {
            InitializeComponent();
            MSG.Content = message;
            if (ok)
            {
                NoBorder.Visibility = Visibility.Collapsed;
                YesButton.Content = "OK";
            }
        }

        private void No(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Yes(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}

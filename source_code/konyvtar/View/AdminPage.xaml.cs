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

namespace konyvtar.View
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        AdminVM VM;
        public AdminPage(Employee admin)
        {
            InitializeComponent();
            VM = FindResource("VM") as AdminVM;
            VM.Admin = admin;
            VM.InitLogic(new AdminLogic());
        }

    }
}

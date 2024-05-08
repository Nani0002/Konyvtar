using konyvtar.BusinessLogic;
using konyvtar.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace konyvtar.ViewModel
{
    class EmployeeLogInVM : ObservableObject
    {
        string username;
        public string Username { get => username; set => SetProperty(ref username, value); }

        string password;
        public string Password { get => password; set => SetProperty(ref password, value); }


        public ICommand LogIn { get; private set; }

        public EmployeeLogInVM()
        {
            LogIn = new RelayCommand(() => this.logic.LogIn(username, password));
        }

        ILogInLogic logic;

        public void InitLogic(LogInLogic logic)
        {
            this.logic = logic;
        }
    }
}

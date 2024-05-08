using konyvtar.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konyvtar.ViewModel
{
    class EditUserVM : ObservableObject
    {
        private User user;

        public User User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }
    }
}

using konyvtar.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konyvtar.ViewModel
{
    class EditEmployeeVM : ObservableObject
    {
        private Employee employee;

        public Employee Employee
        {
            get { return employee; }
            set { SetProperty(ref employee, value); }
        }
    }
}

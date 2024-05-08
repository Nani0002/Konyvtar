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
    class AdminVM : ObservableObject
    {
        private Employee admin;

        public Employee Admin
        {
            get { return admin; }
            set { SetProperty(ref admin, value); }
        }

        private ObservableCollection<Employee> employees;

        public ObservableCollection<Employee> Employees
        {
            get 
            { 
                if(search != null && search != "")
                {
                    List<Employee> list = employees.Where(x => x.Name.ToLower().Contains(Search.ToLower())).ToList();
                    return new ObservableCollection<Employee>(list);
                }
                else
                {
                    return employees;
                }
            }
            set { SetProperty(ref employees, value); }
        }

        private Employee selectedEmployee;

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set { SetProperty(ref selectedEmployee, value); }
        }

        private string search;

        public string Search
        {
            get { return search; }
            set 
            { 
                SetProperty(ref search, value);
                OnPropertyChanged(nameof(Employees));
            }
        }

        public ICommand AddEmployee { get; private set; }
        public ICommand EditEmployee { get; private set; }
        public ICommand DeleteEmployee { get; private set; }
        public ICommand LogOut { get; private set; }

        public AdminVM()
        {
            konyvtarContext ctx = new konyvtarContext();
            employees = new ObservableCollection<Employee>(ctx.Employees);

            AddEmployee = new RelayCommand(() => this.logic.AddEmployee(Admin));
            EditEmployee = new RelayCommand(() => this.logic.EditEmployee(SelectedEmployee, Admin));
            DeleteEmployee = new RelayCommand(() => this.logic.DeleteEmployee(SelectedEmployee, Admin));
            LogOut = new RelayCommand(() => this.logic.LogOut());
        }

        IAdminLogic logic;

        public void InitLogic(AdminLogic logic)
        {
            this.logic = logic;
        }
    }
}

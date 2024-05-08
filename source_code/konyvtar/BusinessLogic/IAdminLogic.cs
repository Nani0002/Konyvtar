using konyvtar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konyvtar.BusinessLogic
{
    interface IAdminLogic
    {
        public void AddEmployee(Employee admin);
        public void EditEmployee(Employee employee, Employee admin);
        public void DeleteEmployee(Employee employee, Employee admin);
        public void LogOut();
    }
}

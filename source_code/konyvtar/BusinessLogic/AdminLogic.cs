using konyvtar.Model;
using konyvtar.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace konyvtar.BusinessLogic
{
    class AdminLogic : IAdminLogic
    {
        ErrorWindow w;

        public void AddEmployee(Employee admin)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditEmployeePage(new Employee(), admin);
        }

        public void EditEmployee(Employee employee, Employee admin)
        {
            if (employee == null)
            {
                SystemSounds.Beep.Play();
                return;
            } 
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditEmployeePage(employee, admin);
        }

        public void DeleteEmployee(Employee employee, Employee admin)
        {
            if(employee == null)
            {
                SystemSounds.Beep.Play();
                return;
            }
            if (admin.Id == employee.Id)
            {
                w = new ErrorWindow("Jelenlegi felhasználó nem törölhető!", true);
                w.ShowDialog();
                return;
            }
            konyvtarContext ctx = new konyvtarContext();
            ctx.Employees.Remove(employee);
            ctx.SaveChanges();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new AdminPage(admin);
        }

        public void LogOut()
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EmployeeLogInPage();
        }
    }
}

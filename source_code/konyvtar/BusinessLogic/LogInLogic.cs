using konyvtar.Model;
using konyvtar.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace konyvtar.BusinessLogic
{
    class LogInLogic : ILogInLogic
    {
        ErrorWindow w;

        public void LogIn(string username, string password)
        {
            if(username == null || password == null)
            {
                w = new ErrorWindow("Felhasználónév és jelszó megadása kötelező!", true);
                w.ShowDialog();
                return;
            }
            else
            {
                konyvtarContext ctx = new konyvtarContext();
                try
                {
                    Employee e = ctx.Employees.First();
                }
                catch
                {
                    ErrorWindow w = new ErrorWindow("Adatbázis hiba!", true);
                    w.ShowDialog();
                    Application.Current.Shutdown();
                    return;
                }
                foreach (Employee employee in ctx.Employees)
                {
                    if(employee.Name == username && employee.Password == password)
                    {
                        if (employee.Privilige == 0)
                        {
                            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EmployeePage(employee, "");
                        }
                        else if (employee.Privilige == 1)
                        {
                            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new AdminPage(employee);
                        }
                        w = new ErrorWindow("Sikeres bejelentkezés!", true);
                        w.Title = "Success";
                        w.ErrorLabel.Content = "";
                        w.ShowDialog();
                        return;
                    }
                }
                w = new ErrorWindow("Hibás felhasználónév vagy jelszó!", true);
                w.Title = "Skertelen bejelentkezés!";
                w.ShowDialog();
            }
        }
    }
}

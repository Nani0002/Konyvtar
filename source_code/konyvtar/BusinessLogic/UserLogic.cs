using konyvtar.Model;
using konyvtar.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace konyvtar.BusinessLogic
{
    class UserLogic : IUserLogic
    {
        ErrorWindow w;

        public ObservableCollection<User> GetUsers()
        {
            konyvtarContext ctx = new konyvtarContext();
            return new ObservableCollection<User>(ctx.Users.Include(x => x.Bookings));
        }

        public void AddUser(IList<User> users, Employee employee)
        {
            User user = new User();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditUserPage(user, employee);
        }

        public void DeleteUser( User user, Employee employee)
        {
            if (user == null)
            {
                w = new ErrorWindow("Nincs kiválasztva felhasnáló!", true);
                SystemSounds.Beep.Play();
                w.ShowDialog();
                return;
            }
            konyvtarContext ctx = new konyvtarContext();
            ctx.Users.Remove(user);
            ctx.SaveChanges();
            ctx.Dispose();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EmployeePage(employee, "user");

        }

        public void EditUser(IList<User> users, User user, Employee employee)
        {
            if (user == null)
            {
                w = new ErrorWindow("Nincs kiválasztva felhasnáló!", true);
                SystemSounds.Beep.Play();
                w.ShowDialog();
                return;
            }
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditUserPage(user, employee);
        }

        public void UserBookings(User user, Employee employee)
        {
            if (user == null)
            {
                w = new ErrorWindow("Nincs kiválasztva felhasnáló!", true);
                SystemSounds.Beep.Play();
                w.ShowDialog();
                return;
            }
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new UserBookingsPage(user, employee);
        }
    }
}

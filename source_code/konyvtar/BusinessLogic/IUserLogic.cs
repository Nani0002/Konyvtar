using konyvtar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konyvtar.BusinessLogic
{
    interface IUserLogic
    {
        public ObservableCollection<User> GetUsers();

        public void AddUser(IList<User> users, Employee employee);

        public void EditUser(IList<User> users, User user, Employee employee);

        public void DeleteUser(User user, Employee employee);

        public void UserBookings(User user, Employee employee);
    }
}

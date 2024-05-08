using konyvtar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konyvtar.BusinessLogic
{
    interface IBookingsLogic
    {
        ObservableCollection<Booking> GetBookings(User user);

        void AddBooking(Employee employee, User user);
        void EditBooking(Booking booking, Employee employee, User user);
        void DeleteBooking(Booking booking, Employee employee, User user);
    }
}

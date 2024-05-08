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
    class BookingsLogic : IBookingsLogic
    {
        ErrorWindow w;

        public ObservableCollection<Booking> GetBookings(User user)
        {
            konyvtarContext ctx = new konyvtarContext();
            return new ObservableCollection<Booking>(ctx.Bookings.Include(x => x.User).Where(x=> x.User.Id == user.Id).Include(x => x.Book));
        }

        public void AddBooking(Employee employee, User user)
        {
            Booking booking = new Booking();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditBookingPage(booking, user, employee);
        }

        public void EditBooking(Booking booking, Employee employee, User user)
        {
            if(booking == null)
            {
                SystemSounds.Beep.Play();
                w = new ErrorWindow("Nincs kiválasztása kölcsönzés!", true);
                w.ShowDialog();
                return;
            }
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditBookingPage(booking, user, employee);
        }

        public void DeleteBooking(Booking booking, Employee employee, User user)
        {
            if (booking == null)
            {
                SystemSounds.Beep.Play();
                w = new ErrorWindow("Nincs kiválasztása kölcsönzés!", true);
                w.ShowDialog();
                return;
            }
            konyvtarContext ctx = new konyvtarContext();
            ctx.Books.Attach(booking.Book);
            booking.Book.Amount += 1;
            ctx.Bookings.Remove(booking);
            ctx.SaveChanges();
            ctx.Dispose();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new UserBookingsPage(user, employee);
        }
    }
}

using konyvtar.BusinessLogic;
using konyvtar.Model;
using Microsoft.EntityFrameworkCore;
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
    class UserBookingsVM : ObservableObject
    {
        private Employee employee;

        public Employee Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        private User user;

        public User User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        private ObservableCollection<Booking> bookings;

        public ObservableCollection<Booking> Bookings
        {
            get { return bookings; }
            set { SetProperty(ref bookings, value); }
        }

        private Booking selectedBooking;

        public Booking SelectedBooking
        {
            get { return selectedBooking; }
            set { SetProperty(ref selectedBooking, value); }
        }

        IBookingsLogic logic;

        public ICommand NewBooking { get; private set; }
        public ICommand EditBooking { get; private set; }
        public ICommand DeleteBooking { get; private set; }

        public UserBookingsVM()
        {
            bookings = new ObservableCollection<Booking>();

            NewBooking = new RelayCommand(() => logic.AddBooking(Employee, User));
            EditBooking = new RelayCommand(() => logic.EditBooking(SelectedBooking, Employee, User));
            DeleteBooking = new RelayCommand(() => logic.DeleteBooking(SelectedBooking, Employee, User));
        }

        public void Init(BookingsLogic logic)
        {
            this.logic = logic;
            Bookings = logic.GetBookings(User);
        }
    }
}

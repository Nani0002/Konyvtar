using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

#nullable disable

namespace konyvtar.Model
{
    public partial class Booking : ObservableObject
    {
        private int id;
        private int bookid;
        private int userid;
        private DateTime startdate;
        private DateTime enddate;
        private Book book;
        private User user;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public int Bookid { get => bookid; set => SetProperty(ref bookid, value); }
        public int Userid { get => userid; set => SetProperty(ref userid, value); }
        public DateTime Startdate { get => startdate; set => SetProperty(ref startdate, value); }
        public DateTime Enddate { get => enddate; set => SetProperty(ref enddate, value); }
        public bool Legal
        {
            get
            {
                return DateTime.Now.Date < Enddate.Date;
            }
        }
        public virtual Book Book { get => book; set => SetProperty(ref book, value); }
        public virtual User User { get => user; set => SetProperty(ref user, value); }

        public Booking()
        {
            Startdate = DateTime.Now.Date;
            Enddate = DateTime.Now.Date;
        }

        public void CloneFrom(Booking booking)
        {
            Id = booking.Id;
            Book = booking.Book;
            Bookid = booking.Bookid;
            User = booking.User;
            Userid = booking.Userid;
            Startdate = booking.Startdate;
            Enddate = booking.Enddate;
        }
    }
}

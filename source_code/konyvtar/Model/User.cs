using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable

namespace konyvtar.Model
{
    public partial class User : ObservableObject
    {
        private int id;
        private string name;
        private string phone;
        private string email;
        private string address;
        private string idcard;
        private DateTime dob;
        private ICollection<Booking> bookings;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Phone { get => phone; set => SetProperty(ref phone, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Address { get => address; set => SetProperty(ref address, value); }
        public string Idcard { get => idcard; set => SetProperty(ref idcard, value); }
        public DateTime Dob { get => dob; set => SetProperty(ref dob, value); }
        public bool Free
        {
            get
            {
                if (Bookings != null)
                {
                    if (Bookings.Count == 0)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
        }
        public virtual ICollection<Booking> Bookings { get => bookings; set => SetProperty(ref bookings, value); }

        public User()
        {
            Bookings = new HashSet<Booking>();
        }
    }
}

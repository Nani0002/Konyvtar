using konyvtar.Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

#nullable disable

namespace konyvtar.Model
{
    public partial class Book : ObservableObject
    {
        private int id;
        private string title;
        private int authorid;
        private DateTime releasedate;
        private string genre;
        private byte[] img;
        private string language;
        private string description;
        private string isbn;
        private string publisher;
        private int amount;
        private Author author;
        private ICollection<Booking> bookings;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Title { get => title; set => SetProperty(ref title, value); }
        public int Authorid { get => authorid; set => SetProperty(ref authorid, value); }
        public DateTime Releasedate { get => releasedate; set => SetProperty(ref releasedate, value); }
        public string Genre { get => genre; set => SetProperty(ref genre, value); }
        public byte[] Img { get => img; set => SetProperty(ref img, value); }
        public string Language { get => language; set => SetProperty(ref language, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public string ShortDescription
        {
            get
            {
                if (description.Length < 40)
                {
                    return description;
                }
                return description.Substring(0, 40) + "...";
            }
        }
        public string Isbn { get => isbn; set => SetProperty(ref isbn, value); }
        public string Publisher { get => publisher; set => SetProperty(ref publisher, value); }
        public int Amount { get => amount; set => SetProperty(ref amount, value); }
        public virtual Author Author { get => author; set => SetProperty(ref author, value); }
        public virtual ICollection<Booking> Bookings { get => bookings; set => SetProperty(ref bookings, value); }

        public Book()
        {
            Releasedate = DateTime.Now.Date;
            Bookings = new HashSet<Booking>();
        }

        public void CloneFrom(Book b)
        {
            Id = b.Id;
            Title = b.Title;
            Authorid = b.Authorid;
            Releasedate = b.Releasedate;
            Genre = b.Genre;
            Img = b.Img;
            Language = b.Language;
            Description = b.Description;
            Isbn = b.Isbn;
            Publisher = b.Publisher;
            Amount = b.Amount;
            Author = b.Author;
            Bookings = b.Bookings;
        }
    }
}

using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

#nullable disable

namespace konyvtar.Model
{
    public partial class Author : ObservableObject
    {
        public Author()
        {
            Books = new HashSet<Book>();
            Dob = DateTime.Now.Date;
        }

        private int id;
        private string name;
        private DateTime dob;
        private string pob;
        private string nationality;
        private ICollection<Book> books;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public DateTime Dob { get => dob; set => SetProperty(ref dob, value); }
        public string Pob { get => pob; set => SetProperty(ref pob, value); }
        public string Nationality { get => nationality; set => SetProperty(ref nationality, value); }
        public virtual ICollection<Book> Books { get => books; set => SetProperty(ref books, value); }
    }
}

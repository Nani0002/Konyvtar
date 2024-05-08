using konyvtar.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konyvtar.ViewModel
{
    class EditBookingVM : ObservableObject
    {
        private Booking booking;

        public Booking Booking
        {
            get { return booking; }
            set { SetProperty(ref booking, value); }
        }

        private ObservableCollection<Book> books;

        public ObservableCollection<Book> Books
        {
            get { return books; }
            set { SetProperty(ref books, value); }
        }

        private Book selectedBook;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set { SetProperty(ref selectedBook, value); }
        }

        public void Load()
        {
            konyvtarContext ctx = new konyvtarContext();
            //Books = new ObservableCollection<Book>(ctx.Books);
            Books = new ObservableCollection<Book>();
            foreach (Book book1 in ctx.Books)
            {
                if(book1.Amount > 0 || book1.Id == Booking.Bookid)
                {
                    Books.Add(book1);
                }
            }
            bool contains = false;
            foreach (Book book in ctx.Books.Include(x => x.Author))
            {
                if(book.Id == Booking.Bookid)
                {
                    SelectedBook = book;
                    contains = true;
                    break;
                }
            }
            if (!contains)
            {
                SelectedBook = Books.First();
                Booking.Book = SelectedBook;
            }
            /**if(Booking.Book != null)
            {
                foreach (Book book in Books)
                {
                    if (book.Id == Booking.Book.Id)
                    {
                        SelectedBook = book;
                    }
                }
            }
            else
            {
                SelectedBook = Books.First();
                Booking.Book = SelectedBook;
            }*/
        }
    }
}

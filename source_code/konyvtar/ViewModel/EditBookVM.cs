using konyvtar.Data;
using konyvtar.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace konyvtar.ViewModel
{
    class EditBookVM : ObservableObject
    {

        private Book book;

        public Book Book
        {
            get
            {
                try
                {
                    return book;
                }
                catch
                {
                    return new Book();
                }
            }
            set { SetProperty(ref book, value); }
        }

        public ObservableCollection<Author> authors;

        public ObservableCollection<Author> Authors 
        {
            get { return authors; }
            set { SetProperty(ref authors, value); }
        }

        private Author selectedAuthor;

        public Author SelectedAuthor
        {
            get { return selectedAuthor; }
            set { SetProperty(ref selectedAuthor, value); }
        }

        public Array Genres
        {
            get
            {
                return Enum.GetValues(typeof(Genre));
            }
        }

        public void Load()
        {
            konyvtarContext ctx = new konyvtarContext();
            Authors = new ObservableCollection<Author>(ctx.Authors.Include(x => x.Books));
            bool contains = false;
            foreach (Author author in Authors)
            {
                if(author.Id == Book.Authorid)
                {
                    SelectedAuthor = author;
                    contains = true;
                    break;
                }
            }
            if (!contains)
            {
                SelectedAuthor = Authors.First();
                Book.Author = SelectedAuthor;
            }
        }

        /*public Author SelectedAuthor
        {
            get 
            {
                konyvtarContext ctx = new konyvtarContext();
                List<Book> books = new List<Book>(ctx.Books);
                return (from author in Authors
                        where author.Id == Book.Authorid
                        select author).First();

                if(Book != null)
                {
                    if (
                    (from author in Authors
                     where author.Id == Book.Authorid
                     select author) == null)
                    {
                        return new Author();
                    }
                    else
                    {
                        var q = from author in Authors
                                where author.Id == Book.Authorid
                                select author;
                        return q.First();
                    }
                }
                return null;
            }
            set { SetProperty(ref selectedAuthor,value); }
        }
        */

        /**public Author SelectedAuthor
        {
            get 
            {
                int i = (from author in Authors
                            where author.Id == book.Id
                            select author.Id).ToList().Single();
                foreach (Author a in Authors)
                {
                    if(a.Id == i)
                    {
                        return a;
                    }
                }
                return new Author();
            }
            set 
            {
                Book.Authorid = value.Id;
            }
        }*/

    }
}

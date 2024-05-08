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
    class BookLogic : IBookLogic
    {
        ErrorWindow w;

        public ObservableCollection<Book> GetBooks()
        {
            konyvtarContext ctx = new konyvtarContext();
            return new ObservableCollection<Book>(ctx.Books.Include(x => x.Author));
        }

        public void AddBook(Employee employee)
        {
            Book book = new Book();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditBookPage(book, employee);
        }

        public void EditBook(Book book, Employee employee)
        {
            if (book == null)
            {
                SystemSounds.Beep.Play();
                w = new ErrorWindow("Nincs kiválasztva könyv!", true);
                w.ShowDialog();
                return;
            }
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditBookPage(book, employee);
        }

        public void DeleteBook(Book book, Employee employee)
        {
            if (book == null)
            {
                SystemSounds.Beep.Play();
                w = new ErrorWindow("Nincs kiválasztva könyv!", true);
                w.ShowDialog();
                return;
            }
            konyvtarContext ctx = new konyvtarContext();
            ctx.Books.Remove(book);
            ctx.SaveChanges();
            ctx.Dispose();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EmployeePage(employee, "book");
        }
    }
}

using konyvtar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konyvtar.BusinessLogic
{
    interface IBookLogic
    {
        public ObservableCollection<Book> GetBooks();

        public void AddBook(Employee employee);

        public void EditBook(Book book, Employee employee);

        public void DeleteBook(Book book, Employee employee);
    }
}

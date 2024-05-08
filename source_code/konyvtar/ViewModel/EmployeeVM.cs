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
    class EmployeeVM : ObservableObject
    {
        public IBookLogic bookLogic;

        public IUserLogic userLogic;

        private ObservableCollection<Book> books;

        public ObservableCollection<Book> Books
        {
            get
            {
                if (search != null && search != "")
                {
                    List<Book> list = books.Where(x => x.Title.ToLower().Contains(Search.ToLower())).ToList();
                    return new ObservableCollection<Book>(list);
                }
                else
                {
                    return books;
                }
            }
            set { SetProperty(ref books, value); }
        }

        private Book selectedBook;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set { SetProperty(ref selectedBook, value); }
        }

        private ObservableCollection<User> users;

        public ObservableCollection<User> Users
        {
            get
            {
                if (search != null && search != "")
                {
                    List<User> list = users.Where(x => x.Name.ToLower().Contains(Search.ToLower())).ToList();
                    return new ObservableCollection<User>(list);
                }
                else
                {
                    return users;
                }
            }
            set { SetProperty(ref users, value); }
        }

        private User selectedUser;

        public User SelectedUser
        {
            get { return selectedUser; }
            set { SetProperty(ref selectedUser, value); }
        }

        private string search;

        public string Search
        {
            get 
            { 
                return search; 
            }
            set 
            { 
                SetProperty(ref search, value);
                OnPropertyChanged(nameof(Books));
                OnPropertyChanged(nameof(Users));
            }
        }

        private Employee employee;

        public Employee Employee
        {
            get { return employee; }
            set { SetProperty(ref employee, value); }
        }

        public ICommand AddBook { get; private set; }
        public ICommand EditBook { get; private set; }
        public ICommand DeleteBook { get; private set; }

        public ICommand AddUser { get; private set; }
        public ICommand EditUser { get; private set; }
        public ICommand DeleteUser { get; private set; }
        public ICommand UserBookings { get; private set; }

        public EmployeeVM()
        {
            AddBook = new RelayCommand(() => this.bookLogic.AddBook(Employee));
            EditBook = new RelayCommand(() => this.bookLogic.EditBook(SelectedBook, Employee));
            DeleteBook = new RelayCommand(() => this.bookLogic.DeleteBook(SelectedBook, Employee));

            AddUser = new RelayCommand(() => this.userLogic.AddUser(Users, Employee));
            EditUser = new RelayCommand(() => this.userLogic.EditUser(Users, SelectedUser, Employee));
            DeleteUser = new RelayCommand(() => this.userLogic.DeleteUser(SelectedUser, Employee));
            UserBookings = new RelayCommand(() => this.userLogic.UserBookings(SelectedUser, Employee));
        }

        public void InitLogic(BookLogic bookLogic, UserLogic userLogic)
        {
            this.bookLogic = bookLogic;
            this.userLogic = userLogic;

            Books = bookLogic.GetBooks();
            Users = userLogic.GetUsers();
        }
    }
}

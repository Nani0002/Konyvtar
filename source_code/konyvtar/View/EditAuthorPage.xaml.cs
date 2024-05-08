using konyvtar.Model;
using konyvtar.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace konyvtar.View
{
    /// <summary>
    /// Interaction logic for EditAuthorPage.xaml
    /// </summary>
    public partial class EditAuthorPage : Page
    {
        EditAuthorVM VM;
        Employee employee;
        Book book;
        bool isNew = false;

        public EditAuthorPage(Author author, Employee employee, Book b)
        {
            InitializeComponent();
            VM = FindResource("VM") as EditAuthorVM;
            this.employee = employee;
            VM.Author = author;
            book = b;
            if (author.Name == null || author.Name == "")
            {
                isNew = true;
                DTextBox.Text = "";
            }
            else
            {
                NameLabel.Visibility = Visibility.Hidden;
                NameImage.Visibility = Visibility.Hidden;
                PlaceLabel.Visibility = Visibility.Hidden;
                PlaceImage.Visibility = Visibility.Hidden;
                DateLabel.Visibility = Visibility.Hidden;
                DateImage.Visibility = Visibility.Hidden;
                NationalityLabel.Visibility = Visibility.Hidden;
                NationalityImage.Visibility = Visibility.Hidden;
            }

        }

        private void Save(object sender, RoutedEventArgs e)
        {
            konyvtarContext ctx = new konyvtarContext();
            if (isNew)
            {
                ctx.Authors.Add(VM.Author);
            }
            else
            {
                foreach (Author author in ctx.Authors)
                {
                    if(author.Id == VM.Author.Id)
                    {
                        author.Name = VM.Author.Name;
                        author.Nationality = VM.Author.Nationality;
                        author.Dob = VM.Author.Dob;
                        author.Pob = VM.Author.Pob;
                        author.Books = VM.Author.Books;
                        break;
                    }
                }
            }
            ctx.SaveChanges();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditBookPage(book, employee);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditBookPage(book, employee);
        }

        private void FormGotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Name == "NTextBox")
            {
                NameLabel.Visibility = Visibility.Hidden;
                NameImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "PTextBox")
            {
                PlaceLabel.Visibility = Visibility.Hidden;
                PlaceImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "DTextBox")
            {
                DateLabel.Visibility = Visibility.Hidden;
                DateImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "NATextBox")
            {
                NationalityLabel.Visibility = Visibility.Hidden;
                NationalityImage.Visibility = Visibility.Hidden;
            }
        }

        private void FormLostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Name == "NTextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    NameLabel.Visibility = Visibility.Visible;
                    NameImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "PTextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    PlaceLabel.Visibility = Visibility.Visible;
                    PlaceImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "DTextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    DateLabel.Visibility = Visibility.Visible;
                    DateImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "NATextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    NationalityLabel.Visibility = Visibility.Visible;
                    NationalityImage.Visibility = Visibility.Visible;
                }
            }
        }
    }
}

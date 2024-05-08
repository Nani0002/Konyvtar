using konyvtar.Model;
using konyvtar.ViewModel;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for EditBookingPage.xaml
    /// </summary>
    public partial class EditBookingPage : Page
    {
        konyvtarContext ctx;
        EditBookingVM VM;
        Employee employee;
        User user;
        bool isNew = false;

        public EditBookingPage(Booking booking, User user, Employee employee)
        {
            ctx = new konyvtarContext();
            InitializeComponent();
            VM = FindResource("VM") as EditBookingVM;
            VM.Booking = booking;
            ctx.Bookings.Attach(VM.Booking);
            if (booking.Book == null || booking.Book.Title == null)
            {
                //Új kölcsönzés beállítása
                isNew = true;
                VM.Booking.User = user;
                STextBox.Text = "";
                ETextBox.Text = "";
            }
            else
            {
                //Meglévő kölcsönzés beállítása
                StartLabel.Visibility = Visibility.Hidden;
                StartImage.Visibility = Visibility.Hidden;
                EndLabel.Visibility = Visibility.Hidden;
                EndImage.Visibility = Visibility.Hidden;
            }
            VM.Load();
            this.employee = employee;
            this.user = user;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new UserBookingsPage(user, employee);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            //Adatok ellenőrzése
            ErrorWindow w;
            if (VM.Booking.Book == null)
            {
                w = new ErrorWindow("Könyv kiválasztása kötelező!", true);
                w.ShowDialog();
                return;
            }
            if (VM.Booking.Enddate <= DateTime.Now)
            {
                w = new ErrorWindow("A kölcsönzés dátuma már eltelt! Válasszon későbbi időpontot!", true);
                w.ShowDialog();
                return;
            }
            if (VM.Booking.Startdate > VM.Booking.Enddate)
            {
                w = new ErrorWindow("A kölcsönzés kezdete később van, mint a vége!", true);
                w.ShowDialog();
                return;
            }
            foreach (Booking booking in user.Bookings)
            {
                if (isNew)
                {
                    if (booking.Bookid == VM.SelectedBook.Id)
                    {
                        w = new ErrorWindow("Ezt a könyvet már kikölcsönözte az ügyfél!", true);
                        w.ShowDialog();
                        return;
                    }
                }
                else
                {
                    if (booking.Bookid == VM.SelectedBook.Id && booking.Id != VM.Booking.Id)
                    {
                        w = new ErrorWindow("Ezt a könyvet már kikölcsönözte az ügyfél!", true);
                        w.ShowDialog();
                        return;
                    }
                }
            }
            if (isNew)
            {
                //Hozzáadás
                VM.Booking.Book = VM.SelectedBook;
                VM.Booking.Book.Amount -= 1;
                VM.Booking.Bookid = VM.SelectedBook.Id;
                VM.Booking.Userid = VM.Booking.User.Id;
                ctx.Bookings.Add(VM.Booking);
            }
            else
            {
                //Módosítás
                Book b = ctx.Books.First(x => x.Id == VM.Booking.Bookid);
                b.Amount++;
                VM.Booking.Bookid = VM.SelectedBook.Id;
                Book b2 = ctx.Books.First(x => x.Id == VM.SelectedBook.Id);
                b2.Amount--;
            }
            ctx.SaveChanges();
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new UserBookingsPage(user, employee);
        }

        private void NewBook_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditBookPage(new Book(), employee);
        }


        //Adatbeviteli mezők hátterének megváltoztatása
        private void FormGotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Name == "STextBox")
            {
                StartLabel.Visibility = Visibility.Hidden;
                StartImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "ETextBox")
            {
                EndLabel.Visibility = Visibility.Hidden;
                EndImage.Visibility = Visibility.Hidden;
            }
        }

        private void FormLostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Name == "STextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    StartLabel.Visibility = Visibility.Visible;
                    StartImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "ETextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    EndLabel.Visibility = Visibility.Visible;
                    EndImage.Visibility = Visibility.Visible;
                }
            }
        }
    }
}

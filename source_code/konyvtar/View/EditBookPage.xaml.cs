using konyvtar.Model;
using konyvtar.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for EditBookPage.xaml
    /// </summary>
    public partial class EditBookPage : Page
    {
        konyvtarContext ctx;
        EditBookVM VM;
        Employee employee;
        bool isNew = false;
        ErrorWindow w;

        public EditBookPage(Book book, Employee employee)
        {
            ctx = new konyvtarContext();
            InitializeComponent();
            VM = FindResource("VM") as EditBookVM;
            VM.Book = book;
            VM.Load();
            ctx.Books.Attach(VM.Book);
            if (book.Title == null || book.Title == "")
            {
                //Új könyv beállítása
                isNew = true;
                DATextBox.Text = "";
            }
            else
            {
                //Meglévő kölcsönzés beállítása
                TitleLabel.Visibility = Visibility.Hidden;
                TitleImage.Visibility = Visibility.Hidden;
                DescriptionLabel.Visibility = Visibility.Hidden;
                DescriptionImage.Visibility = Visibility.Hidden;
                ISBNLabel.Visibility = Visibility.Hidden;
                ISBNImage.Visibility = Visibility.Hidden;
                PublisherLabel.Visibility = Visibility.Hidden;
                PublisherImage.Visibility = Visibility.Hidden;
                LanguageLabel.Visibility = Visibility.Hidden;
                LanguageImage.Visibility = Visibility.Hidden;
                DateImage.Visibility = Visibility.Hidden;
                DateLabel.Visibility = Visibility.Hidden;
            }
            this.employee = employee;
            Back.Content = "Mégse";
            Next.Content = "Következő";
            Section1.Visibility = Visibility.Visible;
            Section2.Visibility = Visibility.Hidden;
        }

        //Kép hozzáadása
        private void AddImg(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if(file.ShowDialog() ?? false)
            {
                VM.Book.Img = File.ReadAllBytes(file.FileName);
            }
        }

        //Kép törlése
        private void RemoveImg(object sender, RoutedEventArgs e)
        {
            VM.Book.Img = File.ReadAllBytes("NoImg.png");
        }

        //Vissza gomb
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Back.Content.ToString() == "Vissza")
            {
                Next.Content = "Következő";
                Back.Content = "Mégse";
                Section1.Visibility = Visibility.Visible;
                Section2.Visibility = Visibility.Hidden;
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EmployeePage(employee, "book");
            }
        }

        //Mentés gomb
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            //1. Oldal
            if (Next.Content.ToString() == "Következő")
            {
                if (VM.Book.Title == null || VM.Book.Title.Trim() == "")
                {
                    w = new ErrorWindow("Cím megadása kötelező!", true);
                    w.ShowDialog();
                    return;
                }
                else if (VM.Book.Author == null)
                {
                    w = new ErrorWindow("Hibás szerző!", true);
                    w.ShowDialog();
                    return;
                }
                else if (VM.Book.Description == null || VM.Book.Description.Trim() == "")
                {
                    w = new ErrorWindow("Leírás megadása kötelező", true);
                    w.ShowDialog();
                    return;
                }
                else if (VM.Book.Genre == null || VM.Book.Genre == "")
                {
                    w = new ErrorWindow("Hibás műfajt adott meg!", true);
                    w.ShowDialog();
                    return;
                }
                Next.Content = "Mentés";
                Back.Content = "Vissza";
                Section1.Visibility = Visibility.Hidden;
                Section2.Visibility = Visibility.Visible;
                VM.Book.Authorid = VM.SelectedAuthor.Id;
            }
            //2. Oldal
            else
            {
                //Ellenőrzés
                if (VM.Book.Isbn == null || VM.Book.Isbn == "")
                {
                    w = new ErrorWindow("ISBN kód megadása kötelező!", true);
                    w.ShowDialog();
                    return;
                }
                if (VM.Book.Publisher == null || VM.Book.Publisher == "")
                {
                    w = new ErrorWindow("Kiadó megadása kötelező!", true);
                    w.ShowDialog();
                    return;
                }
                if (VM.Book.Language == null || VM.Book.Language == "")
                {
                    w = new ErrorWindow("Nyelv megadása kötelező!", true);
                    w.ShowDialog();
                    return;
                }
                if (VM.Book.Language.Length != 2)
                {
                    w = new ErrorWindow("A nyelvnek két karakterből álló\nkódnak kelle lennie!", true);
                }
                if (VM.Book.Img == null || VM.Book.Img.Length < 1)
                {
                    w = new ErrorWindow("Borítókép megadása kötelező!\nSzeretne üres képet hozzáadni?", false);
                    if (w.ShowDialog() ?? false)
                    {
                        VM.Book.Img = File.ReadAllBytes("NoImg.png");
                    }
                    return;
                }
                int i;
                string s = Amount.Value.ToString();
                if (!int.TryParse(s, out i))
                {
                    w = new ErrorWindow("Mennyiségnek csak számot adhat!", true);
                    w.ShowDialog();
                    return;
                }
                if (i < 0)
                {
                    w = new ErrorWindow("Negatív mennyiséget nem adhat meg!", true);
                }
                //Mentés
                if (isNew)
                {
                    ctx.Books.Add(VM.Book);
                }
                ctx.SaveChanges();
                ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EmployeePage(employee, "book");
            }
        }

        //Szerző hozzáadása
        private void NewAuthor_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditAuthorPage(new Author(), employee, VM.Book);
        }

        //Szerző módosítása
        private void EditAuthor_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new EditAuthorPage(VM.SelectedAuthor, employee, VM.Book);
        }

        //Adatbeviteli mezők hátterének megváltoztatása
        private void FormGotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Name == "TTextBox")
            {
                TitleLabel.Visibility = Visibility.Hidden;
                TitleImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "DTextBox")
            {
                DescriptionLabel.Visibility = Visibility.Hidden;
                DescriptionImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "ITextBox")
            {
                ISBNLabel.Visibility = Visibility.Hidden;
                ISBNImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "PTextBox")
            {
                PublisherLabel.Visibility = Visibility.Hidden;
                PublisherImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "LTextBox")
            {
                LanguageLabel.Visibility = Visibility.Hidden;
                LanguageImage.Visibility = Visibility.Hidden;
            }
            else if (((TextBox)sender).Name == "DATextBox")
            {
                DateLabel.Visibility = Visibility.Hidden;
                DateImage.Visibility = Visibility.Hidden;
            }
        }

        private void FormLostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Name == "TTextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    TitleLabel.Visibility = Visibility.Visible;
                    TitleImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "DTextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    DescriptionLabel.Visibility = Visibility.Visible;
                    DescriptionImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "ITextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    ISBNLabel.Visibility = Visibility.Visible;
                    ISBNImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "PTextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    PublisherLabel.Visibility = Visibility.Visible;
                    PublisherImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "LTextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    LanguageLabel.Visibility = Visibility.Visible;
                    LanguageImage.Visibility = Visibility.Visible;
                }
            }
            else if (((TextBox)sender).Name == "DATextBox")
            {
                if (((TextBox)sender).Text == "")
                {
                    DateLabel.Visibility = Visibility.Visible;
                    DateImage.Visibility = Visibility.Visible;
                }
            }
        }


    }
}

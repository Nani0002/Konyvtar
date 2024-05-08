using konyvtar.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konyvtar.ViewModel
{
    class EditAuthorVM : ObservableObject
    {
        private Author author;

        public Author Author
        {
            get { return author; }
            set { SetProperty(ref author, value); }
        }
    }
}

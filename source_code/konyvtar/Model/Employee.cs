using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

#nullable disable

namespace konyvtar.Model
{
    public partial class Employee : ObservableObject
    {
        private int id;
        private string name;
        private string password;
        private int privilige;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public int Privilige { get => privilige; set => SetProperty(ref privilige, value); }
    }
}

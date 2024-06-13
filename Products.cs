using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMyProject
{
    public class Products : INotifyPropertyChanged
    {
        private string _name;
        private string _manufacturer;
        private string _description;
        private int _quantity;  //остаток на складе
        private double _cost;
        private int basketproductquantity = 1;

        public int BasketProductQuantity
        {
            get { return basketproductquantity; }
            set
            {
                basketproductquantity = value;
                OnPropertyChanged("BasketProductQuantity");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");

            }
        }

        public string Manufacturer
        {
            get { return _manufacturer; }
            set
            {
                _manufacturer = value;
                OnPropertyChanged("Manufacturer");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public double Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                OnPropertyChanged("Cost");
            }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                OnPropertyChanged("IsAdmin");
            }
        }
        public Products() { }

        public SolidColorBrush backgrColor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

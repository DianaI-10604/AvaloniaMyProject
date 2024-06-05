using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMyProject
{
    public class Products
    {
        private string _name;
        private string _manufacturer;
        private string _description;
        private int _quantity;  //остаток на складе
        private double _cost;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
               
            }
        }

        public string Manufacturer
        {
            get { return _manufacturer; }
            set
            {
                _manufacturer = value;
                
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
               
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
               
            }
        }

        public double Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                
            }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
               
            }
        }
        public Products() { }

        public SolidColorBrush backgrColor { get; set; }
    }
}

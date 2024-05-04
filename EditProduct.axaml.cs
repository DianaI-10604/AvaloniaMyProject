using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaMyProject
{
    public partial class EditProduct : Window
    {
        private Products _product;
        public EditProduct()
        {
            InitializeComponent();
        }

        public EditProduct(Products product)
        {
            _product = product;
            InitializeComponent();
            DataContext = product;
        }

        private async void ApplyEdit_ButtonClick(object sender, RoutedEventArgs e)
        {
            string _productNameEdit = ProductNameEdit.Text;
            string _productManufacturerEdit = ProductManufacturerEdit.Text;
            string _productDescriptionEdit = ProductDescriptionEdit.Text;
            string _productQuantityEdit = ProductQuantityEdit.Text;
            string _productCostEdit = ProductCostEdit.Text;

            if (!string.IsNullOrWhiteSpace(_productNameEdit)) //если наша строка непустая
            {
                _product.Name = _productNameEdit;
            }
            if (!string.IsNullOrWhiteSpace(_productManufacturerEdit))
            {
                _product.Manufacturer = _productManufacturerEdit;
            }

            if (!string.IsNullOrWhiteSpace(_productDescriptionEdit))
            {
                _product.Description = _productDescriptionEdit;
            }
            if (!string.IsNullOrWhiteSpace(_productQuantityEdit))
            {
                _product.Quantity = Convert.ToInt32(_productQuantityEdit);
            }
            if (!string.IsNullOrWhiteSpace(_productCostEdit))
            {
                _product.Cost = Convert.ToDouble(_productCostEdit);
            }

            editResultMessage.Text = "Успешно!";

            await Task.Delay(1000);
            this.Close();
        }
    }
}

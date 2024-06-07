using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaloniaMyProject
{
    public partial class EditProduct : Window
    {
        private Products _product;
        private ManagerUserWindow _managerwindow;

        public static List<Products> EditProductsList = new List<Products> {};

        public EditProduct()
        {
            InitializeComponent();
        }

        //передаем сюда из managerUserWindow список, чтобы заново его внести в listbox после редактирования товара
        public EditProduct(Products product, ManagerUserWindow managerwindow, List<Products> ProductsList)
        {
            EditProductsList = ProductsList;
            _product = product;
            _managerwindow = managerwindow; //чтобы передать сюда listbox И обновить его

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

            //теперь нужно сделать так, чтобы менялся товар не только в списке, но и в Listbox

            if (!string.IsNullOrWhiteSpace(_productNameEdit)) //если хотя бы одна строка заполнена, то товар редактируетсяыы
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

            _managerwindow.listboxProducts.Items.Clear();

            //чтобы увидеть изменения в listbox, очищаем старый Listbox и заново добавляем товары
            //после того как наш старый список товаров обновился

            //при выполнении кода выглядит так, будто мы не очищаем список, а просто обновляем отдельные атрибуты
            foreach (var product in EditProductsList)
            {
                if (product.Quantity == 0)
                {
                    Color customColor = Color.FromRgb(102, 98, 103);
                    SolidColorBrush brush = new SolidColorBrush(customColor);
                    product.backgrColor = brush;
                }
                else
                {
                    SolidColorBrush brush = new SolidColorBrush(Colors.Transparent);
                    product.backgrColor = brush;
                }
                _managerwindow.listboxProducts.Items.Add(product);
            }

            await Task.Delay(1000);
            this.Close();
        }
    }
}

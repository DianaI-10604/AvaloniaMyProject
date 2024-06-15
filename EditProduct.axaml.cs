using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaloniaMyProject
{
    public partial class EditProduct : Window
    {
        private Products _product;
        private ManagerUserWindow _managerwindow;
        private Bitmap _imageToEdit;
        private ComboBox _combobox;


        public EditProduct()
        {
            InitializeComponent();
        }

        //передаем сюда из managerUserWindow список, чтобы заново его внести в listbox после редактирования товара
        public EditProduct(Products product, ManagerUserWindow managerwindow, ComboBox combobox)
        {
            _combobox = combobox;
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

            if (_imageToEdit != null)
            {
                _product.ProductImage = _imageToEdit;
            }
            editResultMessage.Text = "Успешно!";

            ManagerUserWindow.UpdateComboBox(Products.ProductsList, _combobox);
            ManagerUserWindow.ProductsToShow.Clear();

            foreach (var product in Products.ProductsList)
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
                ManagerUserWindow.ProductsToShow.Add(product);
            }

            await Task.Delay(1000);
            this.Close();
        }

        private async void EditImage_ButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Images", Extensions = { "png", "jpg", "jpeg", "bmp" } });

            var selectedFiles = await dialog.ShowAsync(this);

            if (selectedFiles != null && selectedFiles.Length > 0)
            {
                string imagePath = selectedFiles[0];
                _imageToEdit = new Bitmap(imagePath);
            }
        }
    }
}

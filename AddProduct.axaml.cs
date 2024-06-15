using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AvaloniaMyProject
{
    public partial class AddProduct : Window
    {
        private ManagerUserWindow _managerWindow;
        private Bitmap _selectedImage;

        public AddProduct()
        {
            InitializeComponent();
            DataContext = this;
        }

        public AddProduct(ManagerUserWindow managerWindow)
        {
            InitializeComponent();
            _managerWindow = managerWindow;
        }

        private async void AddProductImage_ButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Images", Extensions = { "png", "jpg", "jpeg", "bmp" } });

            var selectedFiles = await dialog.ShowAsync(this);

            if (selectedFiles != null && selectedFiles.Length > 0)
            {
                string imagePath = selectedFiles[0];
                _selectedImage = new Bitmap(imagePath);
            }
        }
        private async void AddProduct_ButtonClick(object sender, RoutedEventArgs e)
        {
            var brush = new SolidColorBrush(Colors.Red); //устаналиваем цвет если не все поля заполнены

            //все поля должны быть заполнены
            if (    string.IsNullOrWhiteSpace(AddProductName.Text) || string.IsNullOrEmpty(AddProductName.Text) ||
                    string.IsNullOrWhiteSpace(AddProductManufacturer.Text) || string.IsNullOrEmpty(AddProductManufacturer.Text) || 
                    string.IsNullOrWhiteSpace(AddProductDescription.Text) || string.IsNullOrEmpty(AddProductDescription.Text) ||
                    string.IsNullOrWhiteSpace(AddProductQuantity.Text) || string.IsNullOrEmpty(AddProductQuantity.Text) ||
                    string.IsNullOrWhiteSpace(AddProductCost.Text) || string.IsNullOrEmpty(AddProductCost.Text))
            {
                AddResultMessage.Text = "Не все поля заполнены";
                AddResultMessage.Foreground = brush;
            }

            //иначе: добавляем товар в переданный список товаров (исходный список)
            Products newProduct = new Products()
                {
                    Name = AddProductName.Text,
                    Manufacturer = AddProductManufacturer.Text,
                    Description = AddProductDescription.Text,
                    Quantity = Convert.ToInt32(AddProductQuantity.Text),
                    Cost = Convert.ToDouble(AddProductCost.Text),
                    ProductImage = _selectedImage
                };

            _managerWindow.UpdateProductsList(newProduct);
            


            brush = new SolidColorBrush(Colors.Green); //устаналиваем цвет если не все поля заполнены
            AddResultMessage.Foreground = brush;
            AddResultMessage.Text = "Товар добавлен!";

            await Task.Delay(1000);

            this.Close();

        }
    }
}

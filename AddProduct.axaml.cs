using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
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

        private async void AddProduct_ButtonClick(object sender, RoutedEventArgs e)
        {
            var brush = new SolidColorBrush(Colors.Red); //������������ ���� ���� �� ��� ���� ���������

            //��� ���� ������ ���� ���������
            if (    string.IsNullOrWhiteSpace(AddProductName.Text) || string.IsNullOrEmpty(AddProductName.Text) ||
                    string.IsNullOrWhiteSpace(AddProductManufacturer.Text) || string.IsNullOrEmpty(AddProductManufacturer.Text) || 
                    string.IsNullOrWhiteSpace(AddProductDescription.Text) || string.IsNullOrEmpty(AddProductDescription.Text) ||
                    string.IsNullOrWhiteSpace(AddProductQuantity.Text) || string.IsNullOrEmpty(AddProductQuantity.Text) ||
                    string.IsNullOrWhiteSpace(AddProductCost.Text) || string.IsNullOrEmpty(AddProductCost.Text))
            {
                AddResultMessage.Text = "�� ��� ���� ���������";
                AddResultMessage.Foreground = brush;
            }

            //�����: ��������� ����� � ���������� ������ ������� (�������� ������)
            Products newProduct = new Products()
                {
                    Name = AddProductName.Text,
                    Manufacturer = AddProductManufacturer.Text,
                    Description = AddProductDescription.Text,
                    Quantity = Convert.ToInt32(AddProductQuantity.Text),
                    Cost = Convert.ToDouble(AddProductCost.Text)
                };

            _managerWindow.UpdateProductsList(newProduct);
            


            brush = new SolidColorBrush(Colors.Green); //������������ ���� ���� �� ��� ���� ���������
            AddResultMessage.Foreground = brush;
            AddResultMessage.Text = "����� ��������!";

            await Task.Delay(1000);

            this.Close();

        }
    }
}

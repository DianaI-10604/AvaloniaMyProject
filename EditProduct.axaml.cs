using Avalonia.Controls;
using Avalonia.Interactivity;
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

        //�������� ���� �� managerUserWindow ������, ����� ������ ��� ������ � listbox ����� �������������� ������
        public EditProduct(Products product, ManagerUserWindow managerwindow, List<Products> ProductsList)
        {
            EditProductsList = ProductsList;
            _product = product;
            _managerwindow = managerwindow; //����� �������� ���� listbox � �������� ���

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

            //������ ����� ������� ���, ����� ������� ����� �� ������ � ������, �� � � Listbox

            if (!string.IsNullOrWhiteSpace(_productNameEdit)) //���� ���� �� ���� ������ ���������, �� ����� ���������������
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

            editResultMessage.Text = "�������!";

            _managerwindow.listboxProducts.Items.Clear();

            //����� ������� ��������� � listbox, ������� ������ Listbox � ������ ��������� ������
            //����� ���� ��� ��� ������ ������ ������� ���������

            //��� ���������� ���� �������� ���, ����� �� �� ������� ������, � ������ ��������� ��������� ��������
            foreach (var product in EditProductsList)
            {
                _managerwindow.listboxProducts.Items.Add(product);
            }

            await Task.Delay(1000);
            this.Close();
        }
    }
}

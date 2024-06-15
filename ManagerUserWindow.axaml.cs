using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Input;

namespace AvaloniaMyProject
{
    public partial class ManagerUserWindow : Window
    {
        private User _currentUser;
        private ComboBox _comboBox;

        public static ObservableCollection<Products> ProductsToShow { get; set; } = new ObservableCollection<Products>();

        //список продуктов, который мы будем потом передавать в другой список
        //public static List<Products> ProductsList { get; set; } = new List<Products> {};
        private string textToSearch = string.Empty;
        private string SortAttribute = "Default";
        private string selectedManufacturer = "Все производители";

        public ManagerUserWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ManagerUserWindow(User currentUser)
        {
            //ProductsList = productslist; 

            DataContext = this;
            _currentUser = currentUser; // Инициализация _currentUser

            
            InitializeComponent(); // Инициализация компонентов

            var comboBox = this.FindControl<ComboBox>("ManufacturersComboBox");
            // Устанавливаем начальный выбранный элемент
            _comboBox = comboBox;
            _comboBox.SelectedIndex = 0;

            FilterSearchAndSortProducts();
            UpdateComboBox(Products.ProductsList, _comboBox);


            StatusBlock.Text = "Статус: " + _currentUser.Status;
            NameBlock.Text = "Имя: " + _currentUser.Name;

            //задаем видимость кнопок Редактировать и Удалить для каждого товара в зависимости от статуса
            foreach (var product in Products.ProductsList)
            {
                product.IsAdmin = _currentUser.Status == "Admin";
            }

            //-------------------------------------------
            ProductsToShow.Clear();
            foreach (var product in Products.ProductsList) //добавляем все товары из списка в Listbox
            {
                if (product.Quantity == 0)
                {
                    Color customColor = Color.FromRgb(102, 98, 103);
                    SolidColorBrush brush = new SolidColorBrush(customColor);
                    product.backgrColor = brush;
                }

                ProductsToShow.Add(product);
            }

            //-------------------------------------------

            // Добавляем элемент в список
            Button addProductButton = this.FindControl<Button>("AddProduct");
            if (_currentUser.Status != "Admin")
            {
                addProductButton.IsVisible = false;
            }

            else if (_currentUser.Status == "гость")
            {
                NameBlock.Text = "";
            }
            else
            {
                addProductButton.IsVisible = true;
            } 
        }

        //удалить товар
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, на которую было нажатие
            Button deleteButton = (Button)sender;

            // Получаем контекст данных этой кнопки (т.е. элемент Products, к которому привязана кнопка)
            Products product = (Products)deleteButton.DataContext;

            bool IsDelete = true;
            foreach (var user in MainWindow.Users)
            {
                if (user.UserBasket.Contains(product))
                {
                    IsDelete = false;

                    DeleteProductMessageBox message = new DeleteProductMessageBox();

                    message.firstmessage.Text = "Вы не можете удалить товар, ";
                    message.secondmessage.Text = "который есть в корзине";

                    message.Show();
                }
            }

            DeleteProductCheck(IsDelete, product);

        }

        public static void UpdateComboBox(List<Products> Products, ComboBox _comboBox)
        {
            _comboBox.Items.Clear();
            _comboBox.Items.Add(new ComboBoxItem { Content = "Все производители" });
            _comboBox.SelectedIndex = 0;

            foreach (var item in Products)
            {
                string manufacturer = item.Manufacturer;

                if (!_comboBox.Items.Cast<ComboBoxItem>().Any(item => string.Equals(item.Content?.ToString(), manufacturer, StringComparison.OrdinalIgnoreCase)))
                {
                    _comboBox.Items.Add(new ComboBoxItem { Content = manufacturer });
                }
            }
        }

        private void DeleteProductCheck(bool isDelete, Products productToDelete)
        {
            if (isDelete)
            {
                Products.ProductsList.Remove(productToDelete);

                UpdateListBox(Products.ProductsList);
            }
        }

        //отредактировать товар
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = (Button)sender;

            Products editProduct = (Products)editButton.DataContext;

            //вносим в editproduct параметр того товара, в котором мы нажали на редактирование
            //а также список товаров отсюда, чтобы список товаров не очистился при переходе на другое окно
            EditProduct prod = new EditProduct(editProduct, this, _comboBox);
            prod.Show();
        }

        //это запускаем в окне AddProduct, чтобы прямо в этот список добавить товар
        public void UpdateProductsList(Products newProduct)
        {
            //ProductsList.Add(newProduct);
            newProduct.IsAdmin = _currentUser.Status == "Admin";

            if (newProduct.Quantity == 0)
            {
                Color customColor = Color.FromRgb(102, 98, 103);
                SolidColorBrush brush = new SolidColorBrush(customColor);
                newProduct.backgrColor = brush;
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush(Colors.Transparent);
                newProduct.backgrColor = brush;
            }

            //ДОБАВИЛИ ПРОИЗВОДИТЕШЛЯ В СПИСОК
            if (!_comboBox.Items.Cast<ComboBoxItem>().Any(item => (string)item.Content == newProduct.Manufacturer))
            {
                _comboBox.Items.Add(new ComboBoxItem { Content = newProduct.Manufacturer });
            }

            Products.ProductsList.Add(newProduct);
            
            UpdateListBox(Products.ProductsList);
        }

        //добавляем товар в наш список
        private void AddProduct_ButtonClick(object sender, RoutedEventArgs e)
        {
            // Передаем ссылку на исходный список товаров в окно добавления товара
            AddProduct ap = new AddProduct(this);
            ap.Show();
        }

        //возвращаемся на окно авторизации
        private void ExitToAuthorization_ButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow auth = new MainWindow();
            auth.Show();

            this.Close();
        }

        private void FilterSearchAndSortProducts()
        {
            var filteredProducts = Products.ProductsList.Where(product =>
                (selectedManufacturer == "Все производители" || product.Manufacturer == selectedManufacturer) &&
                (string.IsNullOrWhiteSpace(textToSearch) ||
                    product.Name.ToLower().Contains(textToSearch) ||
                    product.Manufacturer.ToLower().Contains(textToSearch) ||
                    product.Description.ToLower().Contains(textToSearch) ||
                    product.Cost.ToString().Contains(textToSearch))
            );

            List<Products> sortedProducts;
            switch (SortAttribute)
            {
                case "CostDesc":
                    sortedProducts = filteredProducts.OrderByDescending(item => item.Cost).ToList();
                    break;
                case "CostAsc":
                    sortedProducts = filteredProducts.OrderBy(item => item.Cost).ToList();
                    break;
                case "Manufacturer":
                    sortedProducts = filteredProducts.OrderBy(item => item.Manufacturer).ToList();
                    break;
                default:
                    sortedProducts = filteredProducts.ToList();
                    break;
            }

            UpdateListBox(sortedProducts);
        }

        private void UpdateListBox(List<Products> ListToAdd) //Обновляем список товаров
        {
            ProductsToShow.Clear();
            foreach (var item in ListToAdd)
            {
                ProductsToShow.Add(item);
            }
        }

        private void AddToBacket_Button_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, на которую было нажатие
            Button addtobasket = (Button)sender;

            // Получаем контекст данных этой кнопки (т.е. элемент Products, к которому привязана кнопка)
            Products product = (Products)addtobasket.DataContext;

            //проверка на добавление 
            if (product.Quantity == 0)
            {
                DeleteProductMessageBox message = new DeleteProductMessageBox();
                message.firstmessage.Text = "Вы не можете добавить в корзину товар,";
                message.secondmessage.Text = "которого нет в наличии ";

                message.Show();
            }

            else
            {
                _currentUser.UserBasket.Add(product);
            }  
        }

        //Перейти в корзину
        private void GoToBasket_Button_Click(object sender, RoutedEventArgs e)
        {
            Basket basket = new Basket(_currentUser);
            basket.Show();
        }

        private void Sort_fromExpensiveToCheap(object sender, RoutedEventArgs e)
        {
            SortAttribute = "CostDesc";
            FilterSearchAndSortProducts();
        }

        private void Sort_fromCheapToExpensive(object sender, RoutedEventArgs e)
        {
            SortAttribute = "CostAsc";
            FilterSearchAndSortProducts();
        }

        private void Sort_Manufacturer(object sender, RoutedEventArgs e)
        {
            SortAttribute = "Manufacturer";
            FilterSearchAndSortProducts();
        }

        private void SearchTextBox_SelectionChanged(object sender, KeyEventArgs e)
        {
            textToSearch = Search.Text.ToLower();
            FilterSearchAndSortProducts();
        }

        private void ChooseManufacturer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedItem != null && comboBox.SelectedItem is ComboBoxItem comboBoxItem)
            {
                selectedManufacturer = comboBoxItem.Content as string ?? "Все производители";
                FilterSearchAndSortProducts();
            }
        }
    }
}

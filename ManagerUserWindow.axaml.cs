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

        //список продуктов, который мы будем потом передавать в другой список
        public static List<Products> ProductsList = new List<Products> { };

        //список товаров, которые в корзине
        //public List<Products> ProductToBasket = new List<Products>() { };

        public ManagerUserWindow()
        {
            InitializeComponent();

            //Установка контекста данных окна на текущий объект
            DataContext = this;
        }

        public ManagerUserWindow(User currentUser, List<Products> productslist)
        {
            ProductsList = productslist;

            DataContext = this;
            _currentUser = currentUser; // Инициализация _currentUser
            InitializeComponent(); // Инициализация компонентов

            StatusBlock.Text = "Статус: " + _currentUser.Status;
            NameBlock.Text = "Имя: " + _currentUser.Name;

            ProductsList.Add(
               new Products()
               {
                   Name = "Samsung Galaxy A72",
                   Manufacturer = "Samsung",
                   Description = "Безграничный экран AMOLED 6.7', 256ГБ, Черный",
                   Quantity = 5,
                   Cost = 33990
               });

            ProductsList.Add(
               new Products()
               {
                   Name = "Poco F5 Pro",
                   Manufacturer = "Xiaomi",
                   Description = "256/8ГБ, Белый, AMOLED FHD+, 6.67', 2G/3G/4G (LTE)/5G",
                   Quantity = 2,
                   Cost = 33990
               });

            ProductsList.Add(
               new Products()
               {
                   Name = "ITEL A70",
                   Manufacturer = "ATEL",
                   Description = "IPS, 6.6' (1612x720), Unisoc T603, 2G/3G/4G (LTE)",
                   Quantity = 34,
                   Cost = 33990
               });

            //задаем видимость кнопок Редактировать и Удалить для каждого товара в зависимости от статуса
            foreach (var product in ProductsList)
            {
                product.IsAdmin = _currentUser.Status == "Admin";
            }

            //-------------------------------------------
            foreach (var product in ProductsList) //добавляем все товары из списка в Listbox
            {
                if (product.Quantity == 0)
                {
                    Color customColor = Color.FromRgb(102, 98, 103);
                    SolidColorBrush brush = new SolidColorBrush(customColor);
                    product.backgrColor = brush;
                }

                listboxProducts.Items.Add(product);
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

            //если удаляемый товар есть в корзине, То выводим сообщение 
            if (_currentUser.UserBasket.Contains(product))
            {
                DeleteProductMessageBox message = new DeleteProductMessageBox();

                message.firstmessage.Text = "Вы не можете удалить товар, ";
                message.secondmessage.Text = "который есть в корзине";

                message.Show();
            }
            else
            {
                //если нет в корзине, То удаляем из списка товаров
                ProductsList.Remove(product);
                listboxProducts.Items.Remove(product);
            }
        }

        //отредактировать товар
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = (Button)sender;

            Products editProduct = (Products)editButton.DataContext;

            //вносим в editproduct параметр того товара, в котором мы нажали на редактирование
            //а также список товаров отсюда, чтобы список товаров не очистился при переходе на другое окно
            EditProduct prod = new EditProduct(editProduct, this, ProductsList);
            prod.Show();
        }

        //это запускаем в окне AddProduct, чтобы прямо в этот список добавить товар
        public void UpdateProductsList(Products newProduct)
        {
            ProductsList.Add(newProduct);
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
            listboxProducts.Items.Add(newProduct);
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
            MainWindow auth = new MainWindow(ProductsList);
            auth.Show();

            this.Close();
        }

        private void Sort_fromExpensiveToCheap(object sender, RoutedEventArgs e)
        {
            List<Products> SortedList = ProductsList.OrderByDescending(item => item.Cost).ToList();
            UpdateListBox(SortedList);
        }

        private void Sort_fromCheapToExpensive(object sender, RoutedEventArgs e)
        {
            List<Products> SortedList = ProductsList.OrderBy(item => item.Cost).ToList();
            UpdateListBox(SortedList);
        }

        private void Sort_Manufacturer(object sender, RoutedEventArgs e)
        {
            List<Products> SortedList = ProductsList.OrderBy(item => item.Manufacturer).ToList();
            UpdateListBox(SortedList);
        }

        private void UpdateListBox(List<Products> ListToAdd) //Обновляем список товаров
        {
            listboxProducts.Items.Clear();

            foreach (var item in ListToAdd)
            {
                listboxProducts.Items.Add(item);
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

                //Basket basket = new Basket(_currentUser);
            }  
        }

        //Перейти в корзину
        private void GoToBasket_Button_Click(object sender, RoutedEventArgs e)
        {
            Basket basket = new Basket(_currentUser);
            basket.Show();
        }
    }
}

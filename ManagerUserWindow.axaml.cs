using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AvaloniaMyProject
{
    public partial class ManagerUserWindow : Window
    {
        public ObservableCollection<Products> ProductsList { get; set; } = new ObservableCollection<Products>();
        private User _currentUser;
        Products p = new Products();

        public ManagerUserWindow()
        {
            InitializeComponent();

            // Установка контекста данных окна на текущий объект
            DataContext = this;

        }

        public ManagerUserWindow(User currentUser)
        {
            DataContext = this;
            _currentUser = currentUser; // Инициализация _currentUser
            InitializeComponent(); // Инициализация компонентов

            // Добавляем элемент в список
            ProductsList.Add(
                new Products()
                {
                    Name = "Galaxy A72",
                    Manufacturer = "Samsung",
                    Description = "256GB, Black, Защита от воды (IP67), 2 дня работы⁶ и поддержка супербыстрой зарядки 25 Вт," +
                    "Безграничный⁴ 6.7''³ sAMOLED экран 90 Гц",
                    Quantity = 356,
                    Cost = 33210
                }
           );

            ProductsList.Add(
               new Products()
               {
                   Name = "Google Pixel 7",
                   Manufacturer = "Google",
                   Description = "диагональ экрана: 6.30\", количество основных камер: 2, память: 128 ГБ, 256 ГБ, оперативная память: 8 ГБ, " +
                   "емкость аккумулятора: 4355 мА⋅ч, разрешение экрана: 2400x1080, частота обновления экрана: 90 Гц",
                   Quantity = 321,
                   Cost = 37990
               }
          );

            Button addProductButton = this.FindControl<Button>("AddProduct");
            if (_currentUser.Status != "Admin")
            {
                addProductButton.IsVisible = false;
            }
            else
            {
                addProductButton.IsVisible = true;
            }

            this.Opened += ManagerUserWindow_WindowOpened;
        }

        private void ManagerUserWindow_WindowOpened(object? sender, EventArgs e)
        {
            // Установка значения свойства IsAdmin для каждого элемента в ProductsList
            foreach (var product in ProductsList)
            {
                product.IsAdmin = _currentUser.Status == "Admin";
            }
        }

        //удалить товар
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, на которую было нажатие
            Button deleteButton = (Button)sender;

            // Получаем контекст данных этой кнопки (т.е. элемент Products, к которому привязана кнопка)
            Products product = (Products)deleteButton.DataContext;

            // Удаляем элемент из коллекции
            ProductsList.Remove(product);
        }

        //отредактировать товар
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

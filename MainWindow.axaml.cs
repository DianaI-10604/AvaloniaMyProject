using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaMyProject
{
    public partial class MainWindow : Window
    {
        //private Products _product;
        //public static List<Products> AllProducts = new List<Products>(); //здесь будем держать все значения товаров, чтобы они не очищались

        public static List<User> Users = new List<User> {
            new User
            {
                Name = "Ищенко Диана",
                Login = "Diana_2004",
                Password = "1234",
                Status = "Admin"
            },

            new User()
            {
                Name = "vjskdfs",
                Login = "1234",
                Password = "1234",
                Status = "User"
            }
        };

        //public MainWindow()
        //{
        //    InitializeComponent();
        //}

        //сюда надо передать список товаров
        public MainWindow(/*List<Products> products*/)
        {
            InitializeComponent();

            //AllProducts = products;

            // Устанавливаем текст прямо из кода
            ErrorMessage.Text = "";
        }

        private void SignIn_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EmailTextBox.Text)) //Если строка непустая, то проверяем наш список на наличие этих полей
            {
                string login = EmailTextBox.Text;
                string password = PasswordTextBox.Text;

                //ищем первое совпадние в списке users
                User usercheck = Users.FirstOrDefault(u => u.Login == login && u.Password == password);

                if (usercheck != null)
                {
                    // Вход выполнен успешно
                    ManagerUserWindow muw = new ManagerUserWindow(usercheck /*AllProducts*/);
                    muw.Show();
                    this.Close();
                }
                else
                {
                    // Ошибка входа
                    ErrorMessage.Text = "Неверно введены данные";
                    PasswordTextBox.Clear();
                }
            }
        }

        private void GuestMode_ButtonClick(object sender, RoutedEventArgs e)
        {
            User guestUser = new User();
            guestUser.Status = "гость";
            ManagerUserWindow guest = new ManagerUserWindow(guestUser /*AllProducts*/);

            guest.Show();
            this.Close();
        }
    }
}
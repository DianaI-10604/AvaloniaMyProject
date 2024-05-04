using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Linq;

namespace AvaloniaMyProject
{
    public partial class MainWindow : Window
    {
        User user = new User();
        Products p = new Products();

        public MainWindow()
        {
            InitializeComponent();

            // Устанавливаем текст прямо из кода
            ErrorMessage.Text = "";

            //Это администратор
            user.users.Add(
                    new User()
                    {
                        Name = "Ищенко Диана",
                        Login = "Diana_2004",
                        Password = "1234",
                        Status = "Admin"
                    }
                );

            //авторизованный пользователь (тест видимости кнопок)
            user.users.Add(
                  new User()
                  {
                      Name = "Тютюнникова Анна",
                      Login = "equestrian",
                      Password = "1234",
                      Status = "User"
                  }
              );
        }

        private void SignIn_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EmailTextBox.Text)) //Если строка непустая, то проверяем наш список на наличие этих полей
            {
                string login = EmailTextBox.Text;
                string password = PasswordTextBox.Text;

                User usercheck = user.users.FirstOrDefault(u => u.Login == login && u.Password == password);

                if (usercheck != null)
                {
                    // Вход выполнен успешно
                    ManagerUserWindow muw = new ManagerUserWindow(usercheck);
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
    }
}
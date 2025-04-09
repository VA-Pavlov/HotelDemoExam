using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Hotel.Data;
using Hotel.Model;

namespace Hotel.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string userLogin = "";
        private int countLogin = 0;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrWhiteSpace(PasswordPasswordBox.Password))
            {
                MessageBox.Show("Вы не ввели логин или пароль", "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedUser = UsersData.GetUsers().FirstOrDefault(user => user.Login == LoginTextBox.Text);

            // Проверка существования пользователя
            if (selectedUser == null)
            {
                MessageBox.Show("Вы ввели неверный логин", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка пароля
            if (selectedUser.Password != PasswordPasswordBox.Password)
            {
                if (userLogin != selectedUser.Login)
                {
                    userLogin = selectedUser.Login;
                    countLogin = 0;
                }
                countLogin++;
                MessageBox.Show($"Вы ввели неверный пароль.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка статуса пользователя
            if (selectedUser.Status == UserStatus.Blocked)
            {
                MessageBox.Show("Вы заблокированы. Обратитесь к администратору.", "Информация о блокировке", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Проверка даты последнего входа
            if ((selectedUser.LastDateLogin == null || selectedUser.LastDateLogin >= DateTime.Now.AddMonths(-1)) && countLogin < 3)
            {
                MessageBox.Show("Вы успешно вошли", "Вход", MessageBoxButton.OK, MessageBoxImage.Information);

                // Обработка ролей
                switch (selectedUser.Role)
                {
                    case UserRole.Admin:
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                        this.Close();
                        break;

                    case UserRole.Client:
                        if (selectedUser.LastDateLogin == null)
                        {
                            ChangeNewUserPasswordWindow changeNewUserPasswordWindow = new ChangeNewUserPasswordWindow(selectedUser);
                            changeNewUserPasswordWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            selectedUser.LastDateLogin = DateTime.Now;
                            ClientWindow clientWindow = new ClientWindow(selectedUser);
                            clientWindow.Show();
                            this.Close();
                        }
                        break;
                }
            }
            else
            {
                selectedUser.Status = UserStatus.Blocked;
                countLogin = 0;
                MessageBox.Show("Вы заблокированы. Обратитесь к администратору.", "Информация о блокировке", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

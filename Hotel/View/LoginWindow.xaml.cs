using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
            if (LoginTextBox.Text.Trim() != "" && PasswordPasswordBox.Password != "")
            {
                var selectedUser = UsersData.GetUsers().FirstOrDefault(user => user.Login == LoginTextBox.Text);
                if (selectedUser != null)
                {
                    if (selectedUser.Password == PasswordPasswordBox.Password)
                    {
                        if (selectedUser.Status != UserStatus.Blocked)
                        {
                            if ((selectedUser.LastDateLogin == null || selectedUser.LastDateLogin >= DateTime.Now.AddMonths(-1)) && countLogin < 3)
                            {
                                switch (selectedUser.Role)
                                {
                                    case UserRole.Admin:
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
                                            ClientWindow clientWindow = new ClientWindow();
                                            clientWindow.Show();
                                            this.Close();
                                        }
                                        break;

                                }
                            }
                            else
                            {
                                selectedUser.Status = UserStatus.Blocked;
                                MessageBox.Show("Вы заблокированы. Обратитесь к администратору.", "Информация о блокировке", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                            MessageBox.Show("Вы заблокированы. Обратитесь к администратору.", "Информация о блокировке", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (userLogin != selectedUser.Login)
                        {
                            userLogin = selectedUser.Login;
                            countLogin = 0;
                        }
                        countLogin++;
                        MessageBox.Show($"Вы ввели неверный пароль. До блокировки {3 - countLogin} попыток", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                    MessageBox.Show("Вы ввели неверный логин", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Вы не ввели логин или пароль", "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

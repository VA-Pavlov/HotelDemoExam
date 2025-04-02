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
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text.Trim() != "" && PasswordPasswordBox.Password != "")
            {
                var selectedUser = UsersData.GetUsers().FirstOrDefault(user =>
                                    user.Login == LoginTextBox.Text && user.Password == PasswordPasswordBox.Password);
                if (selectedUser != null)
                {
                    if (selectedUser.LastDateLogin >= DateTime.Now.AddMonths(-1))
                    {

                    }
                }
                else
                    MessageBox.Show("Вы ввели неверный логин или пароль", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);


            }
            else
                MessageBox.Show("Вы не ввели логин или пароль", "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

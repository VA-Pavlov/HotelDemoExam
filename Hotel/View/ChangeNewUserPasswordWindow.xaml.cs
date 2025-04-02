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
using Hotel.Model;

namespace Hotel.View
{
    /// <summary>
    /// Логика взаимодействия для ChangeNewUserPasswordWindow.xaml
    /// </summary>
    public partial class ChangeNewUserPasswordWindow : Window
    {
        private User user;
        public ChangeNewUserPasswordWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            this.user.LastDateLogin = DateTime.Now;
        }

        private void ChangePassword_Button_Click(object sender, RoutedEventArgs e)
        {
            if (OldPasswordTextBox.Text != "" && NewPasswordTextBox.Text != "" && RepeadNewPasswordTextBox.Text != "")
            {
                if (NewPasswordTextBox.Text == RepeadNewPasswordTextBox.Text)
                {
                    if (OldPasswordTextBox.Text == user.Password)
                    {
                        if (OldPasswordTextBox.Text != NewPasswordTextBox.Text)
                        {
                            ClientWindow clientWindow = new ClientWindow();
                            clientWindow.Show();
                            this.Close();
                        }
                        else
                        MessageBox.Show("Текущий пароль совпадает с новым", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("Текущий пароль введен неверно", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Пароли не совпадают", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Вы заполнили не все поля", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

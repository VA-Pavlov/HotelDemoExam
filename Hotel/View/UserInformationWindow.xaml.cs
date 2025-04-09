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
    /// Логика взаимодействия для UserInformationWindow.xaml
    /// </summary>
    public partial class UserInformationWindow : Window
    {
        User User { get; set; }
        public UserInformationWindow()
        {
            InitializeComponent();
            User = new User();
            this.DataContext = User;
            Title = "Регистрация пользователя";
            SaveButton.Content = "Сохранить";

        }
        public UserInformationWindow(User user)
        {
            InitializeComponent();
            this.DataContext = user;
            this.User = user;
            Title = "Редактирование пользователя";
            SaveButton.Content = "Сохранить изменения";

        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (UsersData.AddUser(User))
            {
                MessageBox.Show("Пользователь успешно зарегистрирован", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else 
                MessageBox.Show("Ошибка регистраци пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}

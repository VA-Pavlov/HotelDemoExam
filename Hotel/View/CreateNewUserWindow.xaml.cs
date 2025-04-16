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
    /// Логика взаимодействия для CreateNewUserWindow.xaml
    /// </summary>
    public partial class CreateNewUserWindow : Window
    {
        private User user = new User();
        public CreateNewUserWindow()
        {
            InitializeComponent();
            this.DataContext = user;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            user.Role = RoleComboBox.SelectedItem.ToString() == "Пользователь" ? UserRole.Client : UserRole.Admin;
            UsersData.AddUser(user);
            this.DialogResult = true;
            this.Close();
        }
    }
}

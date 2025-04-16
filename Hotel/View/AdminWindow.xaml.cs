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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            UsersListBox.ItemsSource = UsersData.GetUsers();
        }

        private void AddNewUserButton(object sender, RoutedEventArgs e)
        {
            CreateNewUserWindow createNewUserWindow = new CreateNewUserWindow();
            if (createNewUserWindow.ShowDialog() == true)
            {
                UsersData.GetUsers();
            }
        }


        private void EditUserButton(object sender, RoutedEventArgs e)
        {
            User user = UsersListBox.SelectedItem as User;
            UserInformationWindow userInformationWindow = new UserInformationWindow(user);
            userInformationWindow.ShowDialog();
        }

        private void UnblkedUserButton_Click(object sender, RoutedEventArgs e)
        {
            UsersData.UnblockedUser(UsersListBox.SelectedItem as User);
            UsersData.GetUsers();
        }
    }
}

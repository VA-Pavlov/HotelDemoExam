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
    /// Логика взаимодействия для UserInformationWindow.xaml
    /// </summary>
    public partial class UserInformationWindow : Window
    {
        public UserInformationWindow()
        {
            InitializeComponent();
            Title = "Создание пользователя";
            SaveButton.Content = "Сохранить";

        }
        public UserInformationWindow(User user)
        {
            InitializeComponent();
            this.DataContext = user;
            Title = "Редактирование пользователя";
            SaveButton.Content = "Сохранить изменения";

        }

    }
}

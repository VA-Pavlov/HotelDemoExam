using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hotel.Model;

namespace Hotel.Data
{
    static public class UsersData
    {
        private static SqlConnection connection = new SqlConnection(
            "Data Source=WIN-PKUO4L9RGT8;" +
            "Initial Catalog=Hotel;" +
            "Integrated Security=True");
        private static ObservableCollection<User> UserList = new ObservableCollection<User>();
        public static ObservableCollection<User> GetUsers()
        {
            try
            {
                connection.Open();
                var query = "SELECT Id, FirstName, Surname, Login,Password,Status_id, LastDateLogin,Role_id FROM Users;";
                var command = new SqlCommand(query, connection);
                var table = command.ExecuteReader();
                UserList.Clear();
                while (table.Read())
                {
                    var user = new User()
                    {
                        Id = Convert.ToInt32(table[0]),
                        FirstName = Convert.ToString(table[1]),
                        Surname = Convert.ToString(table[2]),
                        Login = Convert.ToString(table[3]),
                        Password = Convert.ToString(table[4]),
                        Status = Convert.ToInt32(table[5]) == 1 ? UserStatus.Active : UserStatus.Blocked,
                        // LastDateLogin = table[6] is null ? null : Convert.ToDateTime(table[6]),
                        Role = Convert.ToInt32(table[7]) == 1 ? UserRole.Admin : UserRole.Client
                    };
                    try
                    {
                        user.LastDateLogin = Convert.ToDateTime(table[6]);
                    }
                    catch
                    {
                        user.LastDateLogin = null;
                    }
                    UserList.Add(user);
                }
                table.Close();
                connection.Close();
                return UserList;
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message, "Ошибка соединения с базой данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return new ObservableCollection<User>();
            }
        }
        public static bool ChangeUserPassword(User user, string newPassword)
        {
            try
            {
                connection.Open();
                var query = $"UPDATE Users SET Password = '{newPassword}',LastDateLogin='{DateTime.Now}' WHERE Id = {user.Id};";
                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                user.Password = newPassword;
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message, "Ошибка соединения с базой данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public static bool AddUser(User user)
        {
            try
            {
                connection.Open();
                int roleId = user.Role == UserRole.Admin ? 1 : 2;
                var query = $"INSERT INTO Users (FirstName,Surname,Login,Password,Role_id,LastDateLogin,Status_id) " +
                                                $"Values ('{user.FirstName}'," +
                                                        $"'{user.Surname}'," +
                                                        $"'{user.Login}'," +
                                                        $"'{user.Password}'," +
                                                        $"{roleId}," +
                                                        $"NULL," +
                                                        $"1);";
                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                connection.Close();
                return false;
            }
        }

        public static bool UnblockedUser(User user)
        {
            if (user.Status == UserStatus.Blocked)
                try
                {
                    connection.Open();
                    var query = $"UPDATE Users SET Status_id = 1, LastDateLogin = '{DateTime.Now}' WHERE Id = {user.Id};";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show($"Пользователь {user.FirstName} {user.Surname} разблокирован!", "Пользователь разблокирован", MessageBoxButton.OK, MessageBoxImage.Information);
                    connection.Close();

                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    connection.Close();
                    return false;
                }
            MessageBox.Show($"Пользователь {user.FirstName} {user.Surname} не заблокирован", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        public static bool EditUser(User user)
        {
            try
            {
                connection.Open();
                int roleId = user.Role == UserRole.Admin ? 1 : 2;
                var quer = $"UPDATE Users SET " +
                                     $"FirstName='{user.FirstName}', " +
                                     $"Surname='{user.Surname}'," +
                                     $"Login='{user.Login}'," +
                                     $"Password='{user.Password}', " +
                                     $"Role_id = {roleId} " +
                                     $"WHERE Id = {user.Id};";
                var command = new SqlCommand(quer, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show($"Пользователь {user.FirstName} {user.Surname} умпешно изменен", "Успешное окончание редактирования", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                connection.Close();
                return false;
            }
        }
            //private static List<User> UserList = new List<User>();
            //public static List<User> GetUsers()
            //{
            //    if (UserList.Count == 0)
            //    {
            //        UserList.Add(new User()
            //        {
            //            Id = 0,
            //            FirstName = "Pasha",
            //            Surname = "Popov",
            //            Login = "Admin",
            //            Password = "12345",
            //            Status = UserStatus.Active,
            //            LastDateLogin = DateTime.Now,
            //            Role = UserRole.Admin
            //        });
            //        UserList.Add(new User()
            //        {
            //            Id = 1,
            //            FirstName = "User1",
            //            Surname = "User1",
            //            Login = "1",
            //            Password = "1",
            //            Status = UserStatus.Active,
            //            LastDateLogin = new DateTime(2025, 3, 5),
            //            Role = UserRole.Client
            //        });
            //        UserList.Add(new User()
            //        {
            //            Id = 2,
            //            FirstName = "User2",
            //            Surname = "User2",
            //            Login = "2",
            //            Password = "2",
            //            Status = UserStatus.Active,
            //            Role = UserRole.Client
            //        });
            //    }
            //    return UserList;
            //}
        }
    }

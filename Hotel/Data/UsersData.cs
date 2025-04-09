using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Model;

namespace Hotel.Data
{
    static public class UsersData
    {
        private static SqlConnection connection = new SqlConnection(
            "Data Source=WIN-PKUO4L9RGT8;" +
            "Initial Catalog=Hotel;" +
            "Integrated Security=True");

        public static bool AddUser(User user)
        {
            try
            {
                connection.Open();
                int roleId = user.Role == UserRole.Admin ? 1 : 2;
                int statusId = user.Status == UserStatus.Active ? 1 : 2;
                var query = $"INSERT INTO Users Values ('{user.FirstName}'," +
                                                        $"'{user.Surname}'," +
                                                        $"'{user.Login}'," +
                                                        $"'{user.Password}'," +
                                                        $"{roleId}," +
                                                        $"NULL," +
                                                        $"{statusId});"; 
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


        private static List<User> UserList = new List<User>();
        public static List<User> GetUsers()
        {
            if (UserList.Count == 0)
            {
                UserList.Add(new User()
                {
                    Id = 0,
                    FirstName = "Pasha",
                    Surname = "Popov",
                    Login = "Admin",
                    Password = "12345",
                    Status = UserStatus.Active,
                    LastDateLogin = DateTime.Now,
                    Role = UserRole.Admin
                });
                UserList.Add(new User()
                {
                    Id = 1,
                    FirstName = "User1",
                    Surname = "User1",
                    Login = "1",
                    Password = "1",
                    Status = UserStatus.Active,
                    LastDateLogin = new DateTime(2025,3,5),
                    Role = UserRole.Client
                });
                UserList.Add(new User()
                {
                    Id = 2,
                    FirstName = "User2",
                    Surname = "User2",
                    Login = "2",
                    Password = "2",
                    Status = UserStatus.Active,
                    Role = UserRole.Client
                });
            }
            return UserList;
        }
    }
}

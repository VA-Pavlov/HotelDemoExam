using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Model;

namespace Hotel.Data
{
    static public class UsersData
    {
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

using AppToken.Domain.Common;
using AppToken.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppToken.Infra.Service
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>
    {
        new User { Id = 1, Username = "seenu", Password = "seenu",Roles="admin" },
        new User { Id = 2, Username = "vasan", Password = "vasan",Roles="user" }
    };
        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            return user;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Controllers
{
    public class UserController
    {
        public User createUser(string name, string port)
        {
            User user = new User()
            {
                Name = name,
                Port = port,
            };
            return user;
        }
        public bool validatePort(string port)
        {
            int result;
            if (string.IsNullOrEmpty(port) || port.Length > 5 || !int.TryParse(port, out result))
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        public bool validateUsername(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length > 8)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}

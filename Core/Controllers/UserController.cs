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
            if (string.IsNullOrEmpty(port))
            {
                return false;
            }
            else
            {
                int result;
                if (int.TryParse(port, out result))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool validateUsername(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            else
            {
                if (name.Length > 8)
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
}

using System;
using System.Linq;
using Core.Controllers;
using Core.Models;
using Fleck;

namespace ConsoleChat
{
    class MainProgram
    {
        public static UserController _userController = new UserController();
        public static MainProgram _main = new MainProgram();


        static void Main(string[] args)
        {
            User conectedUser = new User();
            conectedUser = _main.dataEntry();
            _main.conectToPort(conectedUser.Port);
        }
        public User dataEntry()
        {
            string name = string.Empty;
            string port = string.Empty;

            //Ask for port
            bool validPort = false;
            while (!validPort)
            {
                Console.WriteLine("Enter the Port");
                port = Convert.ToString(Console.ReadLine());
                validPort = _userController.validatePort(port);
                Console.Clear();
                if (!validPort)
                    Console.WriteLine("invalid Port");
            }
            //Ask for Username
            bool validUsername = false;
            while (!validUsername)
            {
                Console.WriteLine("Enter username (min 1 char - max 8 char)");
                name = Convert.ToString(Console.ReadLine());
                validUsername = _userController.validateUsername(name);
                Console.Clear();
                if (!validUsername)
                    Console.WriteLine("invalid username");
            }

            User conectedUser = _userController.createUser(name, port);
            Console.WriteLine("Conected User: " + conectedUser.Name + " port: " + conectedUser.Port);
            return conectedUser;
        }

        public void conectToPort( string port)
        {
            var server = new WebSocketServer("ws://0.0.0.0:" + port);
            server.Start(socket =>
            {
                socket.OnOpen = () => Console.WriteLine("Open");
                socket.OnClose = () => Console.WriteLine("Close");
                socket.OnMessage = message => socket.Send(message);
                
            });
        }
    }
    
}

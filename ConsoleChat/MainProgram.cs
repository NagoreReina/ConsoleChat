﻿using System;
using System.Linq;
using Core.Controllers;
using Core.Models;

namespace ConsoleChat
{
    class MainProgram
    {
        public static UserController _userController = new UserController();
        public static MainProgram _main = new MainProgram();
        static void Main(string[] args)
        {
            _main.dataEntry();
        }
        public void dataEntry()
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
        }
    }
    
}

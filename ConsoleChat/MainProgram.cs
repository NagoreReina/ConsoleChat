using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebSocketSharp;
using Core.Controllers;
using Core.Models;
using Fleck;
using log4net;

namespace ConsoleChat
{
    class MainProgram
    {
        public static UserController _userController = new UserController();
        public static MainProgram _main = new MainProgram();
        public static List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();
        private static WebSocketServer server;

        static void Main(string[] args)
        {

            User conectedUser = _main.dataEntry();
            try
            {
                server = _main.StartServer(conectedUser.Port);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            using (var ws = new WebSocket("ws://127.0.0.1:" + conectedUser.Port))
            {
                ws.OnOpen += (sender, e) => ws.Send("Connected user: " + conectedUser.Name);
                ws.OnMessage += (sender, e) =>
                    { Console.WriteLine(e.Data); };
                ws.OnClose += (sender, e) => ws.Send("Disconnected user: " + conectedUser.Name);
                ws.Connect();
                while (true)
                {
                    var msg = Console.ReadLine();
                    if (msg == "exit")
                    {
                        ws.Send("Disconnected user: " + conectedUser.Name);
                        break;
                    }                                       
                    ws.Send(conectedUser.Name + ": " + msg);
                }
            }
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

            return conectedUser;
        }

        public WebSocketServer StartServer(string port)
        {
            Console.WriteLine("processing...");
            var server = new WebSocketServer("ws://127.0.0.1:" + port);

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("New user!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("user disconnected");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    //Console.WriteLine("debug: " + message);
                    allSockets.ToList().ForEach(s => s.Send(message));
                };
            });
            return server;
        }

    }

}

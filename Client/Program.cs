using Client.Controllers;
using Client.Interfeices;
using SFML.Graphics;
using SFML.Window;
using System;

namespace Client{
    class Program{
        static void Main(string[] args){
            Router router = Router.Init();
            router.graphicsControllers.SetController(new MenuController());

            Console.WriteLine("Oh nooooo!");
            Console.Read();
        }
    }
}


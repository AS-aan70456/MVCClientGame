using Client.Controllers;
using Client.Interfeices;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.Window;
using System;
using System.IO;

namespace Client{
    class Program{
        static void Main(string[] args) {
            Router router = Router.Init();
            Config.LoadConfig();

            //Console.WriteLine(Config.config.ToString());

            router.graphicsControllers.SetController(new MenuController());

            Console.WriteLine(Config.config.ToString());


            Console.WriteLine();
            Console.Read();
        }
    }
}


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
            router.graphicsControllers.SetController(new MenuController());

            //Config.Save(new Config() {fov=70, numRey = 512, isDisplayFPS=true, isTransparantTextures = false });
            

            Console.WriteLine(Config.config.numRey);
            Console.Read();
        }
    }
}


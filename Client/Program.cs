using Client.Interfeices;
using SFML.Graphics;
using SFML.Window;
using System;

namespace Client{
    class Program{
        static void Main(string[] args){
            
            RenderWindow window = new RenderWindow(new VideoMode(1080, 720), "MVC_SFML");

            Router router = Router.Init();
            router.graphicsController.Activation(router.gameControl, window);

            Console.Read();
        }
    }
}

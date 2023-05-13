using System;
using Client.Controllers;
using Client.Interfeices;
using Client.Models;
using SFML.Graphics;
using SFML.Window;

namespace Client{
    class Router{
        private RenderWindow window;

        public bool IsOpen { get { return window.IsOpen; } }

        public GraphicsController graphicsControllers { get; }
        public PlayersController playersControl { get; }

        private static Router router = null;

        private Router() {
            window = new RenderWindow(new VideoMode(1080, 720), "MVC_SFML");

            graphicsControllers = new GraphicsController(window);
            playersControl = new PlayersController();
        }

        public static Router Init() {
            if (router == null)
                router = new Router();
            return router;
        }

    }
}

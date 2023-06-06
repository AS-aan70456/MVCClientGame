using System;
using Client.Controllers;
using Client.Interfeices;
using Client.Models;
using SFML.Graphics;
using SFML.Window;

namespace Client{
    class Router{
        public GraphicsController graphicsControllers { get; private set; }
        public PlayersController playersControl { get; }

        private static Router router = null;

        private Router() {

            graphicsControllers = new GraphicsController();
            playersControl = new PlayersController();
        }

        public static Router Init() {
            if (router == null)
                router = new Router();
            return router;
        }
    }
}

using System;
using Client.Controllers;

namespace Client{
    class Router{

        public MenuController menuControl { get; }
        public GameController gameControl { get; }
        public GraphicsController graphicsController { get; }
        public PlayersController playersControl { get; }

        private static Router router = null;

        private Router() {
            graphicsController = new GraphicsController();
            menuControl = new MenuController();
            gameControl = new GameController();
            playersControl = new PlayersController();
        }

        public static Router Init() {
            if (router == null)
                router = new Router();
            return router;
        }

    }
}

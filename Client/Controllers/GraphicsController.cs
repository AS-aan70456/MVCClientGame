using Client.Interfeices;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Controllers{
    class GraphicsController{
        private RenderWindow window;
        private IDrawController gameController;


        public void Activation(IDrawController gameController, RenderWindow window){
            this.window = window;
            this.gameController = gameController;
            gameController.Activation(window);

            Updata();
        }

        private void Updata() {
            while (true) {
                window.Clear();

                gameController.Draw();
                gameController.Updata();
                window.Display();
                window.DispatchEvents();
            }
        }

    }
}

using Client.Interfeices;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers{
    class GraphicsController{
        private RenderWindow window;
        private IDrawController gameController;


        public  GraphicsController(RenderWindow window){
            this.window = window;
            this.window.Closed += new EventHandler(Close);

        }

        public void SetController(IDrawController gameController){
            this.gameController = gameController;
            gameController.Activation(window);
            Updata();
        }

        private void Updata() {
            Router router = Router.Init();

            while (router.IsOpen) {
                window.Clear();

                gameController.Draw();
                gameController.Updata();

                window.Display();
                window.DispatchEvents();
            }
        }

        private void Close(object sender, EventArgs @event) {
            window.Close();
        }

    }
}

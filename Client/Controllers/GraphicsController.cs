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
            while (true) {
                window.Clear();

                gameController.Draw();
                window.Display();
                window.DispatchEvents();
                gameController.Updata();
            }
        }

        private void Close(object sender, EventArgs @event) {
            window.Close();
        }

    }
}

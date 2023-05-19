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

    //Costile between sfml and my architecture
    class GraphicsController{
        private RenderWindow window;
        public IDrawController gameController { get; private set; }
        private Clock Clock;

        public float time { get; private set; }
        public float fps { get; private set; }

        private Clock clockFps;
        private Time previousTime;
        private Time curentTime;

        public  GraphicsController(RenderWindow window){
            this.window = window;
            this.window.Closed += new EventHandler(Close);

            Clock = new Clock();
            clockFps = new Clock();

            previousTime = clockFps.ElapsedTime;
        }

        public void UpdataWindow(RenderWindow window) {
            this.window = window;
            this.window.Closed += new EventHandler(Close);
        }

        public void SetController(IDrawController gameController) {
            if (this.gameController != null)
                this.gameController.DizActivation();

            this.gameController = gameController;
            this.gameController.Activation(window);
            Updata();
        }

        //Updata scene
        private void Updata() {
            Router router = Router.Init();

            while (router.IsOpen) {
                window.Clear();

                time = Clock.ElapsedTime.AsMicroseconds();
                time = time / 29000;
                Clock.Restart();

                curentTime = clockFps.ElapsedTime;
                fps = 1.0f / (curentTime.AsSeconds() - previousTime.AsSeconds());
                previousTime = curentTime;

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

using Client.Interfeices;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{

    // Graphics controller responsible for managing the rendering and window functionality.
    class GraphicsController
    {
        private RenderWindow window;

        // The current game controller handling the scene.
        public IDrawController gameController { get; private set; }
        private Clock Clock;

        // Properties for tracking window state.
        public bool IsOpen { get { return window.IsOpen; } }
        public float time { get; private set; }
        public float fps { get; private set; }

        private Clock clockFps;
        private Time previousTime;
        private Time curentTime;

        // Constructor initializes the graphics controller.
        public GraphicsController()
        {
            // Initialize the window based on configuration settings.
            if (Config.config.isFullScrean)
                InitFullScrean();
            else
                InitScrean();

            // Set up the window event handler for closing.
            this.window.Closed += new EventHandler(Close);

            // Initialize clocks for timing and FPS calculation.
            Clock = new Clock();
            clockFps = new Clock();
            previousTime = clockFps.ElapsedTime;
        }

        // Set up the window event handler for updating window events.
        public void UpdataWindow()
        {
            this.window.Closed += new EventHandler(Close);
        }

        // Set the current game controller and activate it.
        public void SetController(IDrawController gameController)
        {
            // Deactivate the previous controller, if exists.
            if (this.gameController != null)
                this.gameController.DizActivation();

            // Set the new game controller and activate it.
            this.gameController = gameController;
            this.gameController.Activation(window);
            Updata();
        }

        // Update the scene in a loop.
        private void Updata()
        {
            while (window.IsOpen)
            {
                window.Clear();

                // Calculate time elapsed since last frame.
                time = Clock.ElapsedTime.AsMicroseconds();
                time = time / 29000;
                Clock.Restart();

                // Calculate frames per second.
                curentTime = clockFps.ElapsedTime;
                fps = 1.0f / (curentTime.AsSeconds() - previousTime.AsSeconds());
                previousTime = curentTime;

                // Draw and update the game controller.
                gameController.Draw();
                gameController.Updata();

                window.Display();
                window.DispatchEvents();
            }
        }

        // Initialize the window in full-screen mode.
        public void InitFullScrean()
        {
            window?.Close();
            window = new RenderWindow(new VideoMode(480, 360), "MVC_SFML", Styles.Fullscreen);
            UpdataWindow();
            gameController?.Activation(window);
        }

        // Initialize the window in windowed mode.
        public void InitScrean()
        {
            window?.Close();
            window = new RenderWindow(new VideoMode(1080, 720), "MVC_SFML", Styles.Close);
            UpdataWindow();
            gameController?.Activation(window);
        }

        // Event handler for closing the window.
        private void Close(object sender, EventArgs @event)
        {
            window.Close();
        }
    }
}

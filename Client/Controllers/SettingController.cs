using Client.Interfeices;
using Client.Views;
using SFML.Graphics;
using System;

namespace Client.Controllers
{
    // Controller for handling settings related to the game.
    class SettingController : IDrawController
    {

        SettingView View;

        // Activation method to initialize the controller.
        public void Activation(RenderWindow window)
        {
            View = new SettingView(window);
        }

        // Deactivation method to clean up resources.
        public void DizActivation()
        {
            View.DizActivation();
        }

        // Draw method to render the settings view.
        public void Draw()
        {
            View.Render();
        }

        // Update method for handling settings updates.
        public void Updata()
        {
            // Typically used to handle user input and settings changes.
        }
    }
}

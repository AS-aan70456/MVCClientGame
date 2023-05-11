using System;
using Client.Views;
using Client.Interfeices;
using SFML.Graphics;

namespace Client.Controllers{
    class MenuController : IDrawController{

        MenuView View;

        public void Activation(RenderWindow window){
            View = new MenuView(window);

        }

        public void Draw(){
            View.Render();
        }

        public void Updata() {
            
        }
    }
}

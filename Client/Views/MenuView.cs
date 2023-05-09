using Client.Interfeices;
using SFML.Graphics;
using System;
using System.Linq;


namespace Client.Views{
    class MenuView : IRender{
        public RenderWindow window { get; set; }

        public MenuView(RenderWindow window) {
            this.window = window;
        }

        public void Render(){
            
        }
    }
}

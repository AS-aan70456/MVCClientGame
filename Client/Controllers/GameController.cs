using Client.Interfeices;
using Client.Views;
using Client.Models;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Window;
using Client.Services;

namespace Client.Controllers{
    class GameController : IDrawController{

        bool[] Button;

        private GameView View;

        private ReyCastService reyCast;

        private List<Enemy> enemy;
        private Player player;
        private Level level ;

        private Vector2i MousePosition;

        public void Activation(RenderWindow window){

            level = new Level();
            enemy = new List<Enemy>();
            player = new Player(level);

            reyCast = new ReyCastService(level);

            View = new GameView(window, reyCast, player, enemy);

            Button = new bool[6];
            window.MouseMoved += new EventHandler<MouseMoveEventArgs>(MouseMoved);
            window.KeyPressed += new EventHandler<KeyEventArgs>(KeyPressed);
            window.KeyReleased += new EventHandler<KeyEventArgs>(KeyReleased);
        }

        private void KeyPressed(object sender, KeyEventArgs @event) {
            if (@event.Code == Keyboard.Key.W)
                Button[0] = true;
            else if (@event.Code == Keyboard.Key.S)
                Button[1] = true;
            else if (@event.Code == Keyboard.Key.A)
                Button[2] = true; 
            else if (@event.Code == Keyboard.Key.D)
                Button[3] = true;
            else if (@event.Code == Keyboard.Key.Q)
                Button[4] = true;
            else if (@event.Code == Keyboard.Key.E)
                Button[5] = true;

        }

        private void KeyReleased(object sender, KeyEventArgs @event){
            if (@event.Code == Keyboard.Key.W)
                Button[0] = false;
            else if (@event.Code == Keyboard.Key.S)
                Button[1] = false;
            else if (@event.Code == Keyboard.Key.A)
                Button[2] = false;
            else if (@event.Code == Keyboard.Key.D)
                Button[3] = false;
            else if (@event.Code == Keyboard.Key.Q)
                Button[4] = false;
            else if (@event.Code == Keyboard.Key.E)
                Button[5] = false;
        }

        public void Draw(){
            View.Render();
        }
    
        public void Updata(){

            if (Button[0] == true)
                player.Move(new Vector2f(0.1f, 0));
            if (Button[1] == true)
                player.Move(new Vector2f(-0.1f, 0));
            if (Button[2] == true)
                player.Move(new Vector2f(0, 0.1f));
            if (Button[3] == true)
                player.Move(new Vector2f(0, -0.1f));
            if (Button[4] == true)
                player.Rotate(1.2f);
            if (Button[5] == true)
                player.Rotate(-1.2f);
            
        }

        private void MouseMoved(object sender, MouseMoveEventArgs @event) {
            player.Rotate(( MousePosition.X - @event.X) * 0.6f);

            RenderWindow render = (RenderWindow)sender;
            MousePosition = new Vector2i(@event.X, @event.Y);
            //Mouse.SetPosition(render.Position + (Vector2i)(render.Size / 2));

        }

        public void DizActivation() {
            View.window.MouseMoved -= new EventHandler<MouseMoveEventArgs>(MouseMoved);
            View.window.KeyPressed -= new EventHandler<KeyEventArgs>(KeyPressed);
            View.window.KeyReleased -= new EventHandler<KeyEventArgs>(KeyReleased);
        }
    }
}

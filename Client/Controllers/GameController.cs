using Client.Interfeices;
using Client.Views;
using Client.Models;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using Services;
using SFML.System;
using SFML.Window;

namespace Client.Controllers{
    class GameController : IDrawController{

        bool[] Button;

        private GameView View;

        private ReyCastService reyCast;

        private Maps maps;
        private List<Enemy> enemy;
        private Player player;

        private Vector2i MousePosition;

        public void Activation(RenderWindow window){
            maps = new Maps();
            enemy = new List<Enemy>();
            player = new Player(new Vector2f(11.5f, 3.5f), 1);
            reyCast = new ReyCastService(maps);

            View = new GameView(window, reyCast, maps, player, enemy);

            Button = new bool[6];

            MousePosition = Mouse.GetPosition();
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
            Console.Clear();
            Console.WriteLine("W : " + Button[0]);
            Console.WriteLine("S : " + Button[1]);
            Console.WriteLine("A : " + Button[2]);
            Console.WriteLine("D : " + Button[3]);
            Console.WriteLine("Q : " + Button[4]);
            Console.WriteLine("D : " + Button[5]);

            if (Button[0] == true)
                player.Position -= new Vector2f((float)(Math.Cos(((player.angle * Math.PI) / 180)) * 0.2), (float)(Math.Sin(((player.angle * Math.PI) / 180)) * 0.2));
            if (Button[1] == true)
                player.Position += new Vector2f((float)(Math.Cos(((player.angle * Math.PI) / 180)) * 0.2), (float)(Math.Sin(((player.angle * Math.PI) / 180)) * 0.2));
            if (Button[2] == true)
                player.Position -= new Vector2f((float)(Math.Cos((((player.angle + 90) * Math.PI) / 180)) * 0.2), (float)(Math.Sin((((player.angle + 90) * Math.PI) / 180)) * 0.2));
            if (Button[3] == true)
                player.Position += new Vector2f((float)(Math.Cos((((player.angle + 90) * Math.PI) / 180)) * 0.2), (float)(Math.Sin((((player.angle + 90) * Math.PI) / 180)) * 0.2));
            if (Button[4] == true)
                player.angle += 1.2f;
            if (Button[5] == true)
                player.angle -= 1.2f;
        }

        private void MouseMoved(object sender, MouseMoveEventArgs @event) {
            player.angle += (( MousePosition.X - @event.X) * 0.6f);

            RenderWindow render = (RenderWindow)sender;
            MousePosition = new Vector2i(@event.X, @event.Y);
            //Mouse.SetPosition(render.Position + (Vector2i)(render.Size / 2));

        }

    }
}

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

        private GameView View;

        private ReyCastService reyCast;

        private Maps maps;
        private List<Enemy> enemy;
        private Player player;

        private Vector2i MousePosition;

        public void Activation(RenderWindow window){
            maps = new Maps();
            enemy = new List<Enemy>();
            player = new Player(new Vector2f(15, 7), 45);
            reyCast = new ReyCastService(maps);

            View = new GameView(window, reyCast, maps, player, enemy);

            MousePosition = Mouse.GetPosition();
            window.MouseMoved += new EventHandler<MouseMoveEventArgs>(MouseMoved);
        }


        public void Draw(){
            View.Render();
        }

        public void Updata(){
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                player.Position += new Vector2f((float)(Math.Cos(((player.angle * Math.PI) / 180)) * 0.2), (float)(Math.Sin(((player.angle * Math.PI) / 180)) * 0.2));
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                player.Position -= new Vector2f((float)(Math.Cos(((player.angle * Math.PI) / 180)) * 0.2), (float)(Math.Sin(((player.angle * Math.PI) / 180)) * 0.2));
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                player.Position += new Vector2f((float)(Math.Cos((((player.angle + 90) * Math.PI) / 180)) * 0.2), (float)(Math.Sin((((player.angle + 90) * Math.PI) / 180)) * 0.2));
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                player.Position -= new Vector2f((float)(Math.Cos((((player.angle + 90) * Math.PI) / 180)) * 0.2), (float)(Math.Sin((((player.angle + 90) * Math.PI) / 180)) * 0.2));
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                player.angle += 1.2f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
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

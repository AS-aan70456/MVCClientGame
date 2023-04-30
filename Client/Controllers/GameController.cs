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

        GameView View;

        ReyCastService reyCast;

        Maps maps;
        List<Enemy> enemy;
        Player player;

        public void Activation(RenderWindow window){
            maps = new Maps();
            enemy = new List<Enemy>();
            player = new Player(new Vector2f(15, 7), 45);
            reyCast = new ReyCastService(maps);

            View = new GameView(window, reyCast, maps, player, enemy);
        }

        public void Draw(){
            View.Render();
        }

        public void Updata(){
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                player.Position += new Vector2f((float)(Math.Cos(((player.angle * Math.PI) / 180)) * 0.5), (float)(Math.Sin(((player.angle * Math.PI) / 180)) * 0.5));
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                player.Position -= new Vector2f((float)(Math.Cos(((player.angle * Math.PI) / 180)) * 0.5), (float)(Math.Sin(((player.angle * Math.PI) / 180)) * 0.5));
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                player.Position -= new Vector2f(0.5f, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                player.Position += new Vector2f(0.5f, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                player.angle += 0.9f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
                player.angle -= 0.9f;

        }

    }
}

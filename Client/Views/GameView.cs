using Client.Interfeices;
using Client.Models;
using Client.Services;
using Client.Views.Shared;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Views{
    class GameView : IRender{

        public RenderWindow window { get; private set; }

        private GuiBase Canvas;

        private ReyCastService reyCast;
        private List<Enemy> entities;
        private Player player;

        public GameView(RenderWindow window, ReyCastService reyCast, Player player, List<Enemy> entities){
            this.window = window;
            this.reyCast = reyCast;
            this.player = player;
            this.entities = entities;

            Canvas = new Rectangle();

            if (Config.config.isDisplayFPS){

                Board boardFPS = new Board();
                boardFPS.text = new Text("34", ResurceMeneger.LoadFont(@"Resurces\Font\Samson.ttf"));
                boardFPS.text.FillColor = new Color(34, 139, 34);
                boardFPS.text.CharacterSize = 64;

                boardFPS.SetPosition(new Vector2f(32, 20));

                Canvas.addNode(boardFPS);
            }
        }

        public void Render(){
            Router router = Router.Init();

            Rey[] reyCastModel = reyCast.ReyCastWall(player, Config.config.fov, 16, Config.config.numRey);
            for (int i = 0; i < reyCastModel.Length; i++)
                DrawWall(reyCastModel[i], reyCastModel.Length, i);

            Canvas.UpdataNode();

            foreach (var el in Canvas.GetGraphicsPackages())
                window.Draw(el);
        }

    

        private void DrawWall(Rey reyCast,int lenght ,int i) {
            RectangleShape wall = new RectangleShape();

            if (reyCast.rey != null)
                DrawWall(reyCast.rey, lenght, i);

            wall.Texture = Level.GetTexture(reyCast.Wall);

            wall.TextureRect = new IntRect(
                new Vector2i((int)(reyCast.offset * 16), 0),
                new Vector2i(1, 16)
            );

            wall.Size = new Vector2f(
                ((float)window.Size.X / lenght),
                (window.Size.Y / 2) * (1 + (1 / reyCast.ReyDistance)) - (window.Size.Y / 2) * (1 - (1 / reyCast.ReyDistance))
            );

            wall.Position = new Vector2f(
                ((float)window.Size.X / lenght) * (float)(i),
               (window.Size.Y / 2) * (1 - (1 / reyCast.ReyDistance))
            );

            int brightness = ((int)(255 - (reyCast.ReyDistance * 10)));

            if (brightness < 0) brightness = 0;
            if (brightness > 255) brightness = 255;

            wall.FillColor = new Color((byte)(brightness), (byte)(brightness), (byte)(brightness));

            window.Draw(wall);

        }

    }
}
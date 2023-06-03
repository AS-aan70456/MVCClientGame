using Client.Interfeices;
using Client.Models;
using CoreEngine.Entitys;
using CoreEngine.ReyCast;
using CoreEngine.System;
using GraphicsEngine;
using GraphicsEngine.Bilder;
using GraphicsEngine.Resurces;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Client.Views{
    class GameView : IRender{

        public RenderWindow window { get; private set; }

        private UIObject Canvas;

        private ReyCastService reyCast;
        private List<Entity> entities;
        private Player player;

        Level Level;

        public GameView(RenderWindow window, ReyCastService reyCast, Player player, List<Entity> entities, Level Level){
            this.window = window;
            this.reyCast = reyCast;
            this.player = player;
            this.entities = entities;

            Canvas = new UIObject();
            this.Level = Level;

            if (Config.config.isDisplayFPS){
                
                Text boardFPSText = new TextBilder("34", @"Resurces\Font\Samson.ttf")
                    .FillColor(new Color(34, 139, 34))
                    .CharacterSize(64)
                    .Init();
                UIObject boardFPS = new UIObject();
                boardFPS.SetPosition(new Vector2f(32, 20));
                boardFPS.AddDrawble(boardFPSText);
                boardFPS.Updata = () => { boardFPSText.DisplayedString = ((int)Router.Init().graphicsControllers.fps).ToString(); };

                Canvas.addNode(boardFPS);
            }
        }

        public void Render(){

            Rey[] reyCastModel = reyCast.ReyCastWall(player, Config.config.fov, 64, Config.config.numRey);
            for (int i = 0; i < reyCastModel.Length; i++)
                DrawWall(reyCastModel[i], reyCastModel.Length, i);

            Canvas.EventUpdata();

            foreach (var el in Canvas.GetGraphicsPackages())
                window.Draw((Drawable)el);
        }



        private void DrawWall(Rey reyCast,int lenght ,int i) {
            RectangleShape wall = new RectangleShape();

            if (reyCast.rey != null)
                DrawWall(reyCast.rey, lenght, i);

            wall.Texture = ResurceMeneger.GetTexture(reyCast.Wall);

            wall.TextureRect = new IntRect(
                new Vector2i((int)(reyCast.offset * 16), 0),
                new Vector2i(1, 16)
            );

            wall.Position = new Vector2f(
                ((float)window.Size.X / lenght) * (float)(i),
               (window.Size.Y / 2) * ((1 + (player.angleY / 100)) - (1 / reyCast.ReyDistance))
            );

            wall.Size = new Vector2f(
                ((float)window.Size.X / lenght),
                (window.Size.Y / 2) * (1 + (1 / reyCast.ReyDistance)) - (window.Size.Y / 2) * (1 - (1 / reyCast.ReyDistance))
            );

            int brightness = ((int)(255 - (reyCast.ReyDistance * 10)));

            if (brightness < 0) brightness = 0;
            if (brightness > 255) brightness = 255;

            wall.FillColor = new Color((byte)(brightness), (byte)(brightness), (byte)(brightness));

            window.Draw(wall);

        }

    }
}
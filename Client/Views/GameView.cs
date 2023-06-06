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
using System;
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

            DrawFlore(reyCastModel.Length, reyCastModel[reyCastModel.Length / 2]);

            for (int i = 0; i < reyCastModel.Length; i++)
                DrawNoTransplentWall(reyCastModel[i], reyCastModel.Length, i);
            for (int i = 0; i < reyCastModel.Length; i++)
                DrawBackTransplentWall(reyCastModel[i], reyCastModel.Length, i);

            for (int i = 0; i < reyCastModel.Length; i++)
                DrawTransplentWall(reyCastModel[i], reyCastModel.Length, i);

            for (int i = 0; i < entities.Count; i++)
                DrawEntity(entities[i], reyCastModel);

            Canvas.EventUpdata();

            foreach (var el in Canvas.GetGraphicsPackages())
                window.Draw((Drawable)el);

            //float Scale = 6;
            //
            //for (int i = 0; i < Level.Size.Y; i++)
            //{
            //    for (int j = 0; j < Level.Size.X; j++)
            //    {
            //        RectangleShape rectangle = new RectangleShape(new Vector2f(Scale, Scale));
            //        rectangle.Position = new Vector2f(Scale * i, Scale * j);
            //        rectangle.FillColor = Color.Black;
            //        if (Level[j, i] != ' ')
            //            rectangle.FillColor = Color.Green;
            //
            //        window.Draw(rectangle);
            //    }
            //}
            //
            //VertexArray vertexArray = new VertexArray(PrimitiveType.TriangleFan);
            //
            //vertexArray.Append(new Vertex((player.Position + (player.Size / 2)) * Scale));
            //for (int i = 0; i < reyCastModel.Length; i++)
            //{
            //    Vertex newVertex = new Vertex(reyCastModel[i].ReyPoint * Scale);
            //    newVertex.Color = new Color(255, 255, 255, 255);
            //    vertexArray.Append(newVertex);
            //}
            //window.Draw(vertexArray);
            //
            //RectangleShape rect = new RectangleShape(new Vector2f(player.Size.X * Scale, player.Size.Y * Scale));
            //rect.Position = new Vector2f((player.Position * Scale).X, (player.Position * Scale).Y);
            //rect.FillColor = Color.Yellow;
            //window.Draw(rect);
            //
            //foreach (var el in entities) {
            //    RectangleShape enemi = new RectangleShape(new Vector2f(el.Size.X * Scale, el.Size.Y * Scale));
            //    enemi.Position = new Vector2f((el.Position * Scale).X, (el.Position * Scale).Y);
            //    enemi.FillColor = Color.Red;
            //    window.Draw(enemi);
            //}

        }

        private void DrawFlore(int numRey, Rey rey) {
            for (int i = 0; i < numRey; i++){

                float relativedistanse = 0;
                float distanseToFlore = 0;

                float floreX = MathF.Cos(player.angle) * relativedistanse;
                float floreY = MathF.Sin(player.angle) * relativedistanse;



                RectangleShape Flore = new RectangleShape();

                Flore.Texture = ResurceMeneger.LoadTexture(@"Resurces\Img\Flore\Ceiling.png");
                Flore.TextureRect = new IntRect(
                    new Vector2i((int)((16 / (float)(floreX - (int)floreX)) * i), 0),
                    new Vector2i((int)1, 16)
                );

                int brightness = ((int)(255 - (relativedistanse * 10)));

                if (brightness < 0) brightness = 0;
                if (brightness > 255) brightness = 255;

                Flore.FillColor = new Color((byte)(brightness), (byte)(brightness), (byte)(brightness));


                Flore.Position = new Vector2f(
                ((float)window.Size.X / numRey) * (float)(i),
               (window.Size.Y / 2) * ((1 + (player.angleY / 100)) - (1 / relativedistanse))
            );

                //Flore.Size = new Vector2f(
                //    ((float)window.Size.X / numRey),
                //    (window.Size.Y / 2) * (1 + (1 / relativedistanse)) - (window.Size.Y / 2) * (1 - (1 / reyCast.ReyDistance))
                //);


            }

        }

        public void DrawTransplentWall(Rey reyCast, int lenght, int i) {
            if(reyCast.rey != null)
                DrawWall(reyCast, lenght, i);
        }

        public void DrawNoTransplentWall(Rey reyCast, int lenght, int i){
            if (reyCast.rey == null)
                DrawWall(reyCast, lenght, i);
        }

        public void DrawBackTransplentWall(Rey reyCast, int lenght, int i) {
            if (reyCast.rey != null) {
                DrawBackTransplentWall(reyCast.rey, lenght, i);
                DrawWall(reyCast.rey, lenght, i);
            }
        }

        private void DrawWall(Rey reyCast,int lenght ,int i) {



            RectangleShape wall = new RectangleShape();

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

        private void DrawEntity(Entity entity, Rey[] reys) {
            ShortEntity();
            float SpriteDir = ((MathF.Atan2(
                entity.Position.Y - player.Position.Y,
                entity.Position.X - player.Position.X
            ) / MathF.PI) * 180);

            float SpriteDelta = (player.angle - SpriteDir);
            SpriteDelta += 180;
            while (SpriteDelta > 90) SpriteDelta -= 360;
            while (SpriteDelta < -270) SpriteDelta += 360;

            float spriteDistanse = MathF.Sqrt(MathF.Pow(entity.Position.X - player.Position.X, 2) + MathF.Pow(entity.Position.Y - player.Position.Y, 2));
            float spriteScreanSize = MathF.Min(2000, (window.Size.Y / 2) * (1 + (1 / spriteDistanse)) - (window.Size.Y / 2) * (1 - (1 / spriteDistanse)));

            float coutReyFull = (((float)(1000) / spriteDistanse) / (window.Size.X / reys.Length));
            for (int i = 0; i < (int)coutReyFull; i++) {

                float offsetHorizontal = (SpriteDelta * (window.Size.X) / (Config.config.fov) + (window.Size.X) / 2 - spriteScreanSize / 2) + ((window.Size.X / reys.Length) * i);
                float offsetVertical = (window.Size.Y / 2) * ((1 + (player.angleY / 100)) - (1 / spriteDistanse));

                int reyIndex = (int)(offsetHorizontal / ((float)window.Size.X / (float)reys.Length));

                if (offsetHorizontal < 0 || offsetHorizontal > window.Size.X - 1) 
                    continue;
                if (CheckDistanse(reys[reyIndex]) < spriteDistanse)
                    continue;
                RectangleShape spriteEntity = new RectangleShape();

                spriteEntity.Texture = ResurceMeneger.LoadTexture(@"Resurces\Img\Entity\Enemy.png");
                spriteEntity.TextureRect = new IntRect(
                    new Vector2i((int)((16 / coutReyFull) * i), 0),
                    new Vector2i((int)1, 16)
                );

                int brightness = ((int)(255 - (spriteDistanse * 10)));

                if (brightness < 0) brightness = 0;
                if (brightness > 255) brightness = 255;

                spriteEntity.FillColor = new Color((byte)(brightness), (byte)(brightness), (byte)(brightness));


                spriteEntity.Position = new Vector2f(
                    offsetHorizontal,
                    offsetVertical
                );

                spriteEntity.Size = new Vector2f(
                    ((float)window.Size.X / (float)reys.Length),
                    spriteScreanSize
                );
                
                window.Draw(spriteEntity);
                CheckDraweble(reys[reyIndex], reys.Length, reyIndex, spriteDistanse);
            }
        }

        private float CheckDistanse(Rey rey) {
            if (rey.rey != null)
                return CheckDistanse(rey.rey);
            return rey.ReyDistance;
        }

        private void CheckDraweble(Rey reyCast, int lenght, int i, float spriteDistanse){
            if (reyCast.rey == null)
                return;
            if (reyCast.ReyDistance < spriteDistanse)
                DrawWall(reyCast, lenght, i);

        }

        private float CheckDistanse(Entity entity){
            return  MathF.Sqrt(MathF.Pow(entity.Position.X - player.Position.X, 2) + MathF.Pow(entity.Position.Y - player.Position.Y, 2));
        }

        private void ShortEntity() {
            for (var i = 1; i < entities.Count; i++){
                for (var j = 0; j < entities.Count - i; j++){
                    if (CheckDistanse(entities[j]) < CheckDistanse(entities[j + 1])){
                        var temp = entities[j];
                        entities[j] = entities[j + 1];
                        entities[j + 1] = temp;
                    }
                }
            }
        }
    
    }
}
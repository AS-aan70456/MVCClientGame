using Client.Interfeices;
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
using System.Linq;

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
            ReyContainer[] reyCastModel = reyCast.ReyCastWall(player, Config.config.fov, 64, Config.config.numRey);

            DrawFlore(reyCastModel, Config.config.fov, Config.config.numRey, 150);

            for (int i = 0; i < reyCastModel.Length; i++)
                DrawBackTransplentWall(reyCastModel[i].GetWallHit(), reyCastModel.Length, i);

            for (int i = 0; i < entities.Count; i++)
                DrawEntity(entities[i], reyCastModel);

            Canvas.EventUpdata();

            foreach (var el in Canvas.GetGraphicsPackages())
                window.Draw((Drawable)el);
        }

        private void DrawFlore(ReyContainer[] reyCast, float fov, float countRey, float step) {

            
        }

        public void DrawBackTransplentWall(IEnumerable<Hit> reyCast, int lenght, int i) {
            foreach (var hit in reyCast) {
                DrawWall((HitWall)hit, lenght, i);
            }
        }

        private void DrawWall(HitWall reyCast,int lenght ,int i) {
            RectangleShape wall = new RectangleShape();
            
            wall.Texture = ResurceMeneger.GetTexture(reyCast.Wall);
            wall.TextureRect = new IntRect(
                new Vector2i((int)(reyCast.offset * 16), 0),
                new Vector2i(1, 16)
            );

            wall.Position = new Vector2f(
                ((float)window.Size.X / lenght) * (float)(i),
               (window.Size.Y / 2) * ((1 + (player.angleY / 100)) - (1 / reyCast.ReyDistance)) //+ (player.PositionZ * window.Size.Y)
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

        private void DrawEntity(Entity entity, ReyContainer[] reys) {
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

        
            float coutReyFull = (((float)(window.Size.X) / spriteDistanse) / (window.Size.X / reys.Length));
            for (int i = 0; i < (int)coutReyFull; i++) {
        
                float offsetHorizontal = (SpriteDelta * (window.Size.X) / (Config.config.fov) + (window.Size.X) / 2 - spriteScreanSize / 2) + ((window.Size.X / reys.Length) * i);
                float offsetVertical = (window.Size.Y / 2) * ((1 + (player.angleY / 100)) - (1 / spriteDistanse));
        
                int reyIndex = (int)(offsetHorizontal / ((float)window.Size.X / (float)reys.Length));
        
                if (offsetHorizontal < 0 || offsetHorizontal > window.Size.X - 1) 
                    continue;
                if (reys[reyIndex].GetFirstHit(new HitWall()).ReyDistance < spriteDistanse)
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
        
        private void CheckDraweble(ReyContainer reyCast, int lenght, int i, float spriteDistanse){
            foreach (var hit in reyCast.GetWallHit()) 
                if (hit.ReyDistance < spriteDistanse)
                    DrawWall((HitWall)hit, lenght, i);
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
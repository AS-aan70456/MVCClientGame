using Client.Interfeices;
using Client.Models;
using Client.Services;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Views
{
    class GameView : IRender
    {

        public RenderWindow window { get; private set; }

        private ReyCastService reyCast;
        private List<Enemy> entities;
        private Player player;

        Texture Walss;
        Texture Walss2;
        Texture Window;
        Texture Door;
        Texture DoorOpen;

        public GameView(RenderWindow window, ReyCastService reyCast, Player player, List<Enemy> entities){
            this.window = window;
            this.reyCast = reyCast;
            this.player = player;
            this.entities = entities;

            Walss2 = ResurceMeneger.LoadTexture(@"Resurces\Img\Walss\Wall.png"); 
            Walss = ResurceMeneger.LoadTexture(@"Resurces\Img\Walss\Wall2.png");
            Window = ResurceMeneger.LoadTexture(@"Resurces\Img\Walss\Window.png"); 
            Door = ResurceMeneger.LoadTexture(@"Resurces\Img\Walss\Door.png"); 
            DoorOpen = ResurceMeneger.LoadTexture(@"Resurces\Img\Walss\DoorOpen.png");
        }

        public void Render(){
            Router router = Router.Init();

            Rey[] reyCastModel = reyCast.ReyCastWall(player, 70, 16, 1080);
            for (int i = 0; i < reyCastModel.Length; i++){
                DrawWall(reyCastModel[i], reyCastModel.Length, i);
            }

            for (int i = 0; i < router.maps.Size.X; i++){
                for (int j = 0; j < router.maps.Size.Y; j++){
                    RectangleShape rectangle = new RectangleShape(new Vector2f(10, 10));
                    rectangle.Position = new Vector2f(10 * i, 10 * j);
                    rectangle.FillColor = Color.Black;
                    if (router.maps.Map[i + (j * router.maps.Size.X)] != ' ')
                        rectangle.FillColor = Color.Green;
                    if (router.maps.Map[i + (j * router.maps.Size.X)] == '5')
                        rectangle.FillColor = Color.Red;

                    window.Draw(rectangle);
                }
            }

            VertexArray vertexArray = new VertexArray(PrimitiveType.TriangleFan);

            vertexArray.Append(new Vertex((player.Position + (player.Size / 2)) * 10));
            for (int i = 0; i < reyCastModel.Length; i++){
                Vertex newVertex = new Vertex(reyCastModel[i].ReyPoint * 10);
                newVertex.Color = new Color(255, 255, 255, 255);
                vertexArray.Append(newVertex);
            }
            window.Draw(vertexArray);

            RectangleShape rect = new RectangleShape(new Vector2f(player.Size.X * 10, player.Size.Y * 10));
            rect.Position = (player.Position * 10);
            rect.FillColor = Color.Yellow;
            window.Draw(rect);
        }

    

        private void DrawWall(Rey reyCast,int lenght ,int i) {
            RectangleShape wall = new RectangleShape();

            if (reyCast.rey != null)
                DrawWall(reyCast.rey, lenght, i);

            if (reyCast.Wall == '1')
                wall.Texture = Walss;
            else if (reyCast.Wall == '2')
                wall.Texture = Walss2;
            else if (reyCast.Wall == '3')
                wall.Texture = Window;
            else if (reyCast.Wall == '4')
                wall.Texture = Door;
            else if (reyCast.Wall == '5')
                wall.Texture = DoorOpen;

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
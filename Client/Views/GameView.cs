using Client.Interfeices;
using Client.Models;
using Client.ViewModel;
using Services;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Views{
    class GameView : IRender{

        public RenderWindow window { get; set; }

        private ReyCastService reyCast;
        private List<Enemy> entities;
        private Maps map;
        private Player player;

        Texture Walss;
        Texture Walss2;
        Texture Window;
        Texture Door;

        public GameView(RenderWindow window, ReyCastService reyCast, Maps map, Player player, List<Enemy> entities) {
            this.window = window;
            this.reyCast = reyCast;
            this.player = player;
            this.entities = entities;
            this.map = map;

            Walss2 = new Texture(AppDomain.CurrentDomain.BaseDirectory + @"Resurces\Img\Walss\Wall.png");
            Walss = new Texture(AppDomain.CurrentDomain.BaseDirectory + @"Resurces\Img\Walss\Wall2.png");
            Window = new Texture(AppDomain.CurrentDomain.BaseDirectory + @"Resurces\Img\Walss\Window.png");
            Door = new Texture(AppDomain.CurrentDomain.BaseDirectory + @"Resurces\Img\Walss\Door.png");
        }

        public void Render(){

            ReyCastViewModel[] reyCastModel = reyCast.ReyCast(player);
            string map = this.map.GetMap();

            RectangleShape wall = new RectangleShape();
            

            for (int i = 0; i < reyCast.CountRey; i++){

                if (reyCastModel[i].Wall == '1')
                    wall.Texture = Walss;
                else if(reyCastModel[i].Wall == '2')
                    wall.Texture = Walss2;
                else if(reyCastModel[i].Wall == '3')
                    wall.Texture = Window;
                else if(reyCastModel[i].Wall == '4')
                    wall.Texture = Door;



                wall.TextureRect = new IntRect(
                    new Vector2i((int)(reyCastModel[i].offset * 16), 0),
                    new Vector2i(1, 16)
                );


                wall.Size = new Vector2f(
                    ((float)window.Size.X / reyCast.CountRey),
                    (window.Size.Y / 2) * (1 + (1 / reyCastModel[i].ReyDistance)) - (window.Size.Y / 2) * (1 - (1 / reyCastModel[i].ReyDistance))
                );

                wall.Position = new Vector2f(
                    ((float)window.Size.X / reyCast.CountRey) * (float)(i),
                   (window.Size.Y / 2) * (1 - (1 / reyCastModel[i].ReyDistance))
                );

                int brightness = ((int)(255 - (reyCastModel[i].ReyDistance * 20)));

                if (brightness < 0)
                    brightness = 0;

                wall.FillColor = new Color(255, 255, 255, (byte)brightness);

                window.Draw(wall);
            }



            for (int i = 0; i < this.map.Size.X ; i++) {
                for (int j = 0; j < this.map.Size.Y + 1; j++) {
                    RectangleShape rectangle = new RectangleShape(new Vector2f(10 , 10));
                    rectangle.Position = new Vector2f(10 * i, 10 * j);
                        rectangle.FillColor = Color.Black;
                    if (map[i + (j * this.map.Size.X)] != ' ')
                        rectangle.FillColor = Color.Green;
                    window.Draw(rectangle);
                }
            }

            

            VertexArray vertexArray = new VertexArray(PrimitiveType.TriangleFan);

            vertexArray.Append(new Vertex(player.Position * 10 ) );
            for (int i = 0; i < reyCast.CountRey; i++){
                Vertex newVertex = new Vertex(reyCastModel[i].ReyPoint * 10);
                newVertex.Color = new Color(255, 255, 255 ,200);
                vertexArray.Append(newVertex);
            }
            vertexArray.Append(new Vertex(player.Position * 10));
            window.Draw(vertexArray);

            RectangleShape circle = new RectangleShape(new Vector2f(10, 10));
            circle.Rotation = player.angle;
            circle.Position = (player.Position * 10) - new Vector2f(5 ,5);
            circle.FillColor = Color.Yellow;
            window.Draw(circle);
        }

    }
}

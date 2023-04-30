using Client.Interfeices;
using Client.Models;
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

        public GameView(RenderWindow window, ReyCastService reyCast, Maps map, Player player, List<Enemy> entities) {
            this.window = window;
            this.reyCast = reyCast;
            this.player = player;
            this.entities = entities;
            this.map = map;
        }

        public void Render(){
            reyCast.ReyCast(player);
            string map = this.map.GetMap();
            for (int i = 0; i < this.map.Size.X ; i++) {
                for (int j = 0; j < this.map.Size.Y + 1; j++) {
                    RectangleShape rectangle = new RectangleShape(new Vector2f(10 , 10));
                    rectangle.Position = new Vector2f(10 * i, 10 * j);
                        rectangle.FillColor = Color.Green;
                    if (map[i + (j * this.map.Size.X)] == '#')
                        window.Draw(rectangle);


                }
            }

            CircleShape circle = new CircleShape(5,25);
            circle.Rotation = player.angle;
            circle.Position = (player.Position * 10) ;
            circle.FillColor = Color.Yellow;
            window.Draw(circle);

            VertexArray vertexArray = new VertexArray(PrimitiveType.TriangleFan);

            vertexArray.Append(new Vertex(player.Position * 10 ) );
            for (int i = 0; i < reyCast.ReyPoint.Length; i++){
                Vertex newVertex = new Vertex(reyCast.ReyPoint[i] * 10);
                newVertex.Color = new Color(255, 255, 255 ,200);
                vertexArray.Append(newVertex);
            }
            vertexArray.Append(new Vertex(player.Position * 10));
            window.Draw(vertexArray);

            for (int i = 0;i < reyCast.ReyDistance.Length; i++) {
                RectangleShape rectangle = new RectangleShape(new Vector2f(window.Size.X / reyCast.ReyDistance.Length, reyCast.ReyDistance[i]));
                rectangle.Position = new Vector2f((window.Size.X / reyCast.ReyDistance.Length) * i, reyCast.ReyDistance[i] / 2);
                rectangle.FillColor = new Color();
                window.Draw(rectangle);
            }
            
        }

    }
}

using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models{
    class Entity{

        public Vector2f Position { get; set; }
        public Vector2f Size { get; protected set; }
        public float angle { get; protected set; }

        public Entity(Vector2f Position, Vector2f Size, float angle) {
            this.Size = Size;
            this.Position = Position;
            this.angle = angle;
        }

        public Entity(){
            Size = new Vector2f(0.5f, 0.5f);
            Position = new Vector2f(0, 0);
            angle = 180;
        }

        public void Move(Vector2f velocity){
            Router router = Router.Init();

            float X = 0;
            float Y = 0;

            float X1 = 0;
            float Y1 = 0;

            X = ((float)((MathF.Cos((((angle) * MathF.PI) / 180)) * velocity.X)));
            X1 = ((float)((MathF.Cos((((90 + angle) * MathF.PI) / 180)) * velocity.Y)));

            Y = ((float)((MathF.Sin((((angle) * MathF.PI) / 180)) * velocity.X)));
            Y1 = ((float)((MathF.Sin((((90 - angle) * MathF.PI) / 180)) * velocity.Y)));

            Position -= new Vector2f(0, Y);
            Position -= new Vector2f(0, Y1);

            float dy = Position.Y;

            for (int i = (int)Position.Y; i <= (int)(Position.Y + Size.Y); i++){
                for (int j = (int)Position.X; j <= (int)(Position.X + Size.X); j++){
                    if (router.maps.IsCollision(router.maps[i, j])){
                        if ((Position.Y - (int)Position.Y) < 0.9)
                            dy = i - (Size.Y + 0.01f);
                        else
                            dy = i + 1.01f;
                    }
                }
            }
            
            Position = new Vector2f(Position.X, dy);

            Position -= new Vector2f(X, 0);
            Position -= new Vector2f(X1, 0);

            float dx = Position.X;

            for (int i = (int)Position.Y; i <= (int)(Position.Y + Size.Y); i++){
                for (int j = (int)Position.X; j <= (int)(Position.X + Size.X); j++){
                    if (router.maps.IsCollision(router.maps[i, j])){
                        if ((Position.X - (int)Position.X) < 0.9)
                            dx = j - (Size.X + 0.01f);
                        else
                            dx = j + 1.01f;
                    }
                }
            }


            Position = new Vector2f(dx, Position.Y);

        }

        public void Rotate(float angle){
            this.angle += angle;

            

            

           //for (int i = (int)entity.Position.Y; i <= (int)(entity.Position.Y + entity.Size.Y); i++)
           //{
           //    for (int j = (int)entity.Position.X; j <= (int)(entity.Position.X + entity.Size.X); j++)
           //    {
           //        if (map.IsCollision(map[i, j]))
           //        {
           //            if ((entity.Position.X - (int)entity.Position.X) < 0.8)
           //                dx = j - (entity.Size.X + 0.01f);
           //            else
           //                dx = j + 1.01f;
           //        }
           //    }
           //}
           //
           //entity.Position = new Vector2f(dx, 0);
           //entity.Position = new Vector2f(0, dy);

        }
    }
}

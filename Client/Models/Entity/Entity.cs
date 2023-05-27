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

        Level Level;

        public Entity(Level Level) {
            this.Level = Level;
        }

        // Movement and collision function
        public void Move(Vector2f velocity){

            //velocity = velocity * Router.Init().graphicsControllers.time;

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
            // Collision Y
            for (int i = (int)Position.Y; i <= (int)(Position.Y + Size.Y); i++)
            {
                for (int j = (int)Position.X; j <= (int)(Position.X + Size.X); j++)
                {
                    if (Level.IsCollision(Level[i, j]))
                    {
                        if ((Position.Y - (int)Position.Y) < 0.8)
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
            // Collision X
            for (int i = (int)Position.Y; i <= (int)(Position.Y + Size.Y); i++)
            {
                for (int j = (int)Position.X; j <= (int)(Position.X + Size.X); j++)
                {
                    if (Level.IsCollision(Level[i, j]))
                    {
                        if ((Position.X - (int)Position.X) < 0.8)
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
        }
    }
}

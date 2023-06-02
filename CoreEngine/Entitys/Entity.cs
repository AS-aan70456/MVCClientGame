using CoreEngine.System;
using SFML.System;
using System;

namespace CoreEngine.Entitys{
    public abstract class Entity{

        public Vector3f Position { get; set; }
        public Vector3f Size { get; protected set; }
        public float angle { get; protected set; }

        private Level Level;

        public Entity(Level Level) {
            this.Level = Level;
        }

        public Entity(EntitySettings settings){
            this.Level = settings.Level;
            this.Position = settings.Position;
            this.Size = settings.Size;
            this.angle = settings.angle;
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

            Position -= new Vector3f(0, Y, 0);
            Position -= new Vector3f(0, Y1, 0);

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

            Position = new Vector3f(Position.X, dy, 0);

            Position -= new Vector3f(X, 0, 0);
            Position -= new Vector3f(X1, 0, 0);

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


            Position = new Vector3f(dx, Position.Y, 0);
        }

        public void Rotate(float angle){
            this.angle += angle;
        }
    }
}

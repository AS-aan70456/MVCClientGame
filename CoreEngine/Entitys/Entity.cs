using CoreEngine.System;
using SFML.System;
using System;

namespace CoreEngine.Entitys
{
    public abstract class Entity
    {

        // The position of the entity.
        public Vector2f Position { get; protected set; }

        // The size of the entity.
        public Vector2f Size { get; protected set; }

        // The angle of the entity.
        public float angle { get; protected set; }

        // The position on the Z-axis (used for 3D calculations, may not be fully implemented).
        public float PositionZ { get; protected set; }

        // The level in which the entity exists.
        private Level Level;

        // Constructor that takes a Level object.
        public Entity(Level Level)
        {
            this.Level = Level;
        }

        // Constructor that takes an EntitySettings object.
        public Entity(EntitySettings settings)
        {
            this.Level = settings.Level;
            this.Position = settings.Position;
            this.Size = settings.Size;
            this.angle = settings.angle;
        }

        // Movement and collision function.
        public void Move(Vector2f velocity)
        {

            // Calculate the movement along X and Y axes based on the entity's angle.
            float X = ((float)((MathF.Cos((((angle) * MathF.PI) / 180)) * velocity.X)));
            float X1 = ((float)((MathF.Cos((((90 + angle) * MathF.PI) / 180)) * velocity.Y)));

            float Y = ((float)((MathF.Sin((((angle) * MathF.PI) / 180)) * velocity.X)));
            float Y1 = ((float)((MathF.Sin((((90 - angle) * MathF.PI) / 180)) * velocity.Y)));

            // Apply the movement and collision detection along the Y-axis.
            Position -= new Vector2f(0, Y);
            Position -= new Vector2f(0, Y1);

            float dy = Position.Y;
            // Collision detection along the Y-axis.
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

            // Apply the movement and collision detection along the X-axis.
            Position -= new Vector2f(X, 0);
            Position -= new Vector2f(X1, 0);

            float dx = Position.X;
            // Collision detection along the X-axis.
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

        // Rotate the entity by a given angle.
        public void Rotate(float angle)
        {
            this.angle += angle;
        }
    }
}

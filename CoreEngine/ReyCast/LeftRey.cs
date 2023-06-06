using SFML.System;
using System;

namespace CoreEngine.ReyCast{
    class LeftRey : IStrategyReyCanculate{

        public Vector2f StartReyPos(Vector2f Position, float angle)
        {

            Vector2f deltePosition = new Vector2f()
            {
                X = (Position.X - (int)Position.X),
                Y = (Position.Y - (int)Position.Y)
            };

            Position.X += -deltePosition.X;
            Position.Y += ((-deltePosition.X) / MathF.Tan((angle * MathF.PI) / 180));

            Position -= new Vector2f(0.00001f, 0.00001f);

            return Position;
        }

        public Vector2f NextReyPos(float angle)
        {
            return new Vector2f(
                -1f,
                -(1f / MathF.Tan((angle * MathF.PI) / 180))
            );
        }

        public float GetOfset(Vector2f pos)
        {
            return (float)(pos.Y - (int)pos.Y);
        }

        public Vector2f GetSide()
        {
            return new Vector2f(-1, 0);
        }
    }
}

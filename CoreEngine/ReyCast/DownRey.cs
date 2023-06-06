using SFML.System;
using System;

namespace CoreEngine.ReyCast
{
    class DownRey : IStrategyReyCanculate
    {

        public Vector2f StartReyPos(Vector2f Position, float angle)
        {
            Vector2f deltePosition = new Vector2f()
            {
                X = 1 - (Position.X - (int)Position.X),
                Y = 1 - (Position.Y - (int)Position.Y),
            };

            Position.X -= (((-deltePosition.Y)) / MathF.Tan((angle * MathF.PI) / 180));
            Position.Y -= (-deltePosition.Y);

            return Position;
        }

        public Vector2f NextReyPos(float angle)
        {
            return new Vector2f(
                (1f / MathF.Tan((angle * MathF.PI) / 180)),
                1f
            );
        }

        public float GetOfset(Vector2f pos)
        {
            return (float)(pos.X - (int)pos.X);
        }

        public Vector2f GetSide(){
            return new Vector2f(0, 1);
        }
    }


}

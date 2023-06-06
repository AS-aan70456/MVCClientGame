using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreEngine.ReyCast
{
    class RightRey : IStrategyReyCanculate
    {

        public Vector2f StartReyPos(Vector2f Position, float angle)
        {
            Vector2f deltePosition = new Vector2f()
            {
                X = 1 - (Position.X - (int)Position.X),
                Y = 1 - (Position.Y - (int)Position.Y),
            };

            Position.X -= (-deltePosition.X);
            Position.Y -= (((-deltePosition.X)) / MathF.Tan((angle * MathF.PI) / 180));

            return Position;
        }

        public Vector2f NextReyPos(float angle)
        {
            return new Vector2f(
                1f,
                (1f / MathF.Tan((angle * MathF.PI) / 180))
            );
        }

        public float GetOfset(Vector2f pos)
        {
            return (float)(pos.Y - (int)pos.Y);
        }

        public Vector2f GetSide() {
            return new Vector2f(1, 0);
        }
    }
}

using CoreEngine.System;
using SFML.System;

namespace CoreEngine.Entitys
{
    public class Player : Entity{

        public float angleY { get; protected set; }

        public Player(Level level) : base(level){
            Position = new Vector2f(level.SpawnPoint.X, level.SpawnPoint.Y);
            Size = new Vector2f(0.7f, 0.7f);
            PositionZ = 0.5f;
        }

        public void RotateY(float angle){
            this.angleY += angle;
        }
    }
}

using CoreEngine.System;
using SFML.System;

namespace CoreEngine.Entitys
{
    public class Player : Entity{

        public float angleY { get; protected set; }

        public Player(Level level) : base(level){
            Position = new Vector3f(level.SpawnPoint.X, level.SpawnPoint.Y,0);
            Size = new Vector3f(0.7f, 0.7f, 0);
        }

        public void RotateY(float angle){
            this.angleY += angle;
        }
    }
}

using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models{
    class Entity{

        public Vector2f Position { get; }
        public float angle { get; }

        public Entity(Vector2f Position, float angle) {
            this.Position = Position;
            this.angle = angle;
        }

        public Entity(){
            Position = new Vector2f(0, 0);
            angle = 180;
        }

        //public void Move(Vector2f velocity) {
        //
        //}
    }
}

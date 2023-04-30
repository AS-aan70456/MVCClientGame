using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models{
    class Entity{

        public Vector2f Position { get; set; }
        public float angle { get; set; }
        public float fov { get; set; }
        public float distanse { get; set; }

        public Entity(Vector2f Position, float angle) {
            this.Position = Position;
            this.angle = angle;
            fov = 80;
            distanse = 100;
        }

        public Entity(){
            Position = new Vector2f(0, 0);
            angle = 180;
            fov = 80;
            distanse = 100;
        }
    }
}

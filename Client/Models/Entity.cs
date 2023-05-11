using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models{
    class Entity{

        public Vector2f Position { get; protected set; }
        public float angle { get; protected set; }

        public Entity(Vector2f Position, float angle) {
            this.Position = Position;
            this.angle = angle;
        }

        public Entity(){
            Position = new Vector2f(0, 0);
            angle = 180;
        }

        public void Move(Vector2f velocity){

            float X = 0;
            float Y = 0;

            float X1 = 0;
            float Y1 = 0;

            X = ((float)((MathF.Cos((((angle ) * MathF.PI) / 180)) * velocity.X)));
            Y = ((float)((MathF.Sin((((angle) * MathF.PI) / 180)) * velocity.X)));

            X1 = ((float)((MathF.Cos((((90 + angle ) * MathF.PI) / 180)) * velocity.Y)));
            Y1 = ((float)((MathF.Sin((((90 - angle) * MathF.PI) / 180)) * velocity.Y)));

            Position -= new Vector2f( X, Y);
            Position -= new Vector2f(X1, Y1);

        }

        public void Rotate(float angle){
            this.angle += angle;
        }
    }
}

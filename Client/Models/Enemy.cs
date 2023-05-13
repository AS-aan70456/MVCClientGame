using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models{
    class Enemy : Entity{

        public Enemy(Vector2f Position, Vector2f Size, float angle) : base(Position, Size, angle){

        }

        public Enemy() : base(){

        }

    }
}

using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models{
    class Enemy : Entity{

        public Enemy(Vector2f Position, float angle) : base(Position, angle){

        }

        public Enemy() : base(){

        }

    }
}

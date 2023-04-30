using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models{
    class Player : Entity{

        public Player(Vector2f Position, float angle) : base(Position, angle) {
        
        }

        public Player() : base(){

        }
    }
}

using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models{
    class Player : Entity{

        public Player(Level level) : base(level){
            Position = new Vector2f(2,2);
            Size = new Vector2f(0.7f, 0.7f);
        }
    }
}

using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Dungeons{
    class Chank{

        public Vector2i Position { get; private set; }

        private Room room;

        public Room Room { 
            get {
                if (room != null)
                    return room;
                else
                    return new Room();
            }
            set { room = value; }
        }

        public Chank(Vector2i Position) {
            this.Position = Position;
        }

    }
}

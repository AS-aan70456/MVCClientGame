using SFML.System;
using System;

namespace Client.Models.Dungeons
{
    // Represents a chunk in the dungeon
    class Chank
    {

        public Vector2i Position { get; private set; } // The position of the chunk

        private Room room; // The associated room within the chunk

        public Room Room
        {
            get
            {
                if (room != null)
                    return room; // Return the associated room if set
                else
                    return new Room(); // Return an empty room if room is not set
            }
            set { room = value; } // Set the associated room
        }

        public Chank(Vector2i Position)
        {
            this.Position = Position; // Initialize the position of the chunk
        }

    }
}

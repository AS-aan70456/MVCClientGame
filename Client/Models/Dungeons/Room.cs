using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Dungeons
{

    // Represents a room within the dungeon
    class Room
    {

        public Vector2i Position { get; set; } // The position of the room
        public Vector2i Size { get; private set; } // The size of the room
        public Vector2i Center { get { return (Position + (Size / 2)); } } // The center position of the room

        public char[,] Structure { get; private set; } // The structure of the room (wall, floor, etc.)

        public Room()
        {
            Position = new Vector2i();
            Size = new Vector2i();
        }

        // Generates a room with specified size and position within a chunk
        public static Room GenerateRoom(Vector2i Size, Vector2i chankPos, int chankSize)
        {
            Room room = new Room();

            room.Structure = new char[Size.X, Size.Y];
            room.Size = Size;
            room.Position = new Vector2i(chankPos.X + ((chankSize - Size.X) / 2), chankPos.Y + ((chankSize - Size.Y) / 2));

            // Fills the structure of the room with walls and floors
            for (int i = 0; i < Size.Y; i++)
            {
                for (int j = 0; j < Size.X; j++)
                {
                    if (i == 0 || j == 0 || i == Size.Y - 1 || j == Size.X - 1)
                    {
                        if ((i + j) % 5 != 0)
                            room.Structure[j, i] = '1'; // Wall
                        else
                            room.Structure[j, i] = '2'; // Door
                    }
                    else
                    {
                        room.Structure[j, i] = ' '; // Floor
                    }
                }
            }

            return room;
        }

        // Checks if the room is too close to any of the existing rooms
        public bool CheckColisionRoom(List<Room> rooms)
        {
            foreach (var room in rooms) if (TooCloseTo(room))
                    return true;
            return false;
        }

        // Checks if the room is too close to another room
        private bool TooCloseTo(Room room)
        {
            return ((Position.X > room.Position.X && Position.X < room.Position.X + room.Size.Y) || (room.Position.X > Position.X && Position.X < Position.X + Size.Y)) &&
                ((Position.Y > room.Position.Y && Position.Y < room.Position.Y + room.Size.X) || (room.Position.Y > Position.Y && Position.Y < Position.Y + Size.X));

        }
    }
}

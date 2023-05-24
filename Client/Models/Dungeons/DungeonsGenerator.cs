using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Dungeons{
   

    class DungeonsGenerator{

        

        public DungeonsGenerator() {
        
        }

        public Level GenerateDungeon(int key) {
            Random random = new Random(key);
            List<Room> rooms = new List<Room>();

            List<Vector2i> roomsPosCenter = new List<Vector2i>();

            int CountRoom = 6;

            //GenerationDungeon
            for (int i = 0; i < CountRoom; i++) {
                Room newRoom = Room.GenerateRoom(
                    new Vector2i(random.Next(8) + 6, random.Next(8) + 6)
                );
                newRoom.Position = GetRandomPointInCircle(random.Next(360), random.Next(6) + 6);
                while (newRoom.CheckColisionRoom(rooms)){
                    newRoom.Position = GetRandomPointInCircle(random.Next(360), random.Next(6) + 6);
                }

                rooms.Add(newRoom);
                roomsPosCenter.Add(newRoom.Center);

            }

            char[,] Dangeons = RoomsToCharArry(rooms);
            return new Level(ToString(Dangeons), new Vector2i(Dangeons.GetLength(0), Dangeons.GetLength(1)),new Vector2i(10, 10));
        }


        private Vector2i GetRandomPointInCircle(float radius, float disntanse) {
            return new Vector2i((int)(Math.Cos(radius) * disntanse), (int)(Math.Sin(radius) * disntanse));
        }

        private char[,] RoomsToCharArry(List<Room> rooms) {
            char[,] result;

            Vector2i MaxSize = new Vector2i();
            Vector2i MinSize = new Vector2i();
            for (int i = 0; i < rooms.Count; i++)
            {
                if (MaxSize.X < rooms[i].Size.X + rooms[i].Position.X) MaxSize.X = rooms[i].Size.X + rooms[i].Position.X;
                if (MaxSize.Y < rooms[i].Size.Y + rooms[i].Position.Y) MaxSize.Y = rooms[i].Size.Y + rooms[i].Position.Y;

                if (MinSize.X > rooms[i].Position.X) MinSize.X = rooms[i].Position.X;
                if (MinSize.Y > rooms[i].Position.Y) MinSize.Y = rooms[i].Position.Y;
            }

            Vector2i Size = (-MinSize) + MaxSize;

            result = new char[Size.X, Size.Y];

            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    result[i, j] = '1';
                }
            }

            for (int room = 0; room < rooms.Count; room++)
            {
                for (int i = (rooms[room].Position.Y) + (-MinSize.Y); i < (rooms[room].Position.Y + rooms[room].Size.Y) + (-MinSize.Y); i++)
                {
                    for (int j = (rooms[room].Position.X) + (-MinSize.X); j < (rooms[room].Position.X + rooms[room].Size.X) + (-MinSize.X); j++)
                    {
                        result[j, i] = rooms[room].Structure[j - ((rooms[room].Position.X) + (-MinSize.X)), i - ((rooms[room].Position.Y) + (-MinSize.Y))];
                    }
                }

            }

            return result;
        }

        private string ToString(char[,] Dangeons){
            StringBuilder @string = new StringBuilder();
            for (int i = 0; i < Dangeons.GetLength(1); i++) {
                for (int j = 0; j < Dangeons.GetLength(0); j++){
                    @string.Append(Dangeons[j,i]);
                }
            }
            return @string.ToString();
        }
    }
}

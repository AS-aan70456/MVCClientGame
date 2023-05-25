using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using Client.Models.Structure;

namespace Client.Models.Dungeons{
   

    class DungeonsGenerator{

        private Graph graph;
        private Random _Rand;

        private List<Chank> chanks = new List<Chank>();
        private List<Room> rooms = new List<Room>();
        private int chankSize;

        private Vector2i dungeonSize;
        private int roomCount;

        public DungeonsGenerator(Vector2i size, int roomCount, int chankSize) {
            this.dungeonSize = size;
            this.chankSize = chankSize;
            this.roomCount = roomCount;

            graph = new Graph();
        }

        public Level GenerateDungeon(int key) {
            _Rand = new Random(key);

            //GenerationDungeon
            GenerateChank();
            ShuffleChank();
            GenerateRoom();
            char[,] Dangeons = CreateCorridor(RoomsToCharArry(rooms));

            
            return new Level(ToString(Dangeons), new Vector2i(Dangeons.GetLength(0), Dangeons.GetLength(1)),rooms[0].Center);
        }

        private void GenerateChank() {
            Vector2i chankPos = new Vector2i();
            for (int y = 0; y < dungeonSize.Y / chankSize; y++) {
                for (int x = 0; x < dungeonSize.Y / chankSize; x++){
                    chankPos.X = x * chankSize;
                    chankPos.Y = y * chankSize;
                    chanks.Add(new Chank(chankPos));
                }
            }
        }

        private void ShuffleChank(){

            for (int i = chanks.Count - 1; i >= 1; i--){
                int j = _Rand.Next(i + 1);

                Chank currentChank = chanks[j];
                chanks[j] = chanks[i];
                chanks[i] = currentChank;
            }
        }

        private void GenerateRoom() {
            int minRoom = (int)(chankSize / 2.5f);
            int maxRoom = (int)(chankSize / 1f);

            for (int i = 0; i < roomCount; i++){
                Room room = Room.GenerateRoom(
                    new Vector2i(_Rand.Next(maxRoom - minRoom) + minRoom, _Rand.Next(maxRoom - minRoom) + minRoom),
                    chanks[i].Position,
                    chankSize
                );
                rooms.Add(room);
                graph.AddNode(room.Center);
                chanks[i].Room = room;
            }
        }

        public char[,] CreateCorridor(char[,] Dangeons) {
            List<NodeData> endPoinds = graph.GetDataNode();

            foreach (var el in endPoinds) {
                Vector2i currentPos = el.StartPos;
                Vector2i leght = el.StartPos - el.EndPos;

                int offsetX;
                if (leght.X > 0) 
                   offsetX = -1;
                else
                   offsetX = 1;

                int offsetY;
                if (leght.Y > 0)
                    offsetY = -1;
                else
                    offsetY = 1;

                for (int x = 0; x < Math.Abs(leght.X); x++) {
                    
                    if(Dangeons[currentPos.X, currentPos.Y] == '1')
                        Dangeons[currentPos.X, currentPos.Y] = '5';
                    else
                        Dangeons[currentPos.X, currentPos.Y] = ' ';
                    currentPos.X += offsetX;
                }
                for (int y = 0; y < Math.Abs(leght.Y); y++){
 
                    if (Dangeons[currentPos.X, currentPos.Y] == '1')
                        Dangeons[currentPos.X, currentPos.Y] = '5';
                    else
                        Dangeons[currentPos.X, currentPos.Y] = ' ';
                    currentPos.Y += offsetY;
                }

            }
            return Dangeons;
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

            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    result[x, y] = '2';
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

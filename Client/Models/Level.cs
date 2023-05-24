using Client.Models.Dungeons;
using Client.Services;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models{
    class Level {

        public Vector2i SpawnPoint { get; private set; }

        public Vector2i Size { get; private set; }
        public string Map { get; private set; }
        private static char[] Void;
        private static char[] Transparent;
        private static char[] Collision;

        public char this[int i,int j] { get { return Map[(int)i * Size.X + (int)j]; } }

        public Level(string Map, Vector2i Size, Vector2i SpawnPoint) {

            this.Map = Map;
            this.Size = Size;
            this.SpawnPoint = SpawnPoint;

            Void = new char[] { ' ' };
            Transparent = new char[] { '3', '5' };
            Collision = new char[] { '1', '2', '3', '4' };
        }

        public static Texture GetTexture(char ch){
            switch (ch) {
                case '1':
                    return ResurceMeneger.LoadTexture(@"Resurces\Img\Walss\Wall2.png");
                case '2':
                    return ResurceMeneger.LoadTexture(@"Resurces\Img\Walss\Wall.png");
                case '3':
                    return ResurceMeneger.LoadTexture(@"Resurces\Img\Walss\Window.png");
                case '4':
                    return ResurceMeneger.LoadTexture(@"Resurces\Img\Walss\Door.png");
                case '5':
                    return ResurceMeneger.LoadTexture(@"Resurces\Img\Walss\DoorOpen.png");
                default:
                    return ResurceMeneger.LoadTexture(@"Resurces\Img\Walss\Flore.jpg");
            }
        }

        public static bool IsVoid(char Cell) {
            for (int i = 0; i < Void.Length; i++) if (Cell == Void[i]) return true; return false;
        }

        public static bool IsTransparent(char Cell){
            for (int i = 0; i < Transparent.Length; i++) if (Cell == Transparent[i]) return true; return false;
        }

        public static bool IsCollision(char Cell){
            for (int i = 0; i < Collision.Length; i++) if (Cell == Collision[i]) return true; return false;
        }
    }
}

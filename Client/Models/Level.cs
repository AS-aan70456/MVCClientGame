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

        public Vector2i Size { get; private set; }
        public string Map { get; private set; }
        private static char[] Void;
        private static char[] Transparent;
        private static char[] Collision;

        public char this[int i,int j] { get { return Map[(int)i * Size.X + (int)j]; } }

        public Level() {
            Size = new Vector2i(24, 15);
            Map += "111111111121111111111211";
            Map += "1              1       1";
            Map += "2              5       1";
            Map += "1              4       1";
            Map += "1              1       1";
            Map += "112132112111511112   211";
            Map += "1        1             1";
            Map += "2        3             1";
            Map += "1        1             1";
            Map += "1        1             1";
            Map += "2111512111             2";
            Map += "1                      1";
            Map += "1                      1";
            Map += "1                      1";
            Map += "111111211111411112211111";

            Void = new char[] { ' ' };
            Transparent = new char[] { '3', '5' };
            Collision = new char[] { '1', '2', '3', '4' };
        }

        public void GenerateLevel(int key) {
            Random random = new Random(key);
            StringBuilder sBuilder = new StringBuilder();

            //sBuilder[];

            Map = sBuilder.ToString();
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

        public bool IsVoid(char Cell) {
            for (int i = 0; i < Void.Length; i++) if (Cell == Void[i]) return true; return false;
        }

        public bool IsTransparent(char Cell){
            for (int i = 0; i < Transparent.Length; i++) if (Cell == Transparent[i]) return true; return false;
        }

        public bool IsCollision(char Cell){
            for (int i = 0; i < Collision.Length; i++) if (Cell == Collision[i]) return true; return false;
        }
    }
}

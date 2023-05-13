using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models{
    class Maps {

        public Vector2i Size { get; private set; }
        public string Map { get; private set; }
        private char[] Void;
        private char[] Transparent;
        private char[] Collision;

        public char this[int i,int j] { get { return Map[(int)i * Size.X + (int)j]; } }

        public Maps() {
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

        // # 1 - wall
        // # 2 - wall2
        // # 3 - window
        // # 4 - dors
        // # 5 - dorsOpen


        public string GetMap() {
            return Map;
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

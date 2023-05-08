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


        public Maps() {
            Size = new Vector2i(24, 15);
            Map += "111111111111111111111111";
            Map += "1                      1";
            Map += "1                      1";
            Map += "1                      1";
            Map += "1                      1";
            Map += "1                      1";
            Map += "1                      1";
            Map += "1                      1";
            Map += "1                      1";
            Map += "1                      1";
            Map += "1                      1";
            Map += "1                      1";
            Map += "1                      1";
            Map += "1                      1";
            Map += "111111111111111111111111";



            Void = new char[] { ' ' };
            Transparent = new char[] { '3', '5' };
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

    }
}

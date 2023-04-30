using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models{
    class Maps{

        public Vector2i Size { get; private set; }
        private string Map { get; }

        public Maps() {
            Size = new Vector2i(46, 14);
            Map += "1111111111111111111111311111111111113111111111";
            Map += "1     ##                                     1";
            Map += "1     ##      2         2          2         3";
            Map += "3     33                                     1";
            Map += "1     ##                                     1";
            Map += "1     ##                                     3";
            Map += "3             2         2          2         1";
            Map += "1                                            1";
            Map += "1                                            1";
            Map += "4                   222          2222223222221";
            Map += "1                   222          2           1";
            Map += "1                   222          3           1";
            Map += "1                                2           1";
            Map += "1                                            1";
            Map += "1111111111311111111131111111311111111111141111";
        }

        // # 1 - wall
        // # 2 - wall2
        // # 3 - window
        // # 4 - dors

   

        public string GetMap() {
            return Map;
        }

    }
}

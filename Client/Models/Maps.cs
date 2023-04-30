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
            Size = new Vector2i(29, 14);
            Map += "#############################";
            Map += "#     ##                    #";
            Map += "#     ##                    #";
            Map += "#     ##                    #";
            Map += "#     ##                    #";
            Map += "#     ##                    #";
            Map += "#                           #";
            Map += "#                           #";
            Map += "#                           #";
            Map += "#                   ###     #";
            Map += "#                   ###     #";
            Map += "#      ##           ###     #";
            Map += "#                           #";
            Map += "#                           #";
            Map += "#############################";
        }

        public string GetMap() {
            return Map;
        }

    }
}

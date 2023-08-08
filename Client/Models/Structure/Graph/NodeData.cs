using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Structure
{

    // Represents data about a connection between two nodes in a graph
    class NodeData
    {

        public Vector2i StartPos { get; set; } // Starting position of the connection
        public Vector2i EndPos { get; set; } // Ending position of the connection

    }
}

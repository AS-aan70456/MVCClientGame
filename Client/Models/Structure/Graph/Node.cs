using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Structure
{

    // Represents a node in a graph
    class Node
    {

        public Vector2i PositionNode { get; set; } // Position of the node

        public List<Edge> Edges { get; set; } // List of edges connected to this node
        public Node Parent { get; set; } // Reference to the parent node in the graph

        // Constructor to initialize a node with a given position
        public Node(Vector2i nodeData)
        {
            PositionNode = nodeData; // Set the node's position
            Edges = new List<Edge>(); // Initialize the list of edges connected to this node
        }
    }
}

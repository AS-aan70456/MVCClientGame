using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Structure
{

    // Represents an edge between nodes in a graph
    class Edge
    {

        public int weight; // Weight or cost associated with the edge
        public Node AdjacentNode { get; set; } // The node connected by this edge

        public Edge(Node adjacentNode, int weight)
        {
            this.AdjacentNode = adjacentNode;
            this.weight = weight;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Structure{
    class Edge{

        public int weight;
        public Node AdjacentNode { get; set; }

        public Edge(Node adjacentNode, int weight) {
            this.AdjacentNode = adjacentNode;
            this.weight = weight;
        }
    }
}

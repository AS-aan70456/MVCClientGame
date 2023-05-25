using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Structure{
    class Graph{

        private List<Node> nodes;

        public Graph() {
            nodes = new List<Node>();
        }

        public void AddNode(Vector2i data){
            Node newNode = new Node(data);

            float globalDistance = 100000;
            Node accesNode = null;

            foreach (var node in nodes) {
                float a = (data.Y - node.PositionNode.Y);
                float b = (data.X - node.PositionNode.X);
                float Distance = MathF.Abs(MathF.Sqrt((a * a) + (b * b)));

                if (globalDistance > Distance) {
                    globalDistance = Distance;
                    accesNode = node;
                }
            }

            if (accesNode == null){
                nodes.Add(newNode);
                return;
            }
            newNode.Parent = accesNode;
            accesNode.Edges.Add(new Edge(newNode, 1));

            nodes.Add(newNode);
        }

        public List<NodeData> GetDataNode(){
            List<NodeData> result = new List<NodeData>();
            foreach (var el in nodes) {
                foreach (var point in el.Edges) {
                    result.Add(new NodeData() {
                        StartPos = el.PositionNode,
                        EndPos = point.AdjacentNode.PositionNode
                    });
                }
            }
            return result;
        }

    }
}

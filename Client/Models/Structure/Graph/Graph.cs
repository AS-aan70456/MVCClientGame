using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Structure
{

    // Represents a graph of nodes and edges
    class Graph
    {

        private List<Node> nodes; // List to store nodes in the graph

        public Graph()
        {
            nodes = new List<Node>(); // Initialize the list of nodes
        }

        // Adds a new node to the graph
        public void AddNode(Vector2i data)
        {
            Node newNode = new Node(data); // Create a new node with the given data

            float globalDistance = 100000; // A high initial value for global distance
            Node accesNode = null; // Reference to the node with the closest position

            // Find the nearest node to the new node's position
            foreach (var node in nodes)
            {
                float a = (data.Y - node.PositionNode.Y);
                float b = (data.X - node.PositionNode.X);
                float Distance = MathF.Abs(MathF.Sqrt((a * a) + (b * b)));

                // Update the nearest node if a closer node is found
                if (globalDistance > Distance)
                {
                    globalDistance = Distance;
                    accesNode = node;
                }
            }

            if (accesNode == null)
            {
                nodes.Add(newNode); // If no nearest node found, add the new node
                return;
            }
            newNode.Parent = accesNode; // Set the nearest node as parent
            accesNode.Edges.Add(new Edge(newNode, 1)); // Add an edge between the nearest node and the new node

            nodes.Add(newNode); // Add the new node to the list
        }

        // Get data about nodes and their adjacent nodes for corridor creation
        public List<NodeData> GetDataNode()
        {
            List<NodeData> result = new List<NodeData>(); // List to store node data
            foreach (var el in nodes)
            {
                foreach (var point in el.Edges)
                {
                    result.Add(new NodeData()
                    {
                        StartPos = el.PositionNode,
                        EndPos = point.AdjacentNode.PositionNode
                    }); // Add node data to the result list
                }
            }
            return result; // Return the list of node data
        }

    }
}

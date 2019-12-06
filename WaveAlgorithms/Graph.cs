using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveAlgorithms
{
    class Graph
    {
        private bool isDirected;
        public List<Node> nodes;
        public Graph(bool isDirected, int vertCount, IEnumerable<Tuple<int, int>> edges)
        {
            this.isDirected = isDirected;
            nodes = new List<Node>();
            for (int i = 0; i < vertCount; i++)
            {
                var node = new Node();
                nodes.Add(node);
            }
            foreach(var edge in edges)
            {
                nodes[edge.Item1].AddNeigbour(nodes[edge.Item2]);
                if (!isDirected)
                {
                    nodes[edge.Item2].AddNeigbour(nodes[edge.Item1]);
                }
            }
        }
    }
}

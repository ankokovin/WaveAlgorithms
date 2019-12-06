using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveAlgorithms
{
    class Node
    {
        private string Name;
        public List<Node> Neigbours;
        public Node()
        {
            Name = "Client_" + (Guid.NewGuid()).ToString();
            Neigbours = new List<Node>();
        }
        public void AddNeigbour(Node node)
        {
            Neigbours.Add(node);
        }
        public override string ToString()
        {
            return Name;
        }

        public string Args()
        {
            string result = Name;
            foreach (var name in Neigbours.Select(x => x.Name)) result += " " + name;
            return result;
        }
    }
}

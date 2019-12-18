using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveAlgorithms
{
    class Node
    {
        private static int cnt = 0;
        private string Name;
        public List<Node> Neigbours;
        public Node()
        {
            Name = "Client_" +(++cnt);
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

        public string Args(decimal val)
        {
            string result = Name+" " + val + " ";
            foreach (var name in Neigbours.Select(x => x.Name)) result += " " + name;
            return result;
        }
    }
}

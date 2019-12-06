using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaveAlgorithms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(var node in graph.nodes)
            {
                Program.StartChild(node.Args());
            }
        }
        private Graph graph;
        private void bParseGraph_Click(object sender, EventArgs e)
        {
            var input = rtbGraphStructure.Lines;
            int vertCount = int.Parse(input[0]);
            var edges = new List<Tuple<int, int>>();
            foreach (var str in input.Skip(1))
            {
                int[] nums = str.Split().Select(x => int.Parse(x)).ToArray();
                edges.Add(new Tuple<int, int>(nums[0], nums[1]));
            }
            graph = new Graph(cbIsDirected.Checked, vertCount, edges);
            bStart.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(var process in Program.ChildProcesses)
            {
                process.Kill();
            }
        }
    }
}

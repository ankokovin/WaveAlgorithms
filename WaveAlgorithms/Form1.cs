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
            for (int i = 0; i < graph.nodes.Count; i++)
            {
                tableLayoutPanel1.Controls[3 * i + 2].Enabled = true;
            }
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
            tableLayoutPanel1.RowCount  = vertCount;
            for (int i = 0; i < vertCount; i++)
            {
                var label = new Label();
                label.Text = graph.nodes[i].ToString();
                tableLayoutPanel1.Controls.Add(label, 0, i);
                var numeric = new NumericUpDown();
                tableLayoutPanel1.Controls.Add(numeric, 1, i);
                var button = new Button();
                button.Text = "Начать алгоритм";
                button.Enabled = false;
                tableLayoutPanel1.Controls.Add(button, 2, i);

            }
            bStart.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}

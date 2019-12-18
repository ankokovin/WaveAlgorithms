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
        public bool started = false;
        public decimal[] vals;
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
                Program.StartChild(graph.nodes[i].Args(vals[i]));
            }
            started = true;
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
            graph = new Graph(false, vertCount, edges);
            tableLayoutPanel1.RowCount  = vertCount;
            vals = new decimal[vertCount];
            for (int i = 0; i < vertCount; i++)
            {
                vals[i] = 1;
                var id = i;
                var label = new Label();
                label.Text = graph.nodes[i].ToString();
                tableLayoutPanel1.Controls.Add(label, 0, i);
                var numeric = new NumericUpDown();
                numeric.Value = 1;
                tableLayoutPanel1.Controls.Add(numeric, 1, i);
                var button = new Button();
                button.Text = "Обновить";
                button.Enabled = false;
                tableLayoutPanel1.Controls.Add(button, 2, i);
                button.MouseClick+= (_sender, _e) => UpdateValue(id, numeric.Value);
                var button2 = new Button();
                button2.Text = "Волна";
                tableLayoutPanel1.Controls.Add(button2, 3, i);
                button2.MouseClick += (_sender, _e) => Start(id);

            }
            bStart.Enabled = true;
            
        }

        private void Start(int idx)
        {
            if (started)
                Program.Send(graph.nodes[idx].ToString(), "start");
        }


        private void UpdateValue(int idx, decimal newVal)
        {
            vals[idx] = newVal;
            if (started)
            {
                SendNewVal(idx, newVal);
            }
        }

        private void SendNewVal(int idx, decimal newVal)
        {
            Program.Send(graph.nodes[idx].ToString(), "set " + newVal);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}

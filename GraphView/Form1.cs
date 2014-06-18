using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphImplementation;

namespace GraphView
{
    public partial class Form1 : Form
    {
        Graph g = new Graph();
        public Form1()
        {
            InitializeComponent();
            g.OnChange += Update;
        }

        public void Update(Graph sender)
        {
            label1.Text =
                           "Is tree: \t" + g.IsTree().ToString() + "\n" +
                            "Is connected: \t" + g.IsConnected().ToString() + "\n" +
                            "Is Euler: \t" + g.IsEuler().ToString() + "\n";
            listBox2.Items.Clear();
            foreach(Edge i in g.Edges())
            {
                listBox2.Items.Add(i);
            }
            listBox1.Items.Clear();
            foreach (Vertex i in g.BFS())
            {
                listBox1.Items.Add(i.Info);
            }
        }

        private void Load_Click(object sender, EventArgs e)
        {
            GraphController.Open(ref g);
            g.OnChange += Update;
            Update(g);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            GraphController.Save(ref g);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            g.AddVertex(textBox4.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            g.RemoveVertex(Convert.ToInt32(textBox1.Text));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            g.AddEdge(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            g.RemoveEdge(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            foreach(Vertex  i in g.DFS())
            {
                listBox3.Items.Add(i.Info);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            foreach (Vertex i in g.BFS())
            {
                listBox3.Items.Add(i.Info);
            }
        }



    }
}

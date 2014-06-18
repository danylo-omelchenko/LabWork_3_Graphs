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
            g.OnChange += new Graph.EventChange(Update);
        }

        public void Update(Graph sender)
        {
            label1.Text =
                           // "Is tree: \t" + g.IsTree().ToString() + "\n" +
                            "Is concted: \t" + g.IsConnected().ToString() + "\n";
        }

        private void Load_Click(object sender, EventArgs e)
        {
            GraphController.Open(ref g);
            Update(g);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            GraphController.Save(ref g);
        }



    }
}

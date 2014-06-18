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
    public partial class GraphForm : Form
    {
        public GraphForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VertexView v = new VertexView();
            v.Vertex = new Vertex("10");
            v.Location = new Point(100, 100);
            v.MouseDown += On_MouseDown;
            v.MouseUp += On_MouseUp;
            v.MouseLeave += On_MouseLeave;
            v.MouseEnter += On_MouseEnter;
            canvasView1.Views.Add(v);
            canvasView1.Refresh();
        }

        private void On_MouseDown(Views sender)
        {
            (sender as VertexView).BackColor = Color.Red;
        }

        private void On_MouseUp(Views sender)
        {
            (sender as VertexView).BackColor = Color.GreenYellow;
        }

        private void On_MouseEnter(Views sender)
        {
            (sender as VertexView).BackColor = Color.Pink;
        }

        private void On_MouseLeave(Views sender)
        {
            (sender as VertexView).BackColor = Color.GreenYellow;
        }

        List<VertexView> Vertexes = new List<VertexView>();
    }
}

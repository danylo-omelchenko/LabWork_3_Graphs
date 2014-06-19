﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using GraphImplementation;

namespace GraphView
{
    public delegate void EventSelectedChange(VertexView v);
  
    public partial class GraphForm : Form
    {
        Graph graph = new Graph();
        public event EventSelectedChange OnSelectedChange;
        VertexView Selected = null;
        bool isFindWay = false;
        public GraphForm()
        {
            InitializeComponent();

            graph.OnEdgeAdded += On_EdgeAdded;
            graph.OnEdgeRemoved += On_EdgeRemoved;
            graph.OnVertexRemoved += On_VertexRemoved;
            graph.OnVertexAdded += On_VertexAdded;
            graph.OnChange += On_Change;
            this.OnSelectedChange += GraphForm_OnSelectedChange;
            canvasView1.MouseMove += On_MouseMove;

            propertyGrid1.SelectedObject = graph;
        }

        void GraphForm_OnSelectedChange(VertexView v)
        {
            Selected = v;
            propertyGrid2.SelectedObject = v.Vertex;
        }

        public void On_Change(Graph sender)
        {
            propertyGrid1.Refresh();
        }

        public void On_VertexRemoved(Graph sender, Vertex vertex)
        {
            VertexView v = canvasView1.FindViewByVertex(vertex);
            if (v != null){ 
                v.MouseDown -= On_MouseDown;
                v.MouseUp -= On_MouseUp;
                v.MouseLeave -= On_MouseLeave;
                v.MouseEnter -= On_MouseEnter;
                canvasView1.Views.Remove(v);
            }
            v = null;
            canvasView1.Refresh();
        }
        public void On_VertexAdded(Graph sender, Vertex vertex)
        {
            canvasView1.Views.Add(VertexAdd(vertex));
            canvasView1.Refresh();
        }
        private VertexView VertexAdd(Vertex vertex)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            VertexView v = new VertexView();
            v.Vertex = vertex;
            v.Location = new Point(r.Next(v.Radius, canvasView1.Width - v.Radius), r.Next(v.Radius, canvasView1.Height - v.Radius));
            v.MouseDown += On_MouseDown;
            v.MouseUp += On_MouseUp;
            v.MouseLeave += On_MouseLeave;
            v.MouseEnter += On_MouseEnter; 
            return v;
        }
        private VertexView VertexAdd(Vertex vertex, Point location)
        {
            VertexView v = new VertexView();
            v.Vertex = vertex;
            v.Location = location;
            v.MouseDown += On_MouseDown;
            v.MouseUp += On_MouseUp;
            v.MouseLeave += On_MouseLeave;
            v.MouseEnter += On_MouseEnter;
            return v;
        }
        public void On_EdgeRemoved(Graph sender, Edge edge)
        {
            EdgeView e = canvasView1.FindViewByEdge(edge);
            if (e != null) canvasView1.Views.Remove(e);
            canvasView1.Refresh();
        }

        public void On_EdgeAdded(Graph sender, Edge edge)
        {
            EdgeView e = new EdgeView();
            e.Edge = edge;
            e.Point1 = canvasView1.FindViewByVertex(edge.Vertex1).Location;
            e.Point2 = canvasView1.FindViewByVertex(edge.Vertex2).Location;
            canvasView1.Views.Add(e);
            canvasView1.Refresh();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            graph.AddVertex();
        }

        VertexView moving = null;
        VertexView VPreview = new VertexView();
        VertexView Over = null;
        EdgeView EPreview = new EdgeView();
        Point StartPoint;
        Point OldPoint;

        Boolean isAdding = false;
        private void On_MouseDown(Views sender)
        {
            
            if(MouseButtons == MouseButtons.Left)
            {
                if (!isFindWay)
                {
                    foreach (VertexView v in canvasView1.Views.OfType<VertexView>())
                    {
                        v.IsSelected = false;
                    }
                    (sender as VertexView).IsSelected = true;
                    if (OnSelectedChange != null) OnSelectedChange(sender as VertexView);
                    canvasView1.BringToFront(sender);
                    moving = (VertexView)sender;
                }
                else
                {
                    clearHighLight();
                    Vertex last = null;
                    List<Vertex> way = new List<Vertex>();
                    try
                    {
                        way = GraphController.FindWay(Selected.Vertex, (sender as VertexView).Vertex);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    foreach(Vertex v in way)
                    {
                        canvasView1.FindViewByVertex(v).isHighLighted = true;
                        if(last!=null)
                        {
                            canvasView1.FindViewByEdge(new Edge(last, v)).IsHighLighted = true;
                        }
                        last = v;
                    }
                    isFindWay = false;
                    button4.Enabled = true ;
                    button4.BackgroundImage = global::GraphView.Properties.Resources.WAY;
                }
            }
            else
            {
                VPreview.BackColor = Color.GreenYellow;// Color.FromArgb(100, 0, 255, 0);
                VPreview.BorderColor = Color.Green;
                VPreview.Location = (sender as VertexView).Location;
                VPreview.Vertex = new Vertex("");
                canvasView1.Views.Add(VPreview);

                EPreview.Edge = new Edge((sender as VertexView).Vertex, VPreview.Vertex);
                EPreview.Point1 = canvasView1.FindViewByVertex(EPreview.Edge.Vertex1).Location;
                EPreview.Point2 = canvasView1.FindViewByVertex(EPreview.Edge.Vertex2).Location;
                canvasView1.Views.Add(EPreview);
                canvasView1.BringToFront(VPreview);
                isAdding = true;
            }
            OldPoint = (sender as VertexView).Location;
            StartPoint = MousePosition;
            
           


            //(sender as VertexView).BackColor = Color.Red;
        }


        private void On_MouseMove(Object sender, MouseEventArgs e)
        {
            if (moving != null)
            {
                moving.Location = new Point(OldPoint.X + (MousePosition.X - StartPoint.X), OldPoint.Y + (MousePosition.Y - StartPoint.Y));
                foreach (Edge ed in moving.Vertex.IncidentedEdges())
                {
                    if (canvasView1.FindViewByEdge(ed).Edge.Vertex1 == moving.Vertex)
                    {
                        canvasView1.FindViewByEdge(ed).Point1 = moving.Location;
                    }
                    else
                    {
                        canvasView1.FindViewByEdge(ed).Point2 = moving.Location;
                    }

                }
            }
            if (isAdding)
            {
                VertexView hitTest = canvasView1.VertexHitTest(new Point(e.X, e.Y), VPreview, canvasView1.FindViewByVertex(EPreview.Edge.Vertex1));
                if (hitTest != null)
                {
                    VPreview.Location = hitTest.Location;
                    Over = hitTest;
                }
                else
                {
                    VPreview.Location = new Point(OldPoint.X + (MousePosition.X - StartPoint.X), OldPoint.Y + (MousePosition.Y - StartPoint.Y));
                    Over = null;
                }
                EPreview.Point2 = canvasView1.FindViewByVertex(EPreview.Edge.Vertex2).Location;
            }
        }

        private void On_MouseUp(Views sender)
        {
           if (moving != null)
               moving = null;
           if(isAdding)
           {
               isAdding = false;

               if (Over != null)
               {
                   graph.AddEdge(Over.Vertex, EPreview.Edge.Vertex1);
               }
               else
               {
                       graph.AddVertex();
                       (canvasView1.Views[canvasView1.Views.Count - 1] as VertexView).Location = VPreview.Location;
                       graph.AddEdge(EPreview.Edge.Vertex1, (canvasView1.Views[canvasView1.Views.Count - 1] as VertexView).Vertex);
               }
               canvasView1.Views.Remove(VPreview);
               canvasView1.Views.Remove(EPreview);
           }
           canvasView1.Refresh();
           // (sender as VertexView).BackColor = Color.GreenYellow;
        }
        
        private void On_MouseEnter(Views sender)
        {
           (sender as VertexView).BackColor = Color.Yellow;
        }

        private void On_MouseLeave(Views sender)
        {
           (sender as VertexView).BackColor = Color.GreenYellow;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            canvasView1.Views.Clear();

            GraphController.Open(ref graph);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Vertex> ToRemove = new List<Vertex>();
            foreach (VertexView v in canvasView1.Views.OfType<VertexView>())
            {
                if (v.IsSelected)
                {
                    ToRemove.Add(v.Vertex);
                }
            }
            foreach (Vertex v in ToRemove)
            {
                graph.RemoveVertex(v);
            }
        }

        private void canvasView1_Click(object sender, EventArgs e)
        {

        }

        private void GraphForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(Selected!=null)
            {
                isFindWay = true;
                button4.Enabled = false;
                button4.BackgroundImage = global::GraphView.Properties.Resources.WAY_DISENABLED;
            }
            else
            {
                MessageBox.Show("Вершина не выделена");
            }
        }

        private void propertyGrid2_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            canvasView1.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(BFSview);
            
            t.Start();
        }
        private void BFSview()
        {
            clearHighLight();
            foreach(Vertex v in graph.BFS())
            {
                canvasView1.FindViewByVertex(v).isHighLighted = true;
                canvasView1.Refresh();
                System.Threading.Thread.Sleep(350);
            }
            
        }
        public void clearHighLight()
        {
            foreach (VertexView v in canvasView1.Views.OfType<VertexView>())
            {
                v.isHighLighted = false;
            }
            foreach (EdgeView v in canvasView1.Views.OfType<EdgeView>())
            {
                v.IsHighLighted = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(DFSview);

            t.Start();
        }
        private void DFSview()
        {
            clearHighLight();
            foreach (Vertex v in graph.DFS())
            {
                canvasView1.FindViewByVertex(v).isHighLighted = true;
                canvasView1.Refresh();
                System.Threading.Thread.Sleep(350);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            clearHighLight();
        }

        private void canvasView1_MouseUp(object sender, MouseEventArgs e)
        {

            if (canvasView1.VertexHitTest(new Point(e.X,e.Y)) == null && e.Button == MouseButtons.Right)
            {
                graph.AddVertex();
                (canvasView1.Views[canvasView1.Views.Count - 1] as VertexView).Location = new Point(e.X, e.Y);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            GraphController.Save(ref graph);
        }
    }
}

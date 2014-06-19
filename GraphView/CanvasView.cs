using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GraphImplementation;

namespace GraphView
{
    public class CanvasView:PictureBox
    {
        public CanvasView()
        {
            this.Resize += On_Resaize;
            this.Paint += On_Paint;
            this.MouseDown += On_Mouse_Down;
            this.MouseUp += On_Mouse_Up;
            this.MouseMove += On_Mouse_Move;
        }

        public List<Views> Views = new List<Views>();
        private Bitmap canvas = new Bitmap(1, 1);

        public void BringToFront(Views v)
        {
            for (int i = Views.Count - 1; i > 0; i--)
            {
                if (Views[i] == v)
                {
                    Views tmp = Views[i];
                    Views[i] = Views[i - 1];
                    Views[i - 1] = tmp;
                }
            }
            Refresh();
        }

        public VertexView FindViewByVertex(Vertex vertex)
        {
            foreach (VertexView v in Views.OfType<VertexView>())
            {
                if (v.Vertex == vertex) return v;
            }
            return null;
        }
        public EdgeView FindViewByEdge(Edge edge)
        {
            foreach (EdgeView v in Views.OfType<EdgeView>())
            {
                if (v.Edge == edge) return v;
            }
            return null;
        }

        public override void Refresh()
        {
            Graphics g = Graphics.FromImage(canvas);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(backColor);

            foreach (Views v in Views.Reverse <Views>())
            {
                v.Draw(g);
            }
            this.Image = canvas;
        }

        private void On_Paint(Object sender, PaintEventArgs e)
        {

        }

        private void On_Resaize(Object sender, EventArgs e)
        {
            canvas = new Bitmap(this.Width, this.Height);
        }

        private Color backColor = Color.White;
        public override Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        private void On_Mouse_Move(Object sender, MouseEventArgs e)
        {
            VertexView on_over = VertexHitTest(new Point(e.X, e.Y));
            foreach (VertexView v in Views.OfType<VertexView>())
            {
                if (on_over != null && v == on_over)
                {
                    on_over.Raise_MouseEnter();
                    on_over.Raise_MouseMove();
                }
                else
                {
                    v.Raise_MouseLeave();
                }
            }
            Refresh();
        }

        private void On_Mouse_Up(Object sender, MouseEventArgs e)
        {
            try
                {
                foreach (VertexView v in Views.OfType<VertexView>())
                {
                        v.Raise_MouseUp();
                }
            Refresh();
            }catch(Exception)
            {

            }
           
        }

        private void On_Mouse_Down(Object sender, MouseEventArgs e)
        {
            VertexView on_down = VertexHitTest(new Point(e.X,e.Y));
            if (on_down != null)
            {
                on_down.Raise_MouseDown();
            }
            Refresh();
        }

        public VertexView VertexHitTest(Point point,params VertexView[] ignore)
        {
            VertexView vv = null;
            foreach (VertexView v in Views.OfType<VertexView>())
            {
                bool flag = true;
                foreach (VertexView iv in ignore)
                {
                    if (iv == v)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag & Math.Sqrt((point.X - v.Location.X) * (point.X - v.Location.X) + (point.Y - v.Location.Y) * (point.Y - v.Location.Y)) <= v.Radius)
                {
                    vv = v; break;
                }

            }
            return vv;
        }
    }
}

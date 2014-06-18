using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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

        public override void Refresh()
        {
            Graphics g = Graphics.FromImage(canvas);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(backColor);

            foreach (Views v in Views)
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
            foreach (VertexView v in Views.OfType<VertexView>())
            {
                    v.Raise_MouseUp();
            }
            Refresh();
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

        private VertexView VertexHitTest(Point point)
        {
            VertexView vv = null;
            foreach (VertexView v in Views.OfType<VertexView>())
            {
                if (Math.Sqrt((point.X - v.Location.X) * (point.X - v.Location.X) + (point.Y - v.Location.Y) * (point.Y - v.Location.Y)) <= v.Radius)
                {
                    vv = v; break;
                }
            }
            return vv;
        }
    }
}

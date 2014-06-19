using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using GraphImplementation;
using System.Windows.Forms;


namespace GraphView
{
    public class VertexView:Views
    {
        public event MouseHandler MouseDown;
        public event MouseHandler MouseUp;
        public event MouseHandler MouseEnter;
        public event MouseHandler MouseLeave;
        public event MouseHandler MouseMove;

        public void Raise_MouseDown()
        {
            if (MouseDown != null)
            {
                MouseDown(this);
                isDown = true;
            }
        }

        public Boolean IsSelected = false;

        public void Raise_MouseUp()
        {
            if (MouseUp != null && isDown)
            {
                MouseUp(this);
                isDown = false;
            }
        }

        public void Raise_MouseEnter()
        {
            if (MouseEnter != null && !isOver) 
            { 
                MouseEnter(this);
                isOver = true;
            }
        }

        public void Raise_MouseLeave()
        {
            if (MouseLeave != null && isOver) 
            { 
                MouseLeave(this);
                isOver = false;
            }
        }

        public void Raise_MouseMove()
        {
            if (MouseMove != null && isOver)
            {
                MouseMove(this);
            }
        }

        private Boolean isDown = false;
        private Boolean isOver = false;

        void Views.Draw(Graphics g)
        {
            if(IsSelected)
            {
                g.FillEllipse(new SolidBrush(selectedColor), location.X - radius, location.Y - radius, radius * 2, radius * 2);
                g.DrawEllipse(new Pen(borderColor, 4), location.X - radius, location.Y - radius, radius * 2, radius * 2);
            }
            else
            {
                g.FillEllipse(new SolidBrush(backColor), location.X - radius, location.Y - radius, radius * 2, radius * 2);
                g.DrawEllipse(new Pen(borderColor, 2), location.X - radius, location.Y - radius, radius * 2, radius * 2);
            }
         
            TextRenderer.DrawText(g, vertex.Info, new Font("Times New Roman",10), new Rectangle(location.X - radius, location.Y - radius, radius * 2, radius * 2), Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.WordEllipsis);
        }

        private Vertex vertex;
        public Vertex Vertex
        {
            get { return vertex; }
            set { vertex = value; }
        }

        private Point location = new Point(0,0);
        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        private Int32 radius = 20;
        public Int32 Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        private Color backColor = Color.GreenYellow;
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        private Color selectedColor = Color.Yellow;
        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }

        private Color borderColor = Color.Green;
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

    }
}

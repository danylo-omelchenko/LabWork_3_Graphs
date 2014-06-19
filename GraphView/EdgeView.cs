using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GraphImplementation;

namespace GraphView
{
    public class EdgeView:Views
    {
        void Views.Draw(System.Drawing.Graphics g)
        {
            g.DrawLine(new Pen(color, width), point1, point2);
        }
        private bool isHighLighted;
        public bool IsHighLighted
        {
            get { return isHighLighted; }
            set 
            { 
                isHighLighted = value;
                if(isHighLighted)
                {
                    color = Color.Red;
                   
                }
                else
                {
                    color = Color.Green;
                }
            
            }
        }

        private Point point1 = new Point(0, 0),point2 = new Point(0, 0);
        public Point Point1
        {
            get { return point1; }
            set { point1 = value; }
        }

        public Point Point2
        {
            get { return point2; }
            set { point2 = value; }
        }

        private Int32 width = 2;
        public Int32 Width
        {
            get { return width; }
            set { width = value; }
        }

        private Color color = Color.Green;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }


        public Edge Edge;


    }
}

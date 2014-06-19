using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GraphView
{
    public delegate void MouseHandler(Views sender);
    
    public interface Views
    {
        
        void Draw(Graphics g);
    }
}

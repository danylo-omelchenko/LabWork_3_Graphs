using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphImplementation
{
    public class Edge
    {
        /// <summary>
        /// Страндартный конструктор
        /// </summary>
        public Edge()
        {

        }
        /// <summary>
        /// Создает экземпляр ребра графа между двумя вершинами
        /// </summary>
        /// <parparam name="Vertex1">Первая вершина</parparam>
        /// <param name="Vertex2">Вторая вершина</param>
        public Edge(Vertex Vertex1, Vertex Vertex2)
        {
            this.Vertex1 = Vertex1;
            this.Vertex2 = Vertex2;
        }
        public Vertex Vertex1;
        public Vertex Vertex2;
        public override string ToString()
        {
            return Vertex1.Info + " - " + Vertex2.Info;
        }

        public static bool operator ==(Edge e1, Edge e2)
        {
            if ((e1.Vertex1 == e2.Vertex1 && e1.Vertex2 == e2.Vertex2) || (e1.Vertex1 == e2.Vertex2 && e1.Vertex2 == e2.Vertex1))
                return true;
            else
                return false;
        }

        public static bool operator !=(Edge e1, Edge e2)
        {
            if ((e1.Vertex1 == e2.Vertex1 && e1.Vertex2 == e2.Vertex2) || (e1.Vertex1 == e2.Vertex2 && e1.Vertex2 == e2.Vertex1))
                return false;
            else
                return true;
        }
    }
}

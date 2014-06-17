using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    public class Graph
    {
        private List<Vertex> Vertexes = new List<Vertex>();
        public void AddVertex(Vertex NewVertex)
        {
            if(!Vertexes.Contains(NewVertex)) Vertexes.Add(NewVertex);
        }
        public void AddNewEdge(Vertex Vertex1, Vertex Vertex2)
        {
            if (!Vertex1.Vertexes.Contains(Vertex2)) Vertex1.Vertexes.Add(Vertex2);
            if (!Vertex2.Vertexes.Contains(Vertex1)) Vertex2.Vertexes.Add(Vertex1);
        }
        public Boolean IsEmpty()
        {
            return Vertexes.Count == 0;
        }

        public Boolean IsTree()
        {
            throw new NotImplementedException();
        }

        public Boolean IsСonnected()
        {
            throw new NotImplementedException();
        }

        public Boolean IsEuler()
        {
            throw new NotImplementedException();
        }

        public Boolean IsHamelton()
        {
            throw new NotImplementedException();
        }

        public Boolean IsWood()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vertex> DFS()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vertex> BFS()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vertex> Incidented(Vertex CurrentVertex)
        {
            foreach(Vertex ver in CurrentVertex.Vertexes)
            {
                yield return ver;
            }
        }


    }
}

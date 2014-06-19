using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphImplementation
{
    public class Vertex
    {
        public Vertex()
        {
            
        }
        public Vertex(String Info)
        {
            this.Info = Info;
        }
        public List<Vertex> Vertexes = new List<Vertex>();
        public String Info;

        //incident
        //isIsolated
        //degree

        public Int32 Degree
        {
            get { return Vertexes.Count; }
        }

        public Boolean IsIsolated()
        {
            return Vertexes.Count == 0;
        }

        public IEnumerable<Vertex> Incidented()
        {
            foreach (Vertex ver in this.Vertexes)
            {
                yield return ver;
            }
        }

        public IEnumerable<Edge> IncidentedEdges()
        {
            foreach (Vertex ver in this.Vertexes)
            {
                yield return new Edge(ver,this);
            }
        }
        
    }
}

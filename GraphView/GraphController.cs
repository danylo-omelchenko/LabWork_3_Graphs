using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graph;

namespace GraphView
{
    public static class GraphController
    {
        public static Graph.Graph LoadFromFile()
        {
            throw new NotImplementedException();
        }

        public static void SaveToFile(Graph.Graph graph)
        {
            String File = "";
            foreach(Vertex ver in graph.GetEnumerator())
            {
                File += ver.Info + (char)13;
            }
            Int32[,] Matr = graph.GetMutrix();
            for(int i = 0; i < Matr.GetLength(0);i++)
            {

            }
        }
    }
}

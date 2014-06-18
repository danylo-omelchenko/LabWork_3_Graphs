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

            Int32[,] Matr = graph.GetMatrix();
            File += Matr.GetLength(0).ToString() + (char)13;


            foreach (Vertex ver in graph.Simple())
            {
                File += ver.Info + (char)13;
            }

            for(int i = 0; i < Matr.GetLength(0);i++)
            {

            }
        }
    }
}

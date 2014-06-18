using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphImplementation;

namespace GraphView
{
    public static class GraphController
    {
        public static Graph LoadFromFile()
        {
            Graph g = new Graph();

            //throw new NotImplementedException();
            return g;
        }

        public static void SaveToFile(Graph graph)
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
                for (int j = 0; j < Matr.GetLength(1); j++)
                {
                    File += Matr[i,j].ToString() + (char)13;
                }
            }
        }
    }
}

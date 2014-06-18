using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphImplementation;
using System.IO;
using System.Windows.Forms;

namespace GraphView
{
    public static class GraphController
    {
        public static Graph LoadFromFile(String FileName)
        {
            Graph g = new Graph();

            String[] lines = File.ReadAllLines(FileName);
            
            Int32 cursor = 0;
            Int32[,] Matr = new Int32[Convert.ToInt32(lines[cursor]), Convert.ToInt32(lines[cursor])];
            cursor++;
            cursor += Matr.GetLength(0);
            for (int i = 0; i < Matr.GetLength(0); i++)
            {
                for (int j = 0; j < Matr.GetLength(1); j++)
                {
                    Matr[i, j] = Convert.ToInt32(lines[cursor]);
                    cursor++;
                }
            }
            cursor = 1;
            g.GraphFromMatrix(Matr);
            foreach(Vertex v in g.Simple())
            {
                v.Info = lines[cursor];
                cursor++;
            }

            return g;

        }

        public static void SaveToFile(Graph graph, String FileName)
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

        public static void Save(ref Graph g)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Сохранить граф.";
            sfd.Filter = "Файл графа (*.gr)|*.gr";
            DialogResult dr = sfd.ShowDialog();
            if (dr != DialogResult.Cancel)
            {
                SaveToFile(g, sfd.FileName);
            }
        }

        public static void Open(ref Graph g)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Открыть граф.";
            ofd.Filter = "Файл графа (*.gr)|*.gr";
            DialogResult dr = ofd.ShowDialog();
            if (dr != DialogResult.Cancel)
            {
                g.Clear();
                g = LoadFromFile(ofd.FileName);
            }
        }

    }
}

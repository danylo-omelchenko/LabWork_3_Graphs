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
        /// <summary>
        /// Создает новый граф из файла.
        /// </summary>
        /// <param name="FileName">Путь к файлу</param>
        /// <returns>Возвращает граф, загруженный из файла</returns>
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
        /// <summary>
        /// Сохраняет граф в файл
        /// </summary>
        /// <param name="Graph">Граф для сохранения</param>
        /// <param name="FileName">Путь к файлу</param>
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

        public static List<Vertex> FindWay(Graph g,Vertex from,Vertex to)
        {
            int steps = 0;
            List<Vertex> list = new List<Vertex>();
            Dictionary<Vertex, int> Marks = new Dictionary<Vertex, int>();
            Queue<Vertex> queue = new Queue<Vertex>();
            queue.Enqueue(from);
            Marks.Add(from,0);
            while (queue.Count != 0)
            {
                foreach (Vertex i in queue.Peek().Incidented())
                {
                    if (!Marks.ContainsKey(i))
                    {
                        Marks.Add(i, Marks[queue.Peek()]+1);
                        queue.Enqueue(i);
                    }
                    if(i==to)
                    {
                        steps = Marks[i];
                        break;
                    }
                }
                queue.Dequeue();
            }
            queue.Clear();
            list.Add(to);
            Vertex v=to;
            while(v!=from)
            {
                foreach (Vertex i in v.Incidented())
                {
                    if(Marks.ContainsKey(i))
                    {
                        if(Marks[i]==Marks[v]-1)
                        {
                            v = i;
                            list.Add(v);
                            break;
                        }
                    }
                }
            }
            list.Reverse();
            return list ;
        }
        public static List<Vertex> FindWay(Graph g, int from, int to)
        {
            return FindWay(g,g.GetVertextByIndex(from),g.GetVertextByIndex(to));
        }
    }
}

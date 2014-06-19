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
        public static void LoadFromFile(ref Graph g, String FileName)
        {
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

        }
        /// <summary>
        /// Сохраняет граф в файл
        /// </summary>
        /// <param name="graph">Граф для сохранения</param>
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
                LoadFromFile(ref g, ofd.FileName);
            }
        }

        /// <summary>
        /// Ищет путь в графе между двумя вершинами
        /// </summary>
        /// <param name="from">Точка старта</param>
        /// <param name="to">Точка финиша</param>
        /// <returns>Возвращает список вершин на пути между двумя вершинами</returns>
        public static List<Vertex> FindWay(Vertex from, Vertex to)
        {
            int steps = 0;
            List<Vertex> list = new List<Vertex>();
            Dictionary<Vertex, int> Marks = new Dictionary<Vertex, int>();
            Queue<Vertex> queue = new Queue<Vertex>();
            queue.Enqueue(from);
            Marks.Add(from,0);
            bool flag = false;
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
                        flag = true;
                        break;
                    }
                }
                queue.Dequeue();
            }
            if (!flag) throw new Exception("Путь не найден");
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
        /// <summary>
        /// Ищет путь в графе между двумя вершинами
        /// </summary>
        /// <param name="graph">Граф, для поиска</param>
        /// <param name="from">Индекс точки старта</param>
        /// <param name="to">Индекс точки финиша</param>
        /// <returns>Возвращает список вершин на пути между двумя вершинами заданными индексами</returns>
        public static List<Vertex> FindWay(Graph graph, int from, int to)
        {
            try
            {
                return FindWay(graph.GetVertextByIndex(from), graph.GetVertextByIndex(to));
            }
            catch
            {
                throw new Exception("Неправильный индекс вершины");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    public class Graph
    {
        private List<Vertex> Vertexes = new List<Vertex>();

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public Graph()
        {
  
        }

        /// <summary>
        /// Создает новый граф на основе матрици смежности Matrix.
        /// </summary>
        /// <param name="Matrix">Матрица смежности.</param>
        public void GraphFromMatrix(Int32[,] Matrix)
        {
            Clear();
            for(int i = 0;i < Matrix.GetLength(0);i++)
            {
                for (int j = i; j < Matrix.GetLength(1);j++)
                {
                    if (Matrix[i,j]==1)
                    {
                        AddEdge(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает новый граф на основе матрици смежности Matrix.
        /// </summary>
        /// <param name="Matrix">Матрица смежности.</param>
        public static Graph GraphFromMatrix(Int32[,] Matrix)
        {
            Graph newGraph = new Graph();
            newGraph.Clear();
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = i; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] == 1)
                    {
                        newGraph.AddEdge(i, j);
                    }
                }
            }
            return newGraph;
        }

        /// <summary>
        /// Возвращает матрицу смежности графа.
        /// </summary>
        /// <returns>Матрица смежности Int32[,]</returns>
        public Int32[,] GetMutrix()
        {
            Int32[,] Matrix = new Int32[Vertexes.Count,Vertexes.Count];
            Matrix.Initialize();
            foreach(Vertex ver in Vertexes)
            {
                foreach (Vertex inc_ver in ver.Incidented())
                {
                    Matrix[Vertexes.IndexOf(ver), Vertexes.IndexOf(inc_ver)] = 1;
                    Matrix[Vertexes.IndexOf(inc_ver), Vertexes.IndexOf(ver)] = 1;
                }
            }
            return Matrix;
        }

        /// <summary>
        /// Очищает граф, оставляя его пустым.
        /// </summary>
        public void Clear()
        {
            Vertexes.Clear();
        }

        /// <summary>
        /// Добавляет новую вершину в граф.
        /// </summary>
        /// <param name="NewVertex">Вершина для добавления.</param>
        public void AddVertex(String Info)
        {
            if (!Vertexes.Contains(NewVertex)) Vertexes.Add(NewVertex);
        }

         

        /// <summary>
        /// Добавляет конкретную вершину в граф.
        /// </summary>
        /// <param name="NewVertex">Вершина для добавления.</param>
        public void AddVertex(Vertex NewVertex) 
        {
            if(!Vertexes.Contains(NewVertex)) Vertexes.Add(NewVertex);
        }

        /// <summary>
        /// Добавляет ребро в граф.
        /// </summary>
        /// <param name="Vertex1">Первая вершина, которая входит в добавляемое ребро.</param>
        /// <param name="Vertex2">Вторая вершина, которая входит в добавляемое ребро.</param>
        public void AddEdge(Vertex Vertex1, Vertex Vertex2)
        {
            if (!Vertex1.Vertexes.Contains(Vertex2)) Vertex1.Vertexes.Add(Vertex2);
            if (!Vertex2.Vertexes.Contains(Vertex1)) Vertex2.Vertexes.Add(Vertex1);
        }

        /// <summary>
        /// Добавляет ребро в граф, на основе индексов вершин.
        /// </summary>
        /// <param name="Index1">Индекс первой вершины, которая входит в добавляемое ребро.</param>
        /// <param name="Index2">Индекс второй вершины, которая входит в добавляемое ребро</param>
        public void AddEdge(Int32 Index1, Int32 Index2)
        {
            if (!Vertexes[Index1].Vertexes.Contains(Vertexes[Index2])) Vertexes[Index1].Vertexes.Add(Vertexes[Index2]);
            if (!Vertexes[Index2].Vertexes.Contains(Vertexes[Index1])) Vertexes[Index2].Vertexes.Add(Vertexes[Index1]);
        }
        /// <summary>
        /// Удаляет вершину из графа.
        /// </summary>
        /// <param name="DelVertex">Вершина, которую требуется удалить.</param>
        public void RemoveVertex(Vertex DelVertex)
        {
            foreach(Vertex Incident in DelVertex.Incidented())
            {
                Incident.Vertexes.Remove(DelVertex);
            }
            Vertexes.Remove(DelVertex);
        }
        /// <summary>
        /// Удаляет вершину из графа по указанному индексу
        /// </summary>
        /// <param name="DelVertex">Вершина, которую требуется удалить.</param>
        public void RemoveVertex(Int32 DelIndex)
        {
            foreach (Vertex Incident in Vertexes[DelIndex].Incidented())
            {
                Incident.Vertexes.Remove(Vertexes[DelIndex]);
            }
            Vertexes.Remove(Vertexes[DelIndex]);
        }
        public void RemoveEdge(Vertex Vertex1, Vertex Vertex2)
        {
            if (Vertex1.Vertexes.Contains(Vertex2)) Vertex1.Vertexes.Remove(Vertex2);
            if (Vertex2.Vertexes.Contains(Vertex1)) Vertex2.Vertexes.Remove(Vertex1);
        }
        public void RemoveEdge(Int32 Index1, Int32 Index2)
        {
            if (Vertexes[Index1].Vertexes.Contains(Vertexes[Index2])) Vertexes[Index1].Vertexes.Remove(Vertexes[Index2]);
            if (Vertexes[Index2].Vertexes.Contains(Vertexes[Index1])) Vertexes[Index2].Vertexes.Remove(Vertexes[Index1]);
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    public class Graph: IEnumerable
    {
        public delegate void EventChange(Graph sender);
        public event EventChange OnChange;

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
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                AddVertex();
            }
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
        /// Возвращает матрицу смежности графа.
        /// </summary>
        /// <returns>Матрица смежности Int32[,]</returns>
        public Int32[,] GetMatrix()
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

        #region "operations with vertexes"


            /// <summary>
            /// Добавляет новую вершину в граф (С данными по умолчанию).
            /// </summary>
            public void AddVertex()
            {
                Vertexes.Add(new Vertex(Vertexes.Count.ToString()));
                if (OnChange != null) OnChange(this);
            }


            /// <summary>
            /// Добавляет новую вершину в граф.
            /// </summary>
            /// <param name="Info">Данные для новой вершины.</param>
            public void AddVertex(String Info)
            {
                Vertexes.Add(new Vertex(Info));
                if (OnChange != null) OnChange(this);
            }


            /// <summary>
            /// Добавляет конкретную вершину в граф.
            /// </summary>
            /// <param name="NewVertex">Вершина для добавления.</param>
            public void AddVertex(Vertex NewVertex)
            {
                if (!Vertexes.Contains(NewVertex)) Vertexes.Add(NewVertex);
                if (OnChange != null) OnChange(this);
            }


            /// <summary>
            /// Удаляет вершину из графа.
            /// </summary>
            /// <param name="DelVertex">Вершина, которую требуется удалить.</param>
            public void RemoveVertex(Vertex DelVertex)
            {
                foreach (Vertex Incident in DelVertex.Incidented())
                {
                    Incident.Vertexes.Remove(DelVertex);
                }
                Vertexes.Remove(DelVertex);
                if (OnChange != null) OnChange(this);
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
                if (OnChange != null) OnChange(this);
            }
        #endregion

        #region "operation with edges"


            /// <summary>
            /// Добавляет ребро в граф.
            /// </summary>
            /// <param name="Vertex1">Первая вершина, которая входит в добавляемое ребро.</param>
            /// <param name="Vertex2">Вторая вершина, которая входит в добавляемое ребро.</param>
            public void AddEdge(Vertex Vertex1, Vertex Vertex2)
            {
                if (!Vertex1.Vertexes.Contains(Vertex2)) Vertex1.Vertexes.Add(Vertex2);
                if (!Vertex2.Vertexes.Contains(Vertex1)) Vertex2.Vertexes.Add(Vertex1);
                if (OnChange != null) OnChange(this);
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
                if (OnChange != null) OnChange(this);
            }


            /// <summary>
            /// Удаляет ребро соединяющие вершины Vertex1 и Vertex2, если таковые имеется.
            /// </summary>
            /// <param name="Vertex1">Первая вершина.</param>
            /// <param name="Vertex2">Вторая вершина.</param>
            public void RemoveEdge(Vertex Vertex1, Vertex Vertex2)
            {
                if (Vertex1.Vertexes.Contains(Vertex2)) Vertex1.Vertexes.Remove(Vertex2);
                if (Vertex2.Vertexes.Contains(Vertex1)) Vertex2.Vertexes.Remove(Vertex1);
                if (OnChange != null) OnChange(this);
            }


            /// <summary>
            /// Удаляет ребро соединяющие вершины с индексами Index1 и Index2, если таковые имеется.
            /// </summary>
            /// <param name="Vertex1">Индекс первой вершины.</param>
            /// <param name="Vertex2">Индекс второй вершины</param>
            public void RemoveEdge(Int32 Index1, Int32 Index2)
            {
                if (Vertexes[Index1].Vertexes.Contains(Vertexes[Index2])) Vertexes[Index1].Vertexes.Remove(Vertexes[Index2]);
                if (Vertexes[Index2].Vertexes.Contains(Vertexes[Index1])) Vertexes[Index2].Vertexes.Remove(Vertexes[Index1]);
                if (OnChange != null) OnChange(this);
            }
        #endregion

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
            int[,] matrix = this.GetMatrix();
            bool flag = true;
            bool inColumn;
            for(int i=0;i<matrix.GetLength(0); i++)
            {
                inColumn=false;
                for (int j = 0; j < matrix.GetLength(i); j++)
                {
                    if(matrix[i,j]!=0)
                    {
                        inColumn = true;
                        break;
                    }
                }
                if (!inColumn) flag = false;
                break;
            }
            return flag;
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

        #region "iterators"
            

            /// <summary>
            /// Обход графа в глубину.
            /// </summary>
            /// <returns>Возвращает вершины в порядке обхода.</returns>
            public IEnumerable<Vertex> DFS()
            {
                throw new NotImplementedException();
            }


            /// <summary>
            /// Обход графа в ширину.
            /// </summary>
            /// <returns>Возвращает вершины в порядке обхода.</returns>
            public IEnumerable<Vertex> BFS()
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Перечисляет все вершины инцидентные данной.
            /// </summary>
            /// <param name="CurrentVertex">Вершина.</param>
            /// <returns>Возвращает все инцидентные вершины в произвольном порядке.</returns>
            public IEnumerable<Vertex> Incidented(Vertex CurrentVertex)
            {
                foreach (Vertex ver in CurrentVertex.Vertexes)
                {
                    yield return ver;
                }
            }


            /// <summary>
            /// Перечисляет все вершины графа в произвольном порядке.
            /// </summary>
            /// <returns>Возвращает все вершины в произвольном порядке.</returns>
            public IEnumerable<Vertex> GetEnumerator()
            {
                foreach (Vertex ver in Vertexes)
                {
                    yield return ver;
                }
            }

        #endregion

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
    }
}

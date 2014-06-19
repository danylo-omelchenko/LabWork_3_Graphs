using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace GraphImplementation
{
    public class Vertex
    { 
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public Vertex()
        {
            
        } 
        /// <summary>
        /// Создает вершину, добавляя в нее информацию
        /// </summary>
        /// <param name="Info">Информация</param>
        public Vertex(String Info)
        {
            this.info = Info;
        }
        /// <summary>
        /// Вершины, с которыми имеет связь данный узел
        /// </summary>
        public List<Vertex> Vertexes = new List<Vertex>();
        private String info;

        /// <summary>
        /// Показывает информацию, которую хранит узел графа
        /// </summary>
        [Description("Информация, которую хранит узел графа")]
        public String Info
        {
            get { return info; }
            set { info = value; }
        }


        /// <summary>
        /// Показывает степень узла графа
        /// </summary>
        [Description("Показывает степень узла графа, то есть количество смежных вершин")]
        public Int32 Degree
        {
            get { return Vertexes.Count; }
        }
        /// <summary>
        /// Показывает изолирован ли узел графа
        /// </summary>
        [Description("Показывает изолирован ли узел графа")]
        public Boolean IsIsolated
        {
            get
            {
                return Vertexes.Count == 0;
            }
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

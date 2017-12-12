using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Graph
{
    public class GraphEdge
    {
        public GraphNode FromVertex { get;  set; }
        public GraphNode ToVertex { get;  set; }
        public int Weight { get;  set; }

        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="from">noeud départ</param>
        /// <param name="to">noeud arrivée</param>
        /// <param name="weight">poids entre les noeuds</param>
        public GraphEdge(GraphNode from, GraphNode to, int weight)
        {
            FromVertex = from;
            ToVertex = to;
            Weight = weight;
        }

        public GraphEdge()
        {
        }
    }
}
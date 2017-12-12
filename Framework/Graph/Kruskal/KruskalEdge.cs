using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Graph.Kruskal
{
    public class KruskalEdge : IComparable<KruskalEdge>
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }

        /// <summary>
        /// Comparaison entre les différents valeur lié à leur poids
        /// </summary>
        /// <param name="compareEdge"></param>
        /// <returns></returns>
        public int CompareTo(KruskalEdge compareEdge)
        {
            return this.Weight - compareEdge.Weight;
        }

        public KruskalEdge()
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ShortestPath
{
    public class UtilShortestPath
    {
        public List<Edge> Edges { get; set; }

        public List<Node> Nodes { get; set; }

        public List<Node> NodeTampon { get; set; }

        /// <summary>
        /// Position Noeud de départ
        /// </summary>
        public int PosNodeStart { get; set; }

        /// <summary>
        /// Initialisation Bellman-Ford
        /// </summary>
        public void Initialize()
        {
            foreach (Node n in Nodes)
            {
                n.Value = int.MaxValue;
                n.parent = null;
            }
            Nodes[PosNodeStart].Value = 0;
        }

        /// <summary>
        ///retourne le poid entre 2 noeuds
        /// </summary>
        /// <param name="n1">Noeud 1</param>
        /// <param name="n2">Noeud 2</param>
        /// <returns>poids entre les 2 noeuds</returns>
        public int GetWeight(Node n1, Node n2)
        {
            int value = -1;
            foreach (Edge edge in Edges)
            {
                if (edge.A == n1 && edge.B == n2)
                {
                    value = edge.Weight;
                }
            }
            return value;
        }

        /// <summary>
        /// Relâche Les noeuds ,si la valeur du Noeud 2 est supérieur à la valeur du Noeud 1 + (valeur noeud1 +noued2 )
        /// affecte cette valeur au noeud 2 et le noeud 1 devient son parent
        /// </summary>
        /// <param name="n1">Noeud 1</param>
        /// <param name="n2">Noeud 2</param>
        public void Relax(Node n1, Node n2)
        {
            if (n2.Value > (n1.Value + GetWeight(n1, n2)))
            {
                n2.Value = n1.Value + GetWeight(n1, n2);
                n2.parent = n1;
            }
        }
    }
}
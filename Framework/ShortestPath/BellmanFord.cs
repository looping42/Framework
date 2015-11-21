using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ShortestPath
{
    /// <summary>
    /// Algorithme de bellman ford
    /// Trouve le plus court chemin pour chaque noeud en partant du noeud de départ
    /// la liste des noeud contenant le plus court chemin se trouve dans la List<Node> nodes en scannant les noeuds parents
    /// </summary>
    public class BellmanFord
    {
        public List<Edge> edges { get; set; }

        public List<Node> nodes { get; set; }

        //Noeud de départ
        public int posNodeStart { get; set; }

        /// <summary>
        /// Initialisation Bellman-Ford
        /// </summary>
        public void initialize()
        {
            foreach (Node n in nodes)
            {
                n.Value = int.MaxValue;
                n.parent = null;
            }
            nodes[posNodeStart].Value = 0;
        }

        /// <summary>
        ///retourne le poid entre 2 noeuds
        /// </summary>
        /// <param name="n1">Noeud 1</param>
        /// <param name="n2">Noeud 2</param>
        /// <returns>poids entre les 2 noeuds</returns>
        public int getWeight(Node n1, Node n2)
        {
            int value = -1;
            foreach (Edge edge in edges)
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
        private void relax(Node n1, Node n2)
        {
            if (n2.Value > (n1.Value + getWeight(n1, n2)))
            {
                n2.Value = n1.Value + getWeight(n1, n2);
                n2.parent = n1;
            }
        }

        /// <summary>
        /// Vérifie si on est dans un cycle négatif(probléme dans ce cas)
        /// </summary>
        /// <returns>booléen</returns>
        public bool detectingNegativeCycles()
        {
            foreach (Edge edge in edges)
            {
                if (edge.B.Value > edge.A.Value + getWeight(edge.A, edge.B))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Algorithme de Bellman-Ford
        /// </summary>
        public void bellmanFord()
        {
            initialize();

            for (int i = 0; i <= nodes.Count - 2; i++)
            {
                foreach (Edge edge in edges)
                {
                    relax(edge.A, edge.B);
                }
            }
            if (detectingNegativeCycles())
            {
                Log.Logger.Info("Cycle Négatif : non");
            }
            else
            {
                throw new Exception("nou sommes dans un Cycle Négatif");
            }
        }
    }
}
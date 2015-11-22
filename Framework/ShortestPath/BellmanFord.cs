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
    public class BellmanFord : UtilShortestPath
    {
        /// <summary>
        /// Vérifie si on est dans un cycle négatif(probléme dans ce cas)
        /// </summary>
        /// <returns>booléen</returns>
        public bool DetectingNegativeCycles()
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
        public void BellmanFordWork()
        {
            initialize();

            for (int i = 0; i <= nodes.Count - 2; i++)
            {
                foreach (Edge edge in edges)
                {
                    relax(edge.A, edge.B);
                }
            }
            if (DetectingNegativeCycles())
            {
                Log.Logger.Info("Cycle Négatif : non");
            }
            else
            {
                throw new Exception("nous sommes dans un Cycle Négatif");
            }
        }
    }
}
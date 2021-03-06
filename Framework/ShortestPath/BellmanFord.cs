﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ShortestPath
{
    /// <summary>
    /// Algorithme de bellman ford
    /// Trouve le plus court chemin pour chaque noeud en partant du noeud de départ
    /// la liste des noeuds contenant le plus court chemin se trouve dans la List<Node> nodes en scannant les noeuds parents
    /// </summary>
    public class BellmanFord : UtilShortestPath
    {
        /// <summary>
        /// Vérifie si on est dans un cycle négatif(probléme dans ce cas)
        /// </summary>
        /// <returns>booléen</returns>
        public bool DetectingNegativeCycles()
        {
            foreach (Edge edge in Edges)
            {
                if (edge.B.Value > edge.A.Value + GetWeight(edge.A, edge.B))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Algorithme de Bellman-Ford
        /// </summary>
        /// <returns>btrue si valide , false si cycle négatif</returns>
        public bool BellmanFordWork()
        {
            Initialize();

            for (int i = 0; i <= Nodes.Count - 2; i++)
            {
                foreach (Edge edge in Edges)
                {
                    Relax(edge.A, edge.B);
                }
            }
            if (DetectingNegativeCycles())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
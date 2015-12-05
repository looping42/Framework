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

        public int MaxValue { get; set; }

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
                n.Value = MaxValue;
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

        /// <summary>
        /// Initialise un graphe avec une valeur lambda
        /// Les valeurs des liens pour les noeuds (0 à 0 ou 1 à 1 etc ...) sont mise à zero
        /// </summary>
        /// <returns></returns>
        public int[,] InitializeGraphMaxValue()
        {
            int[,] tampon = new int[Nodes.Count(), Nodes.Count()];

            for (int i = 0; i < tampon.GetLongLength(0); i++)
            {
                for (int j = 0; j < tampon.GetLongLength(1); j++)
                {
                    if (i == j)
                    {
                        tampon[i, j] = 0;
                    }
                    else
                    {
                        tampon[i, j] = MaxValue;
                    }
                }
            }
            return tampon;
        }

        /// <summary>
        /// Transforme un graphe en matrice
        /// Pour le poids des Edges allant seulement dans un sens
        /// </summary>
        /// <returns>matrice</returns>
        public int[,] GraphInMatrix()
        {
            int[,] tampon = InitializeGraphMaxValue();

            foreach (Edge edge in Edges)
            {
                tampon[edge.A.Value, edge.B.Value] = edge.Weight;
            }
            return tampon;
        }

        /// <summary>
        /// Transforme un graphe en matrice
        /// Pour le poids des Edges allant en double sens
        /// </summary>
        /// <returns>matrice</returns>
        public int[,] GraphInMatrixDoubleSens()
        {
            int[,] tampon = InitializeGraphMaxValue();

            foreach (Edge edge in Edges)
            {
                tampon[edge.A.Value, edge.B.Value] = edge.Weight;
                tampon[edge.B.Value, edge.A.Value] = edge.Weight;
            }
            return tampon;
        }
    }
}
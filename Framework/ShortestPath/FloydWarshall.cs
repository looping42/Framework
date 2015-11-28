using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ShortestPath
{
    /// <summary>
    /// Recherche du plus court chemin dans un tableau à 2 dimensions
    /// Stocke le résultat dans le PathTwoDimensionArray
    /// </summary>
    public class FloydWarshall
    {
        /// <summary>
        /// Nombre de noeuds max du tableau à 2 dimensions
        /// </summary>
        public int NumberOfNodes { get; set; }

        public int[,] TwoDimensionArray { get; set; }

        public int[,] PathTwoDimensionArray { get; set; }

        public int MaxValue { get; set; }

        /// <summary>
        /// Recherche du plus court chemin
        /// </summary>
        public void Floyd_warshallWork()
        {
            Populatearray();
            int i, j, k;
            for (k = 0; k < NumberOfNodes; k++)
            {
                //Passage sur tout les Noeuds un par un
                for (i = 0; i < NumberOfNodes; i++)
                {
                    for (j = 0; j < NumberOfNodes; j++)
                    {
                        if (TwoDimensionArray[i, j] > (TwoDimensionArray[i, k] + TwoDimensionArray[k, j]))
                        {
                            TwoDimensionArray[i, j] = TwoDimensionArray[i, k] + TwoDimensionArray[k, j];
                            PathTwoDimensionArray[i, j] = PathTwoDimensionArray[k, j];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Peuple le tableau de sortie qui va permettre  de construire le plus court chemin
        /// </summary>
        private void Populatearray()
        {
            PathTwoDimensionArray = new int[NumberOfNodes, NumberOfNodes];
            for (int i = 0; i < NumberOfNodes; i++)
            {
                for (int j = 0; j < NumberOfNodes; j++)
                {
                    if ((i == j) || (TwoDimensionArray[i, j] == MaxValue))
                    {
                        PathTwoDimensionArray[i, j] = -1;
                    }
                    else
                    {
                        PathTwoDimensionArray[i, j] = i;
                    }
                }
            }
        }

        /// <summary>
        /// Choix du plus court chemin entre 2 noeuds
        /// </summary>
        /// <param name="i">Noeud de départ</param>
        /// <param name="j">Noeud d'arrivée</param>
        /// <param name="List<int> rez">Passage par référence de la liste de résultat</param>
        public void GetPath(int i, int j, ref List<int> rez)
        {
            if (i == j)
            {
                rez.Add(i);
            }
            else if (PathTwoDimensionArray[i, j] == -1)
            {
                rez.Add(-1);
            }
            else
            {
                GetPath(i, PathTwoDimensionArray[i, j], ref rez);
                rez.Add(j);
            }
        }

        /// <summary>
        ///Chemin comprenant le noeud précedent
        /// </summary>
        private void PathPrecedent()
        {
            for (int i = 0; i < NumberOfNodes; ++i)
            {
                for (int j = 0; j < NumberOfNodes; ++j)
                {
                    Console.Error.Write(PathTwoDimensionArray[i, j] + "   ");
                }
                Console.Error.WriteLine();
            }
        }

        /// <summary>
        /// Affiche la distance entre chaque noeud
        /// Comptabilise les lien entre les noeuds
        /// </summary>
        private void DistBetweenNode()
        {
            for (int i = 0; i < NumberOfNodes; ++i)
            {
                for (int j = 0; j < NumberOfNodes; ++j)
                {
                    if (TwoDimensionArray[i, j] == MaxValue)
                        Console.Error.Write("INF ");
                    else
                        Console.Error.Write(TwoDimensionArray[i, j] + "   ");
                }
                Console.Error.WriteLine();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ShortestPath
{
    public class FloydWarshall
    {
        /// <summary>
        /// Nombre de noeuds max du tableau à 2 dimension
        /// </summary>
        public int NumberOfNodes { get; set; }

        public int[,] TwoDimensionArray { get; set; }

        public int[,] PathTwoDimensionArray { get; set; }

        public int MaxValue { get; set; }

        public void floyd_warshall()
        {
            Populatearray();
            int i, j, k;
            for (k = 0; k < NumberOfNodes; k++)
            {
                //Passage sur tout les Noeud un par un
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
            List<int> rez = new List<int>();
            getPath(1, 2, ref rez);

            //printSolutionV2(PathTwoDimensionArray);
            //printSolution(TwoDimensionArray);
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

        private StringBuilder path(int u, int v)
        {
            StringBuilder rez = new StringBuilder();
            if (PathTwoDimensionArray[u, v] == -1)
            {
                return rez.Append("Pas de path");
            }
            else
            {
                path(u, PathTwoDimensionArray[u, v]);
                path(PathTwoDimensionArray[u, v], u);
            }
            return rez;
        }

        /// <summary>
        /// Choix du plus court chemin entre 2 noeuds
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void getPath(int i, int j, ref List<int> rez)
        {
            if (i == j)
            {
                //Console.WriteLine(i + ";");
                rez.Add(i);
            }
            else if (PathTwoDimensionArray[i, j] == -1)
            {
                rez.Add(-1);
                Console.WriteLine("pas de path");
            }
            else
            {
                getPath(i, PathTwoDimensionArray[i, j], ref rez);
                //Console.WriteLine(j + ";");
                rez.Add(j);
            }
        }

        private void printSolutionV2(int[,] PathTwoDimensionArray)
        {
            Console.Error.WriteLine("La path pour chaque noeud ");
            for (int i = 0; i < NumberOfNodes; ++i)
            {
                for (int j = 0; j < NumberOfNodes; ++j)
                {
                    Console.Error.Write(PathTwoDimensionArray[i, j] + "   ");
                }
                Console.Error.WriteLine();
            }
        }

        private void printSolution(int[,] nodeTampon)
        {
            Console.WriteLine("La matrice montre le noeud et la distance entre chaque noeud ");
            for (int i = 0; i < NumberOfNodes; ++i)
            {
                for (int j = 0; j < NumberOfNodes; ++j)
                {
                    if (nodeTampon[i, j] == MaxValue)
                        Console.Write("INF ");
                    else
                        Console.Write(nodeTampon[i, j] + "   ");
                }
                Console.WriteLine();
            }
        }
    }
}
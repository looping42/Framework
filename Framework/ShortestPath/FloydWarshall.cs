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
        /// Nombre de noeuds(Sur une ligne)
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
                // Pick all vertices as source one by one
                for (i = 0; i < NumberOfNodes; i++)
                {
                    for (j = 0; j < NumberOfNodes; j++)
                    {
                        if (TwoDimensionArray[i, j] > (TwoDimensionArray[i, k] + TwoDimensionArray[k, j]))
                        {
                            TwoDimensionArray[i, j] = TwoDimensionArray[i, k] + TwoDimensionArray[k, j];
                            PathTwoDimensionArray[i, j] = PathTwoDimensionArray[k, j];

                            //Console.Error.Write("shortest path i to j : " + PathTwoDimensionArray[i, j]);
                        }
                    }
                    //Console.Error.WriteLine();
                }
            }

            //get();
            //Path(i, j);
            printSolutionV2(PathTwoDimensionArray);
            //printSolution(TwoDimensionArray);
        }

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

        private void get()
        {
            for (int i = 0; i < NumberOfNodes; i++)
            {
                for (int j = 0; j < NumberOfNodes; j++)
                {
                    getPath(i, j);
                }
            }
        }

        private void getPath(int i, int j)
        {
            if (i == j)
            {
                Console.WriteLine(i);
            }
            else if (PathTwoDimensionArray[i, j] == 0)
            {
                Console.WriteLine("pas de path");
            }
            else
            {
                getPath(i, PathTwoDimensionArray[i, j]);
                Console.WriteLine(j);
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
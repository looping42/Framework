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
        /// Nombre de noeuds(sur une ligne)
        /// </summary>
        public int NumberOfNodes { get; set; }

        public int[,] TwoDimensionArray { get; set; }

        public int MaxValue { get; set; }

        public void floyd_warshall()
        {
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
                        }
                    }
                }
            }
            printSolution(TwoDimensionArray);
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
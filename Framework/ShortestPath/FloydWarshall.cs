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

        public int[,] graphNode { get; set; }

        public int[,] nodeTampon { get; set; }

        public int MaxValue { get; set; }

        public void floyd_warshall()
        {
            int i, j, k;
            nodeTampon = new int[NumberOfNodes, NumberOfNodes];
            for (i = 0; i < NumberOfNodes; i++)
            {
                for (j = 0; j < NumberOfNodes; j++)
                {
                    nodeTampon[i, j] = graphNode[i, j];
                }
            }
            for (k = 0; k < NumberOfNodes; k++)
            {
                // Pick all vertices as source one by one
                for (i = 0; i < NumberOfNodes; i++)
                {
                    // Pick all vertices as destination for the
                    // above picked source
                    for (j = 0; j < NumberOfNodes; j++)
                    {
                        // If vertex k is on the shortest path from
                        // i to j, then update the value of dist[i][j]
                        if (nodeTampon[i, j] > (nodeTampon[i, k] + nodeTampon[k, j]))
                        {
                            nodeTampon[i, j] = nodeTampon[i, k] + nodeTampon[k, j];
                        }
                    }
                }
            }
            printSolution(nodeTampon);
        }

        private void printSolution(int[,] nodeTampon)
        {
            Console.WriteLine("Following matrix shows the shortest " +
                              "distances between every pair of vertices");
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
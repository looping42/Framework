using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.FlotMaximum
{
    public class FordFulkerson
    {
        public int[,] TwoDimensionArray { get; set; }

        public int[,] PathTwoDimensionArray { get; set; }

        public int NumberOfNodes { get; set; }

        public bool[] Visited { get; set; }

        /// <summary>
        ///
        /// </summary>
        private void PopulatePathTwoDimensionArray()
        {
            PathTwoDimensionArray = new int[NumberOfNodes, NumberOfNodes];
            for (int u = 0; u < NumberOfNodes; u++)
                for (int v = 0; v < NumberOfNodes; v++)
                    PathTwoDimensionArray[u, v] = TwoDimensionArray[u, v];
        }

        /// <summary>
        ///
        /// </summary>
        private void PopulateListNodeVisited()
        {
            Visited = new bool[NumberOfNodes];
            for (int i = 0; i < NumberOfNodes; ++i)
                Visited[i] = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool Bfs(int s, int t, int[] parent)
        {
            PopulateListNodeVisited();

            LinkedList<int> queue = new LinkedList<int>();
            queue.AddLast(s);
            Visited[s] = true;
            parent[s] = -1;

            // Standard BFS Loop
            while (queue.Count() != 0)
            {
                int u = queue.First();
                queue.RemoveFirst();

                for (int v = 0; v < NumberOfNodes; v++)
                {
                    if ((Visited[v] == false) && (PathTwoDimensionArray[u, v] > 0))
                    {
                        queue.AddLast(v);
                        parent[v] = u;
                        Visited[v] = true;
                    }
                }
            }
            return (Visited[t] == true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public int FordFulkersonWork(int s, int t)
        {
            int u, v;
            PopulatePathTwoDimensionArray();
            int[] parent = new int[NumberOfNodes];

            int max_flow = 0;

            while (Bfs(s, t, parent))
            {
                int path_flow = int.MaxValue;
                for (v = t; v != s; v = parent[v])
                {
                    u = parent[v];
                    path_flow = Math.Min(path_flow, PathTwoDimensionArray[u, v]);
                }

                for (v = t; v != s; v = parent[v])
                {
                    u = parent[v];
                    PathTwoDimensionArray[u, v] -= path_flow;
                    PathTwoDimensionArray[v, u] += path_flow;
                }

                max_flow += path_flow;
            }

            return max_flow;
        }
    }
}
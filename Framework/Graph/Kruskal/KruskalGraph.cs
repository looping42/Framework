using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Graph.Kruskal
{
    /// <summary>
    /// Recherche d'arbre couvrant minimal
    /*  pour le graphe ci dessous
             10
        0--------1
        |  \     |
       6|   5\   |15
        |      \ |
        2--------3
            4

    le résultat est
    2 -- 3 == 4
    0 -- 3 == 5
    0 -- 1 == 10
    */

    /// </summary>
    public class KruskalGraph
    {
        public int NumberofNodes { get; set; }
        public int NumberOfEdges { get; set; }
        public KruskalEdge[] Edges { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="NumberNodes"></param>
        /// <param name="NumberEdges"></param>
        public KruskalGraph(int NumberNodes, int NumberEdges)
        {
            NumberofNodes = NumberNodes;
            NumberOfEdges = NumberEdges;
            Edges = new KruskalEdge[NumberOfEdges];
            for (int i = 0; i < NumberEdges; ++i)
            {
                Edges[i] = new KruskalEdge();
            }
        }

        /// <summary>
        /// trouve la partie correspondant a l'élement i passé en paramétres
        /// </summary>
        /// <param name="Nodes">Noeud courant</param>
        /// <param name="i">élement i</param>
        /// <returns>valeur du noeud parent</returns>
        public int Find(KruskalNode[] Nodes, int i)
        {
            // find root and make root as parent of i (path compression)
            if (Nodes[i].Parent != i)
            {
                Nodes[i].Parent = Find(Nodes, Nodes[i].Parent);
            }

            return Nodes[i].Parent;
        }

        /// <summary>
        /// Fais l'union entre 2 noeuds en fonction de leur valeur
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Union(KruskalNode[] nodes, int x, int y)
        {
            int xroot = Find(nodes, x);
            int yroot = Find(nodes, y);

            // Attach smaller rank tree under root of high rank tree
            // (Union by Rank)
            if (nodes[xroot].Value < nodes[yroot].Value)
            {
                nodes[xroot].Parent = yroot;
            }
            else if (nodes[xroot].Value > nodes[yroot].Value)
            {
                nodes[yroot].Parent = xroot;
            }
            else // If ranks are same, then make one as root and increment  its rank by one
            {
                nodes[yroot].Parent = xroot;
                nodes[xroot].Value++;
            }
        }

        /// <summary>
        /// Kruskal algorythm
        /// </summary>
        public List<KruskalEdge> KruskalMST()
        {
            //Stocke le résultat de l'algorithme
            List<KruskalEdge> result = new List<KruskalEdge>();

            //Réorganise le tableau dans l'ordre de leur poids du plus petit au plus grand
            Array.Sort(Edges);

            // Allocate memory for creating V Nodes
            KruskalNode[] subsets = new KruskalNode[NumberofNodes];
            for (int i = 0; i < NumberofNodes; ++i)
            {
                subsets[i] = new KruskalNode();
                subsets[i].Parent = i;
                subsets[i].Value = 0;
            }

            int j = 0;  // Index used to pick next edge
            int e = 0;  // An index variable, used for result[]

            // Number of edges to be taken is equal to V-1
            while (e < NumberofNodes - 1)
            {
                // Step 2: Pick the smallest edge. And increment the index
                // for next iteration
                KruskalEdge next_edge = new KruskalEdge();
                next_edge = Edges[j++];

                int x = Find(subsets, next_edge.From);
                int y = Find(subsets, next_edge.To);

                // If including this edge does't cause cycle, include it
                // in result and increment the index of result for next edge
                if (x != y)
                {
                    result.Add(new KruskalEdge());
                    result[e++] = next_edge;
                    Union(subsets, x, y);
                }
                // Else discard the next_edge
            }
            return result;
        }
    }
}
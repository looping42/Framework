using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Graph.Prim
{
    public class Kruskal : IMinTree
    {
        private DisjointSet djset = new DisjointSet();
        private List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> edge = new List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge>();
        private int numNodes;

        public Kruskal(string inputFile)
        {
            edge = EdgeHandler.initEdges(inputFile);
            numNodes = EdgeHandler.calculateNumNodes(edge);
        }

        public List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> findMinTree()
        {
            edge.Sort(delegate (Framework.Graph.Prim.MinimumSpanningTree.MstEdge e1, Framework.Graph.Prim.MinimumSpanningTree.MstEdge e2)
            {
                if (e1.weight > e2.weight) return 1;
                if (e1.weight < e2.weight) return -1;
                return 0;
            });

            for (int i = 1; i <= numNodes; ++i)
            {
                djset.makeset(i);
            }

            List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> minSpanTree = new List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge>();
            int count = 0;
            int idx = 0;
            while (count < numNodes - 1)
            {
                if (djset.findset(edge[idx].v1) != djset.findset(edge[idx].v2))
                {
                    minSpanTree.Add(edge[idx]);
                    count++;
                    djset.union(edge[idx].v1, edge[idx].v2);
                }
                idx++;
            }

            return minSpanTree;
        }
    }
}
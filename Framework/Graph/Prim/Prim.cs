using Framework.Graph.Prim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Graph.Prim
{
    public class Prim : IMinTree
    {
        private MstGraph graph;
        private List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> edgeList;
        private int[] parent;
        private IndirectHeap heap = new IndirectHeap();

        public Prim(string inputFile)
        {
            edgeList = EdgeHandler.initEdges(inputFile);
            graph = new MstGraph(edgeList);
            parent = new int[graph.Length + 1];
        }

        public List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> findMinTree()
        {
            int start = 1; // start prim's algorithm from vertex 1

            int n = graph.Length;
            int[] key = new int[n + 1];

            for (int i = 0; i < n + 1; i++)
            {
                key[i] = int.MaxValue;
            }
            key[start] = 0;
            parent[start] = 0;

            heap.init(key);
            for (int i = 0; i < n; ++i)
            {
                int v = heap.deleteMin();
                LinkedList<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> adjList = graph.getList(v);
                foreach (Framework.Graph.Prim.MinimumSpanningTree.MstEdge e in adjList)
                {
                    int w = v == e.v1 ? e.v2 : e.v1;
                    if (heap.isIn(w) && e.weight < heap.keyVal(w))
                    {
                        parent[w] = v;
                        heap.decrease(w, e.weight);
                    }
                }
            }
            return null;
        }
    }
}
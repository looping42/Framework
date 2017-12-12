using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Graph.Prim
{
    public class MstGraph
    {
        List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> edgeList;
        LinkedList<Framework.Graph.Prim.MinimumSpanningTree.MstEdge>[] adjList;

        int length;
        public int Length { get { return length; } }

        const int MAX_NODES = 256;

        public MstGraph(List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> edgeList)
        {
            this.edgeList = edgeList;
            adjList = new LinkedList<Framework.Graph.Prim.MinimumSpanningTree.MstEdge>[MAX_NODES];
            for (int i = 0; i < MAX_NODES; ++i)
            {
                adjList[i] = new LinkedList<Framework.Graph.Prim.MinimumSpanningTree.MstEdge>();
            }
            initAdjList();
            length = EdgeHandler.calculateNumNodes(edgeList);
        }

        public LinkedList<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> getList(int v)
        {
            return adjList[v];
        }

        private void initAdjList()
        {
            for (int i = 0; i < edgeList.Count; ++i)
            {
                adjList[edgeList[i].v1].AddLast(edgeList[i]);
                adjList[edgeList[i].v2].AddLast(edgeList[i]);
            }
        }
    }

}

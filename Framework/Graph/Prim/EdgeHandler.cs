using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Framework.Graph.Prim
{
    public class EdgeHandler
    {

        public static List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> initEdges(string inputFile)
        {
            List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> edge = new List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge>();
            using (StreamReader reader = new StreamReader(inputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    edge.Add(createEdge(line.Split(',')));
                }
            }
            return edge;
        }

        public static int calculateNumNodes(List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> edge)
        {
            int numNodes = 0;
            foreach (Framework.Graph.Prim.MinimumSpanningTree.MstEdge e in edge)
            {
                if (e.v1 > numNodes) numNodes = e.v1;
                if (e.v2 > numNodes) numNodes = e.v2;
            }
            return numNodes;
        }

        private static Framework.Graph.Prim.MinimumSpanningTree.MstEdge createEdge(string[] edgePart)
        {
            Framework.Graph.Prim.MinimumSpanningTree.MstEdge e = new Framework.Graph.Prim.MinimumSpanningTree.MstEdge();
            string[] numberName;

            numberName = edgePart[0].Split(':');
            e.Vertex1_name = numberName[1];
            e.v1 = int.Parse(numberName[0]);

            numberName = edgePart[1].Split(':');
            e.Vertex2_name = numberName[1];
            e.v2 = int.Parse(numberName[0]);

            e.weight = int.Parse(edgePart[2]);
            return e;
        }
    }
}

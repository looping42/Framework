using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Graph.Prim
{
    public class MinimumSpanningTree
    {
        public struct MstEdge
        {
            public string Vertex1_name, Vertex2_name;
            public int weight;
            public int v1, v2;
        }

        public class MinSpanTree
        {
            private IMinTree minTreeAlgorithm;

            public MinSpanTree(IMinTree algorithm)
            {
                minTreeAlgorithm = algorithm;
            }

            public void findMinSpanTree()
            {
                List<MstEdge> minSpanTree = new List<MstEdge>();
                minSpanTree = minTreeAlgorithm.findMinTree();
            }

            public void printMinSpanTree()
            {
                List<MstEdge> minSpanTree = new List<MstEdge>();

                minSpanTree = minTreeAlgorithm.findMinTree();
                foreach (MstEdge e in minSpanTree)
                {
                    Console.WriteLine("{0} <--> {1}", e.Vertex1_name, e.Vertex2_name);
                }
            }

            public int solution(int[] A, int N)
            {
                int valParcours = 0;
                int somme = 0;
                foreach (var nbrUnique in A)
                {
                    somme = somme + nbrUnique;
                }

                List<int> result = new List<int>();
                foreach (int nbrUnique in A)
                {
                    valParcours = valParcours + nbrUnique;
                    result.Add(Math.Abs((somme - valParcours) - valParcours));
                }
                return result.Min();
            }
        }
    }
}
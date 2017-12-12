using Framework.Graph;
using Framework.Graph.Kruskal;
using Framework.Graph.Prim;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class UnitTestGraph
    {
        [TestMethod]
        public void TestGraph()
        {
            Graph G = new Graph(true);
            GraphNode u = new GraphNode("u");
            GraphNode v = new GraphNode("v");
            GraphNode w = new GraphNode("w");
            GraphNode x = new GraphNode("x");
            GraphNode y = new GraphNode("y");
            GraphNode z = new GraphNode("z");

            // Add GraphNodees
            G.AddNode(u);
            G.AddNode(v);
            G.AddNode(w);
            G.AddNode(x);
            G.AddNode(y);
            G.AddNode(z);
            // Add edges
            G.AddEdge(u, v, 3);
            G.AddEdge(u, x, 3);
            G.AddEdge(v, y, 3);
            G.AddEdge(w, y, 3);
            G.AddEdge(w, z, 3);
            G.AddEdge(x, v, 3);
            G.AddEdge(y, x, 3);
            G.AddEdge(z, z, 3);
            //Depth Search First
            List<GraphNode> result = G.DepthSearchFirst();

            foreach (GraphNode item in result)
            {
                Console.WriteLine("Vertex {0} has been found!", item.Value2);
            }

            //Undirect Graph
            Graph UG = new Graph(false);
            GraphNode ur = new GraphNode("r");
            GraphNode us = new GraphNode("s");
            GraphNode ut = new GraphNode("t");
            GraphNode uu = new GraphNode("u");
            GraphNode uv = new GraphNode("v");
            GraphNode uw = new GraphNode("w");
            GraphNode ux = new GraphNode("x");
            GraphNode uy = new GraphNode("y");

            // Add GraphNodees
            UG.AddNode(ur);
            UG.AddNode(us);
            UG.AddNode(ut);
            UG.AddNode(uu);
            UG.AddNode(uv);
            UG.AddNode(uw);
            UG.AddNode(ux);
            UG.AddNode(uy);
            // Add Edge
            UG.AddEdge(ur, uv, 3);
            UG.AddEdge(ur, us, 3);
            UG.AddEdge(us, uw, 3);
            UG.AddEdge(ut, uu, 3);
            UG.AddEdge(ut, uw, 3);
            UG.AddEdge(ut, ux, 3);
            UG.AddEdge(uu, ux, 3);
            UG.AddEdge(uu, uy, 3);
            UG.AddEdge(uw, ux, 3);
            UG.AddEdge(ux, uy, 3);

            //Breadth First Search
            List<GraphNode> result1 = UG.BreadthFirstSearch(us);

            foreach (GraphNode item in result1)
            {
                Console.WriteLine("Vertex {0} has been found!", item.Value2);
            }
        }

        [TestMethod]
        public void testGraphBreathFirstSearch()
        {
            Graph G = new Graph(false);
            GraphNode r = new GraphNode("r");
            GraphNode s = new GraphNode("s");
            GraphNode t = new GraphNode("t");
            GraphNode u = new GraphNode("u");
            GraphNode v = new GraphNode("v");
            GraphNode w = new GraphNode("w");
            GraphNode x = new GraphNode("x");
            GraphNode y = new GraphNode("y");

            // Add GraphNodees
            G.AddNode(r);
            G.AddNode(s);
            G.AddNode(t);
            G.AddNode(u);
            G.AddNode(v);
            G.AddNode(w);
            G.AddNode(x);
            G.AddNode(y);

            // Add edges
            G.AddEdge(r, s, 3);
            G.AddEdge(r, v, 3);

            G.AddEdge(s, r, 3);
            G.AddEdge(s, w, 3);

            G.AddEdge(t, u, 3);
            G.AddEdge(t, w, 3);
            G.AddEdge(t, x, 3);

            G.AddEdge(u, t, 3);
            G.AddEdge(u, x, 3);
            G.AddEdge(u, y, 3);

            G.AddEdge(v, r, 3);

            G.AddEdge(w, s, 3);
            G.AddEdge(w, t, 3);
            G.AddEdge(w, x, 3);

            G.AddEdge(x, w, 3);
            G.AddEdge(x, t, 3);
            G.AddEdge(x, u, 3);

            G.AddEdge(y, u, 3);
            G.AddEdge(y, x, 3);

            //Breadth First Search
            List<GraphNode> result = G.BreadthFirstSearch(s);
            List<string> resultvalue = new List<string>();
            List<int> resultvalue2 = new List<int>();

            foreach (GraphNode item in result)
            {
                Console.WriteLine(item.Value2);
                resultvalue.Add(item.Value2);
                Console.WriteLine(item.DatationStart);
                resultvalue2.Add(item.DatationStart);
            }
            CollectionAssert.AreEqual(resultvalue, new List<string> { "s", "r", "w", "v", "t", "x", "u", "y" });
            CollectionAssert.AreEqual(resultvalue2, new List<int> { 0, 1, 1, 2, 2, 2, 3, 3 });
        }

        [TestMethod]
        public void testGraphDepthSearchFirst()
        {
            Graph G = new Graph(true);
            GraphNode u = new GraphNode("u");
            GraphNode v = new GraphNode("v");
            GraphNode w = new GraphNode("w");
            GraphNode x = new GraphNode("x");
            GraphNode y = new GraphNode("y");
            GraphNode z = new GraphNode("z");

            // Add GraphNodees

            G.AddNode(u);
            G.AddNode(v);
            G.AddNode(w);
            G.AddNode(x);
            G.AddNode(y);
            G.AddNode(z);

            // Add edges

            G.AddEdge(u, v, 3);
            G.AddEdge(u, x, 3);

            G.AddEdge(v, y, 3);

            G.AddEdge(w, y, 3);
            G.AddEdge(w, z, 3);

            G.AddEdge(x, v, 3);

            G.AddEdge(y, x, 3);
            G.AddEdge(z, z, 3);

            //Depth Search First
            List<GraphNode> result = G.DepthSearchFirst();
            List<string> resultvalue = new List<string>();
            List<int> resultvalue2 = new List<int>();
            List<int> resultvalue3 = new List<int>();

            foreach (GraphNode item in result)
            {
                //Console.WriteLine(item.Value2);
                resultvalue.Add(item.Value2);
                //Console.WriteLine(item.Datation);
                resultvalue2.Add(item.DatationStart);
                //Console.WriteLine(item.DatationFinal);
                resultvalue3.Add(item.DatationEnd);
            }
            CollectionAssert.AreEqual(resultvalue, new List<string> { "x", "y", "v", "u", "z", "w" });
            CollectionAssert.AreEqual(resultvalue2, new List<int> { 4, 3, 2, 1, 10, 9 });
            CollectionAssert.AreEqual(resultvalue3, new List<int> { 5, 6, 7, 8, 11, 12 });
        }

        [TestMethod]
        public void testGraphKruskal()
        {
            /* Let us create following weighted graph
             10
        0--------1
        |  \     |
       6|   5\   |15
        |      \ |
        2--------3
            4       */
            int V = 4;  // Number of vertices in graph
            int E = 5;  // Number of edges in graph
            KruskalGraph graph = new KruskalGraph(V, E);

            // add edge 0-1
            graph.Edges[0].From = 0;
            graph.Edges[0].To = 1;
            graph.Edges[0].Weight = 10;

            // add edge 0-2
            graph.Edges[1].From = 0;
            graph.Edges[1].To = 2;
            graph.Edges[1].Weight = 6;

            // add edge 0-3
            graph.Edges[2].From = 0;
            graph.Edges[2].To = 3;
            graph.Edges[2].Weight = 5;

            // add edge 1-3
            graph.Edges[3].From = 1;
            graph.Edges[3].To = 3;
            graph.Edges[3].Weight = 15;

            // add edge 2-3
            graph.Edges[4].From = 2;
            graph.Edges[4].To = 3;
            graph.Edges[4].Weight = 4;

            List<KruskalEdge> result = graph.KruskalMST();
            // print the contents of result[] to display the built MST
            Console.WriteLine("Following are the edges in the constructed MST");
            for (int i = 0; i < result.Count; ++i)
                Console.WriteLine(result[i].From + " -- " + result[i].To + " == " +
                                   result[i].Weight);
        }

        [TestMethod]
        public void testGraphPrim()
        {
            Graph G = new Graph(true);
            GraphNode r = new GraphNode("0");
            GraphNode s = new GraphNode("1");
            GraphNode t = new GraphNode("2");
            GraphNode u = new GraphNode("3");
            GraphNode v = new GraphNode("4");
            GraphNode w = new GraphNode("5");
            GraphNode x = new GraphNode("6");
            GraphNode y = new GraphNode("7");
            GraphNode z = new GraphNode("8");

            // Add GraphNodees
            G.AddNode(r);
            G.AddNode(s);
            G.AddNode(t);
            G.AddNode(u);
            G.AddNode(v);
            G.AddNode(w);
            G.AddNode(x);
            G.AddNode(y);
            G.AddNode(z);
            // Add edges
            G.AddEdge(r, s, 4);
            G.AddEdge(r, y, 8);

            G.AddEdge(s, t, 8);
            G.AddEdge(s, y, 11);

            G.AddEdge(t, u, 7);
            G.AddEdge(t, z, 2);
            G.AddEdge(t, w, 4);

            G.AddEdge(u, v, 9);
            G.AddEdge(u, w, 14);

            G.AddEdge(v, w, 10);

            G.AddEdge(w, x, 2);

            G.AddEdge(x, y, 1);
            G.AddEdge(x, z, 6);

            G.AddEdge(y, z, 7);

            //Breadth First Search
            G.Prim(s);

            //List<string> resultvalue = new List<string>();
            //List<int> resultvalue2 = new List<int>();

            foreach (KeyValuePair<GraphNode, LinkedList<GraphEdge>> item in G.nodesKeyEdgeValues)
            {
                Console.WriteLine(item.Key.Value);
                foreach (var lien in item.Value)
                {
                    Console.Write("Parent " + lien.FromVertex.Value + " ");
                    //Console.Write("From " + lien.FromVertex.Value + " ");
                    //Console.Write("To " + lien.FromVertex.Value + " ");
                }
                //resultvalue.Add(item.Value2);
                //Console.WriteLine(item.Key.);
                //resultvalue2.Add(item.DatationStart);
            }
            //CollectionAssert.AreEqual(resultvalue, new List<string> { "s", "r", "w", "v", "t", "x", "u", "y" });
            //CollectionAssert.AreEqual(resultvalue2, new List<int> { 0, 1, 1, 2, 2, 2, 3, 3 });
        }

        [TestMethod]
        public void testGraphPrimv2()
        {
            int s = 4;
            int[] mst;
            string fname = "E:\\projetvisualstudio\\UnitTest\\TestProject\\FileTxt\\wGraph3.txt";

            GraphPrim g = new GraphPrim(fname);

            g.display();

            mst = g.MST_Prim(s);

            g.showMST(mst);

            Console.ReadLine();
        }

        [TestMethod]
        public void testGraphPrimv3()
        {
            int s = 4;
            int[] mst;
            string fname = "E:\\projetvisualstudio\\UnitTest\\TestProject\\FileTxt\\wGraph3V2.txt";

            GraphPrim g = new GraphPrim(fname);

            g.display();

            mst = g.MST_Prim(s);

            g.showMST(mst);

            Console.ReadLine();
        }

        [TestMethod]
        public void testGraphPrimv4()
        {
            int s = 4;
            int[] mst;
            string fname = "E:\\projetvisualstudio\\UnitTest\\TestProject\\FileTxt\\wGraph3V3.txt";

            GraphPrim g = new GraphPrim(fname);

            g.display();

            mst = g.MST_Prim(s);

            g.showMST(mst);

            Console.ReadLine();
        }

        [TestMethod]
        public void testGraphPrimv5()
        {
            //int i, j;

            //Console.WriteLine("Please enter the number of nodes in the graph");
            //int n = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine(n);
            //List<List<GraphPrimV2.node>> a = new List<List<GraphPrimV2.node>>();
            //for (i = 0; i < n; i++) a.Add(new List<GraphPrimV2.node>());
            //Console.WriteLine("Please enter the number of edges in the graph");
            //int e = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Now please enter the edges of the undirected graph one by one along with their wt.");
            //for (i = 0; i < e; i++)
            //{
            //    int p = Convert.ToInt32(Console.ReadLine());
            //    int q = Convert.ToInt32(Console.ReadLine());
            //    int w = Convert.ToInt32(Console.ReadLine());

            //    a.Add(new GraphPrimV2.node(q, w));
            //}
            ////till here the graph is ready with us in adjacency list form
            ////and we have finished taking the input from user
            ////now we write the find and union methods
            ////and finally we call Prims algo
            //Prim(a, n);
        }

        [TestMethod]
        public void testGraphPrimv6()
        {
            string inputFile = "E:\\projetvisualstudio\\UnitTest\\TestProject\\FileTxt\\wGraph3V4.txt";
            Framework.Graph.Prim.MinimumSpanningTree.MinSpanTree msTree = new Framework.Graph.Prim.MinimumSpanningTree.MinSpanTree(new Kruskal(inputFile));
            msTree.findMinSpanTree();
            msTree.printMinSpanTree();

            Console.WriteLine();

            msTree = new Framework.Graph.Prim.MinimumSpanningTree.MinSpanTree(new Prim(inputFile));
            msTree.findMinSpanTree();
            msTree.printMinSpanTree();

            Console.ReadLine();
        }
    }
}
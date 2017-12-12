using Framework.ShortestPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class UnitTestShortestPath
    {
        [TestMethod]
        public void UnitGraphInMatrix()
        {
            BellmanFord newShortPath = new BellmanFord();
            newShortPath.Nodes = new List<Node>();
            newShortPath.Edges = new List<Edge>();
            newShortPath.PosNodeStart = 0;
            newShortPath.MaxValue = -1;

            for (int i = 0; i < 5; i++)
            {
                Node a = new Node();
                a.Value = i;
                newShortPath.Nodes.Add(a);
            }

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[0], B = newShortPath.Nodes[1], Weight = 6 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[0], B = newShortPath.Nodes[3], Weight = 7 });

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[1], B = newShortPath.Nodes[2], Weight = 5 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[1], B = newShortPath.Nodes[3], Weight = 8 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[1], B = newShortPath.Nodes[4], Weight = -4 });

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[2], B = newShortPath.Nodes[0], Weight = -2 });

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[4], B = newShortPath.Nodes[2], Weight = 7 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[4], B = newShortPath.Nodes[0], Weight = 2 });

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[3], B = newShortPath.Nodes[4], Weight = 9 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[3], B = newShortPath.Nodes[2], Weight = -3 });

            Console.Error.WriteLine("Graph simple sens");
            //graph simple sens
            int[,] test = newShortPath.GraphInMatrix();

            for (int i = 0; i < test.GetLongLength(0); i++)
            {
                for (int j = 0; j < test.GetLongLength(1); j++)
                {
                    Console.Error.Write(test[i, j]);
                }
                Console.Error.WriteLine();
            }

            Console.Error.WriteLine("Graph double sens");
            //graphe double sens
            int[,] test2 = newShortPath.GraphInMatrixDoubleSens();
            for (int i = 0; i < test2.GetLongLength(0); i++)
            {
                for (int j = 0; j < test2.GetLongLength(1); j++)
                {
                    Console.Error.Write(test2[i, j]);
                }
                Console.Error.WriteLine();
            }
        }

        [TestMethod]
        public void UnitBellmanFord()
        {
            BellmanFord newShortPath = new BellmanFord();
            newShortPath.Nodes = new List<Node>();
            newShortPath.Edges = new List<Edge>();
            newShortPath.PosNodeStart = 0;
            newShortPath.MaxValue = 9999;
            for (int i = 1; i < 6; i++)
            {
                Node a = new Node();
                //a.Value = 0;
                a.Value = i;
                newShortPath.Nodes.Add(a);
            }

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[0], B = newShortPath.Nodes[1], Weight = 6 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[0], B = newShortPath.Nodes[3], Weight = 7 });

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[1], B = newShortPath.Nodes[2], Weight = 5 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[1], B = newShortPath.Nodes[3], Weight = 8 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[1], B = newShortPath.Nodes[4], Weight = -4 });

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[2], B = newShortPath.Nodes[1], Weight = -2 });

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[4], B = newShortPath.Nodes[2], Weight = 7 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[4], B = newShortPath.Nodes[0], Weight = 2 });

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[3], B = newShortPath.Nodes[4], Weight = 9 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[3], B = newShortPath.Nodes[2], Weight = -3 });

            string result = "";
            bool okNoCyleNeg = newShortPath.BellmanFordWork();
            if (okNoCyleNeg)
            {
                for (int i = 0; i < newShortPath.Nodes.Count; i++)
                {
                    result += newShortPath.Nodes[i].Value;
                    bool ok = true;
                    while (ok)
                    {
                        if (newShortPath.Nodes[i].parent != null)
                        {
                            result += newShortPath.Nodes[i].parent.Value;
                            newShortPath.Nodes[i] = newShortPath.Nodes[i].parent;
                        }
                        else
                        {
                            ok = false;
                        }
                    }
                }
            }
            Assert.AreEqual("0247047070-22470", result);
        }

        [TestMethod]
        public void UnitDjiskra()
        {
            Dijkstra newShortPath = new Dijkstra();
            newShortPath.Nodes = new List<Node>();
            newShortPath.Edges = new List<Edge>();
            newShortPath.PosNodeStart = 0;
            newShortPath.MaxValue = 9999;
            for (int i = 1; i < 6; i++)
            {
                Node a = new Node();
                a.Value = i;
                newShortPath.Nodes.Add(a);
            }

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[0], B = newShortPath.Nodes[1], Weight = 10 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[0], B = newShortPath.Nodes[3], Weight = 5 });

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[1], B = newShortPath.Nodes[2], Weight = 1 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[1], B = newShortPath.Nodes[3], Weight = 2 });

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[2], B = newShortPath.Nodes[4], Weight = 4 });

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[3], B = newShortPath.Nodes[1], Weight = 3 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[3], B = newShortPath.Nodes[2], Weight = 9 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[3], B = newShortPath.Nodes[4], Weight = 2 });

            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[4], B = newShortPath.Nodes[2], Weight = 6 });
            newShortPath.Edges.Add(new Edge() { A = newShortPath.Nodes[4], B = newShortPath.Nodes[0], Weight = 7 });

            newShortPath.DijkstraWork();

            string result = "";
            for (int i = 0; i < newShortPath.Nodes.Count; i++)
            {
                result += newShortPath.Nodes[i].Value;
                bool ok = true;
                while (ok)
                {
                    if (newShortPath.Nodes[i].parent != null)
                    {
                        result += newShortPath.Nodes[i].parent.Value;
                        newShortPath.Nodes[i] = newShortPath.Nodes[i].parent;
                    }
                    else
                    {
                        ok = false;
                    }
                }
            }
            Assert.AreEqual("085098505070", result);
        }

        [TestMethod]
        public void UnitFloydWarshall()
        {
            FloydWarshall newFloyd = new FloydWarshall();
            newFloyd.MaxValue = 9999;
            newFloyd.TwoDimensionArray = new int[5, 5] { { 0, 3, 8, 9999, -4 },
                                            { 9999, 0, 9999, 1, 7 },
                                            { 9999, 4, 0, 9999, 9999 },
                                            { 2, 9999, -5, 0, 9999 },
                                            { 9999, 9999,9999, 6, 0 } };
            //newFloyd.dist = new int[5, 5] { { 0, int.MaxValue, int.MaxValue, 2, int.MaxValue }, { 3, 0, 4, int.MaxValue, int.MaxValue }, { 8, int.MaxValue, 0, -5, int.MaxValue }, { int.MaxValue, 1, int.MaxValue, 0, 6 }, { -4, 7, int.MaxValue, int.MaxValue, 0 } };
            //newFloyd.dist = new int[5, 5] { { 0, int.MaxValue, 3, int.MaxValue, 1 },
            //                                { 5, 0, int.MaxValue, int.MaxValue, 3 },
            //                                { int.MaxValue, 2, 0, 4, int.MaxValue },
            //                                { 2, int.MaxValue, int.MaxValue, 0, 0 },
            //                                { int.MaxValue, int.MaxValue, 7, 1, 0 } };
            //newFloyd.dist = new int[5, 5] { { 0, 5, int.MaxValue, 2, int.MaxValue },
            //                                { int.MaxValue, 0, 2, int.MaxValue, int.MaxValue },
            //                                { 3, int.MaxValue, 0, int.MaxValue, 7 },
            //                                { int.MaxValue, int.MaxValue, 4, 0, 1 },
            //                                { 1, 3, int.MaxValue, int.MaxValue, 0 } };

            //newFloyd.dist = new int[4, 4] { {0,   5,  9999, 10},
            //                                {9999,  0,  3,  9999},
            //                                {9999, 9999, 0,   1},
            //                                {9999, 9999, 9999, 0} };
            newFloyd.NumberOfNodes = 5;
            newFloyd.Floyd_warshallWork();
            List<int> rez = new List<int>();
            newFloyd.GetPath(2, 4, ref rez);

            string result = "";
            foreach (var item in rez)
            {
                result += item;
            }
            Assert.AreEqual("21304", result);
        }
    }
}
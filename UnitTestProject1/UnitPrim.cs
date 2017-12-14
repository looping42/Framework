using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.Graph.Prim;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitPrim
    {
        [TestMethod]
        public void TestPrim()
        {
            /* Let us create the following graph
                        2    3
                    (0)--(1)--(2)
                     |   / \   |
                    6| 8/   \5 |7
                     | /     \ |
                    (3)-------(4) */
            int[,] graph = {
                { 0, 2, 0, 6, 0 },
                { 2, 0, 3, 8, 5 },
                { 0, 3, 0, 0, 7 },
                { 6, 8, 0, 0, 9 },
                { 0, 5, 7, 9, 0 },
            };

            PrimV2.Prim(graph, 5);
        }
    }
}
using Framework.FlotMaximum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    [TestClass]
    public class UnitTestFlow
    {
        [TestMethod]
        public void UnitFordFulkerson()
        {
            // Let us create a graph shown in the above example
            int[,] graph = new int[6, 6] { {-1, 16, 13, 0, 0, 0},
                                            {0, -1, 10, 12, 0, 0},
                                            {0, 4, -1, 0, 14, 0},
                                            {0, 0, 9, -1, 0, 20},
                                            {0, 0, 0, 7, -1, 4},
                                            {0, 0, 0, 0, 0, -1}
                                   };
            FordFulkerson newFlot = new FordFulkerson();
            newFlot.TwoDimensionArray = graph;
            newFlot.NumberOfNodes = 6;

            //Console.WriteLine(newFlot.FordFulkersonWork(0, 5));
            Assert.AreEqual("23", newFlot.FordFulkersonWork(0, 5).ToString());
        }
    }
}
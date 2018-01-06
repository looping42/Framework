using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.BackTrace;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitBackTrackLabyrint
    {
        [TestMethod]
        public void TestBackTrackLabyrint()
        {
            int[,] maze =  {    {1, 1, 1, 0},
                                {0, 0, 1, 0},
                                {1, 1, 1, 0},
                                {1, 0, 0, 0},
                                {1, 1, 1, 1}
    };
            int[,] sol = { {0, 0, 0, 0},

                        {0, 0, 0, 0},
                        {0, 0, 0, 0},
                        {0, 0, 0, 0},
                         {0, 0, 0, 0}
    };
            BackTrackLabyrint.SolveMaze(maze, 0, 0, sol, "down");
            BackTrackLabyrint.PrintSolution(sol);
        }
    }
}
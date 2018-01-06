using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.BackTrace;

namespace UnitTestProject1
{
    [TestClass]
    public class BackTRackKnightTourProblem
    {
        [TestMethod]
        public void KnightTour()
        {
            KnightTourProblem Knight = new KnightTourProblem();
            Knight.SolveKT();
        }
    }
}
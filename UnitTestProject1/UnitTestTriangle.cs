using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.Geometry.Triangle;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestTriangle

    {
        [TestMethod]
        public void TestIFpointIsInsideTriangle()
        {
            /* Let us check whether the point P(10, 15) lies inside the triangle
      formed by A(0, 0), B(20, 0) and C(10, 30) */

            if (Triangle.isInside(0, 0, 20, 0, 10, 30, 10, 15))
                Console.Error.WriteLine("Inside");
            else
                Console.Error.WriteLine("Not Inside");
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using Framework.Geometry.Carre;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestCarre
    {
        [TestMethod]
        public void TestIfPointAreCarre()
        {
            Point p1 = new Point(20, 10);

            Point p2 = new Point(10, 20);

            Point p3 = new Point(20, 20);

            Point p4 = new Point(10, 10);

            bool result = Carre.IsSquare(p1, p2, p3, p4);
            Console.Error.WriteLine(result);
        }
    }
}
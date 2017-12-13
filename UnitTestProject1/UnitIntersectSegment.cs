using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using Framework.IntersectSegment;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitIntersectSegment
    {
        [TestMethod]
        public void TestIntersectSegment1()
        {
            Point p1 = new Point(1, 1);
            Point q1 = new Point(10, 1);

            Point p2 = new Point(1, 2);
            Point q2 = new Point(10, 2);

            bool result = IntersectSegment.DoIntersect(p1, q1, p2, q2);
            //Console.WriteLine(result);

            Assert.AreEqual("False", result.ToString());
        }

        [TestMethod]
        public void TestIntersectSegment2()
        {
            Point p1 = new Point(10, 0);
            Point q1 = new Point(0, 10);

            Point p2 = new Point(0, 0);
            Point q2 = new Point(10, 10);

            bool result = IntersectSegment.DoIntersect(p1, q1, p2, q2);
            //Console.WriteLine(result);
            Assert.AreEqual("True", result.ToString());
        }

        [TestMethod]
        public void TestIntersectSegment3()
        {
            Point p1 = new Point(-5, -5);
            Point q1 = new Point(0, 0);

            Point p2 = new Point(1, 1);
            Point q2 = new Point(10, 10);

            bool result = IntersectSegment.DoIntersect(p1, q1, p2, q2);
            //Console.WriteLine(result);
            Assert.AreEqual("False", result.ToString());
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using Framework.IntersectSegment;
using System.Collections.Generic;

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

        [TestMethod]
        public void TestIntersectSegmentPolygon1()
        {
            Point[] polygon1 = new Point[] { new Point { X = 0, Y = 0 }, new Point { X = 10, Y = 0 }, new Point { X = 10, Y = 10 }, new Point { X = 0, Y = 10 } };

            //int n = polygon1.Length / polygon1[0]
            Point p = new Point(20, 20);
            bool result = IntersectSegment.IsInside(polygon1, 4, p);
            Console.WriteLine(result);
            Assert.AreEqual("False", result.ToString());
        }

        [TestMethod]
        public void TestIntersectSegmentPolygon2()
        {
            Point[] polygon1 = new Point[] { new Point { X = 0, Y = 0 }, new Point { X = 5, Y = 5 }, new Point { X = 5, Y = 0 } };

            Point p = new Point(5, 5);
            bool result = IntersectSegment.IsInside(polygon1, 3, p);
            Console.WriteLine(result);
            Assert.AreEqual("True", result.ToString());

            p = new Point(3, 3);
            result = IntersectSegment.IsInside(polygon1, 3, p);
            Console.WriteLine(result);
            Assert.AreEqual("True", result.ToString());

            p = new Point(5, 1);
            result = IntersectSegment.IsInside(polygon1, 3, p);
            Console.WriteLine(result);
            Assert.AreEqual("True", result.ToString());

            p = new Point(8, 1);
            result = IntersectSegment.IsInside(polygon1, 3, p);
            Console.WriteLine(result);
            Assert.AreEqual("False", result.ToString());
        }

        [TestMethod]
        public void TestIntersectSegmentPolygon3()
        {
            Point[] polygon1 = new Point[] { new Point { X = 0, Y = 0 }, new Point { X = 10, Y = 0 }, new Point { X = 10, Y = 10 }, new Point { X = 0, Y = 10 } };

            Point p = new Point(-1, 10);
            bool result = IntersectSegment.IsInside(polygon1, 4, p);
            Console.WriteLine(result);
            Assert.AreEqual("False", result.ToString());
        }

        [TestMethod]
        public void TestIntersectSegmentJarvisorWrapping()
        {
            Point[] points = new Point[] { new Point { X = 0, Y = 3 }, new Point { X = 2, Y = 2}, new Point { X = 1, Y = 1 }, new Point { X = 2, Y = 1 }
            ,new Point { X = 3, Y = 0 },   new Point { X = 0, Y = 0 } , new Point { X = 3, Y = 3}};

            List<Point> result = IntersectSegment.ConvexHullJarvis(points, 7);

            foreach (var item in result)
            {
                Console.Error.WriteLine(item);
            }
        }
    }
}
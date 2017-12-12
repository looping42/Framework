using Framework.Veb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    [TestClass]
    public class UnitTestVEB
    {
        [TestMethod]
        public void testVeb()
        {
            VebTree test = new VebTree(16);

            /* Insert Elements */
            test.Insert(3);
            test.Insert(5);
            test.Insert(8);
            test.Insert(10);
            test.Insert(12);
            test.Insert(13);
            test.Insert(14);
            test.Insert(15);

            Assert.AreEqual(true, test.Search(3));
            Assert.AreEqual(true, test.Search(5));
            Assert.AreEqual(true, test.Search(8));
            Assert.AreEqual(true, test.Search(10));
            Assert.AreEqual(true, test.Search(12));
            Assert.AreEqual(true, test.Search(13));
            Assert.AreEqual(true, test.Search(14));
            Assert.AreEqual(true, test.Search(15));

            Assert.AreEqual(true, !test.Search(0));
            Assert.AreEqual(true, !test.Search(1));
            Assert.AreEqual(true, !test.Search(2));
            Assert.AreEqual(true, !test.Search(4));
            Assert.AreEqual(true, !test.Search(6));
            Assert.AreEqual(true, !test.Search(7));
            Assert.AreEqual(true, !test.Search(9));
        }

        [TestMethod]
        public void testDeleteSearch()
        {
            VebTree test = new VebTree(16);

            /* Insert Elements */
            test.Insert(3);
            test.Insert(5);
            test.Insert(8);
            test.Insert(10);
            test.Insert(12);
            test.Insert(13);
            test.Insert(14);
            test.Insert(15);

            test.Delete(3);
            Assert.AreEqual(true, !test.Search(3));

            test.Delete(5);
            Assert.AreEqual(true, !test.Search(5));

            test.Delete(8);
            Assert.AreEqual(true, !test.Search(8));

            test.Delete(10);
            Assert.AreEqual(true, !test.Search(10));

            test.Delete(12);
            Assert.AreEqual(true, !test.Search(12));

            test.Delete(13);
            Assert.AreEqual(true, !test.Search(13));

            test.Delete(14);
            Assert.AreEqual(true, !test.Search(14));

            test.Delete(15);
            Assert.AreEqual(true, !test.Search(15));
        }

        [TestMethod]
        public void testPredecessor()
        {
            VebTree test = new VebTree(16);

            /* Insert Elements */
            test.Insert(3);
            test.Insert(5);
            test.Insert(8);
            test.Insert(10);
            test.Insert(12);
            test.Insert(13);
            test.Insert(14);
            test.Insert(15);

            Assert.AreEqual(-1, test.Predecessor(3));

            Assert.AreEqual(3, test.Predecessor(5));

            Assert.AreEqual(5, test.Predecessor(8));

            Assert.AreEqual(8, test.Predecessor(10));

            Assert.AreEqual(14, test.Predecessor(15));
        }

        [TestMethod]
        public void testSuccessor()
        {
            VebTree test = new VebTree(16);

            /* Insert Elements */
            test.Insert(3);
            test.Insert(5);
            test.Insert(8);
            test.Insert(10);
            test.Insert(12);
            test.Insert(13);
            test.Insert(14);
            test.Insert(15);

            Assert.AreEqual(5, test.Successor(3));

            Assert.AreEqual(8, test.Successor(5));

            Assert.AreEqual(10, test.Successor(8));

            Assert.AreEqual(12, test.Successor(10));

            Assert.AreEqual(-1, test.Successor(15));
        }

        [TestMethod]
        public void testMin()
        {
            VebTree test = new VebTree(16);

            /* Insert Elements */

            test.Insert(5);
            test.Insert(8);
            test.Insert(10);
            test.Insert(12);
            test.Insert(3);
            test.Insert(13);
            test.Insert(14);
            test.Insert(15);

            Assert.AreEqual(3, test.Min());
        }

        [TestMethod]
        public void testMax()
        {
            VebTree test = new VebTree(16);

            /* Insert Elements */
            test.Insert(3);
            test.Insert(5);
            test.Insert(8);
            test.Insert(10);
            test.Insert(12);
            test.Insert(13);
            test.Insert(14);
            test.Insert(15);
            Assert.AreEqual(15, test.Max());
        }
    }
}
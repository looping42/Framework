using Framework.BTree;
using Framework.BTree.BTreeV2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TestProject
{
    [TestClass]
    public class UnitTestBTree
    {
        private const int Degree = 3;

        private readonly int[] testKeyData = new int[] { 1, 12, 8, 2, 25 };
        private readonly int[] testPointerData = new int[] { 1, 12, 8, 2, 25 };

        [TestMethod]
        public void CreateBTree()
        {
            var btree = new BTree<int, int>(Degree);

            BtreeNode<int, int> root = btree.Root;
            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entries);
            Assert.IsNotNull(root.Children);
            Assert.AreEqual(0, root.Entries.Count);
            Assert.AreEqual(0, root.Children.Count);

            //Console.WriteLine(root.Children.Count);
        }

        [TestMethod]
        public void InsertOneBtreeNode()
        {
            var btree = new BTree<int, int>(Degree);

            btree.Insert(this.testKeyData[0], this.testPointerData[0]);
            TreeValidation.ValidateTree(btree.Root, Degree, this.testKeyData.Take(0 + 1).ToArray());

            btree.Insert(this.testKeyData[1], this.testPointerData[1]);
            TreeValidation.ValidateTree(btree.Root, Degree, this.testKeyData.Take(1 + 1).ToArray());

            foreach (var item in btree.Root.Entries)
            {
                Console.WriteLine("clé" + item.Key);
                Console.WriteLine("pointeur" + item.Pointer);
            }
            //Assert.AreEqual(1, btree.Height);
        }

        [TestMethod]
        public void InsertMultipleBtreeNodesToSplit()
        {
            var btree = new BTree<int, int>(Degree);

            for (int i = 0; i < this.testKeyData.Length; i++)
            {
                this.InsertTestDataAndValidateTree(btree, i);
            }

            Assert.AreEqual(1, btree.Height);
        }

        [TestMethod]
        public void DeleteBtreeNodes()
        {
            var btree = new BTree<int, int>(Degree);

            for (int i = 0; i < this.testKeyData.Length; i++)
            {
                this.InsertTestData(btree, i);
            }

            for (int i = 0; i < this.testKeyData.Length; i++)
            {
                btree.Delete(this.testKeyData[i]);
                TreeValidation.ValidateTree(btree.Root, Degree, this.testKeyData.Skip(i + 1).ToArray());
            }

            Assert.AreEqual(1, btree.Height);
        }

        [TestMethod]
        public void DeleteNonExistingBtreeNode()
        {
            var btree = new BTree<int, int>(Degree);

            for (int i = 0; i < this.testKeyData.Length; i++)
            {
                this.InsertTestData(btree, i);
            }

            btree.Delete(99999);
            TreeValidation.ValidateTree(btree.Root, Degree, this.testKeyData.ToArray());
        }

        [TestMethod]
        public void SearchBtreeNodes()
        {
            var btree = new BTree<int, int>(Degree);

            for (int i = 0; i < this.testKeyData.Length; i++)
            {
                this.InsertTestData(btree, i);
                this.SearchTestData(btree, i);
            }
        }

        [TestMethod]
        public void SearchNonExistingBtreeNode()
        {
            var btree = new BTree<int, int>(Degree);

            // search an empty tree
            Entry<int, int> nonExisting = btree.Search(9999);
            Assert.IsNull(nonExisting);

            for (int i = 0; i < this.testKeyData.Length; i++)
            {
                this.InsertTestData(btree, i);
                this.SearchTestData(btree, i);
            }

            // search a populated tree
            nonExisting = btree.Search(9999);
            Assert.IsNull(nonExisting);
        }

        [TestMethod]
        public void InsertMultipleBtreeNodesToSplitV2()
        {
            var btree = new BTree<int, int>(Degree);

            for (int i = 0; i < this.testKeyData.Length; i++)
            {
                this.InsertTestDataAndValidateTree(btree, i);
            }
            BtreeNode<int, int> temp = btree.Root;
            Display(temp);
            //Assert.AreEqual(3, btree.Height);
        }

        private static void Display(BtreeNode<int, int> btree)
        {
            foreach (var bt in btree.Children)
            {
                foreach (var item in bt.Entries)
                {
                    Console.Write(item.Key + "|");
                }
                if (bt.Children != null)
                {
                    Console.WriteLine();
                    Display(bt);
                }
            }
        }

        #region Private Helper Methods

        private void InsertTestData(BTree<int, int> btree, int testDataIndex)
        {
            btree.Insert(this.testKeyData[testDataIndex], this.testPointerData[testDataIndex]);
        }

        private void InsertTestDataAndValidateTree(BTree<int, int> btree, int testDataIndex)
        {
            btree.Insert(this.testKeyData[testDataIndex], this.testPointerData[testDataIndex]);
            TreeValidation.ValidateTree(btree.Root, Degree, this.testKeyData.Take(testDataIndex + 1).ToArray());
        }

        private void SearchTestData(BTree<int, int> btree, int testKeyDataIndex)
        {
            for (int i = 0; i <= testKeyDataIndex; i++)
            {
                Entry<int, int> entry = btree.Search(this.testKeyData[i]);
                Assert.IsNotNull(this.testKeyData[i]);
                Assert.AreEqual(this.testKeyData[i], entry.Key);
                Assert.AreEqual(this.testPointerData[i], entry.Pointer);
            }
        }

        #endregion Private Helper Methods
    }
}
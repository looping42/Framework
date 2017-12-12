using Framework.BTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public static class TreeValidation
    {
        public static void ValidateTree(BtreeNode<int, int> tree, int degree, params int[] expectedKeys)
        {
            var foundKeys = new Dictionary<int, List<Entry<int, int>>>();
            ValidateSubtree(tree, tree, degree, int.MinValue, int.MaxValue, foundKeys);
            Assert.AreEqual(0, expectedKeys.Except(foundKeys.Keys).Count());
            foreach (var keyValuePair in foundKeys)
            {
                Assert.AreEqual(1, keyValuePair.Value.Count);
            }
        }

        private static void UpdateFoundKeys(Dictionary<int, List<Entry<int, int>>> foundKeys, Entry<int, int> entry)
        {
            List<Entry<int, int>> foundEntries;
            if (!foundKeys.TryGetValue(entry.Key, out foundEntries))
            {
                foundEntries = new List<Entry<int, int>>();
                foundKeys.Add(entry.Key, foundEntries);
            }

            foundEntries.Add(entry);
        }

        private static void ValidateSubtree(BtreeNode<int, int> root, BtreeNode<int, int> BtreeNode, int degree, int BtreeNodeMin, int BtreeNodeMax, Dictionary<int, List<Entry<int, int>>> foundKeys)
        {
            if (root != BtreeNode)
            {
                Assert.IsTrue(BtreeNode.Entries.Count >= degree - 1);
                Assert.IsTrue(BtreeNode.Entries.Count <= (2 * degree) - 1);
            }

            for (int i = 0; i <= BtreeNode.Entries.Count; i++)
            {
                int subtreeMin = BtreeNodeMin;
                int subtreeMax = BtreeNodeMax;

                if (i < BtreeNode.Entries.Count)
                {
                    var entry = BtreeNode.Entries[i];
                    UpdateFoundKeys(foundKeys, entry);
                    Assert.IsTrue(entry.Key >= BtreeNodeMin && entry.Key <= BtreeNodeMax);

                    subtreeMax = entry.Key;
                }

                if (i > 0)
                {
                    subtreeMin = BtreeNode.Entries[i - 1].Key;
                }

                if (!BtreeNode.IsLeaf)
                {
                    Assert.IsTrue(BtreeNode.Children.Count >= degree);
                    ValidateSubtree(root, BtreeNode.Children[i], degree, subtreeMin, subtreeMax, foundKeys);
                }
            }
        }
    }
}
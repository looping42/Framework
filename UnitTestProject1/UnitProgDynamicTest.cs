using Framework.CutRod;
using Framework.DynamicProgrammation;
using Framework.DynamicProgrammation.Huffman;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class UnitProgDynamicTest
    {
        [TestMethod]
        public void TestCutBarMemo()
        {
            int[] prix = { 1, 5, 8, 9, 10, 17, 17, 20 };

            int taille = prix.Length;

            int result = DynamicProg.CutRod(5, prix);

            //Console.WriteLine( result);
            Assert.AreEqual(result.ToString(), "13");
        }

        [TestMethod]
        public void TestCutBarMemo2()
        {
            int[] prix = { 1, 5, 8, 9, 10, 17, 17, 20, 24, 30 };

            int taille = prix.Length;

            int result = DynamicProg.CutRod(2, prix);

            Assert.AreEqual(result.ToString(), "5");
        }

        [TestMethod]
        public void TestMultipleMatrice()
        {
            int[,] tab = { { 1, 5, 8,},
                           { 9, 3, 2,},
                           { 2, 4, 6} };

            int[,] tab2 = { { 7, 2, 3},
                           { 4, 3, 1  },
                           { 10, 5, 4 } };

            int[,] result = DynamicProg.MultipleMatrice(tab, tab2);

            for (int i = 0; i < result.GetLongLength(0); i++)
            {
                for (int j = 0; j < result.GetLongLength(1); j++)
                {
                    Console.Write(result[i, j] + ";");
                }
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void TestOrderChaineMatrix()
        {
            int[] tab3 = { 30, 35, 15, 5, 10, 20, 25 };
            MutliplyMatrix test = new MutliplyMatrix();
            test.matrixChainOrder(tab3);

            test.printOptimalParenthesizationsCall(0, 5);

            //Assert.AreEqual(MutliplyMatrix.OrderChaineMatrix(tab3, 1, tab3.Length - 1).ToString(), "18");
        }

        [TestMethod]
        public void TestSelectMaxActivities()
        {
            int[] s = { 1, 3, 0, 5, 8, 5 };
            int[] f = { 2, 4, 6, 7, 9, 9 };
            List<int> result = DynamicProg.SelectMaxActivities(s, f);

            //for (int i = 0; i < result.Count; i++)
            //{
            //    Console.Write(result[i]);
            //}
            int[] reztest = { 0, 1, 3, 4 };
            CollectionAssert.AreEqual(DynamicProg.SelectMaxActivities(s, f), reztest);
        }

        [TestMethod]
        public void TestSelectMaxActivitiesV2()
        {
            int[] s = { 1, 3, 0, 5, 3, 5, 6, 8, 8, 2, 12 };
            int[] f = { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            List<int> result = DynamicProg.SelectMaxActivities(s, f);

            //for (int i = 0; i < result.Count; i++)
            //{
            //    Console.Write(result[i] + ";");
            //}
            int[] reztest = { 0, 3, 7, 10 };
            CollectionAssert.AreEqual(DynamicProg.SelectMaxActivities(s, f), reztest);
        }

        //marche pas pourquoi
        [TestMethod]
        public void TestSelectMaxActivitiesV4()
        {
            int[] s = { 3, 9, 11, 16, 24 };
            int[] f = { 7, 10, 16, 24, 28 };
            List<int> result = DynamicProg.SelectMaxActivities(s, f);

            for (int i = 0; i < result.Count; i++)
            {
                Console.Write(result[i] + ";");
            }
            Console.WriteLine("nombre" + result.Count);
            int[] reztest = { 0, 1, 2, 3, 4 };
            CollectionAssert.AreEqual(DynamicProg.SelectMaxActivities(s, f), reztest);
        }

        [TestMethod]
        public void TestSelectMaxActivitiesRecursive()
        {
            int[] s = { 1, 3, 0, 5, 3, 5, 6, 8, 8, 2, 12 };
            int[] f = { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            string result = "0 ";
            result += DynamicProg.SelectMaxActivitiesRecursive(s, f, 0, f.Length);

            //Console.WriteLine(result);

            Assert.AreEqual(result, "0 3 7 10 ");
        }

        [TestMethod]
        public void TestHuffman()
        {
            string input = "abcdef";

            TreeHuffman huffmanTree = new TreeHuffman();

            // Build the Huffman tree
            huffmanTree.Build(input);

            // Encode
            BitArray encoded = huffmanTree.Encode(input);
            Console.Write("Encoded: ");
            foreach (bool bit in encoded)
            {
                Console.Write((bit ? 1 : 0) + "");
            }
            Console.WriteLine();

            // Decode
            string decoded = huffmanTree.Decode(encoded);

            Console.WriteLine("decoded: " + decoded);
        }

        [TestMethod]
        public void TestHuffmanV2()
        {
            string input = "rttihhhhrccaeeanecca";
            TreeHuffman huffmanTree = new TreeHuffman();

            //// Build the Huffman tree
            huffmanTree.Build(input);
            // Encode
            BitArray encoded = huffmanTree.Encode(input);

            Console.Write("Encoded: ");
            foreach (bool bit in encoded)
            {
                Console.Write((bit ? 1 : 0) + "");
            }
            Console.WriteLine();

            // Decode
            string decoded = huffmanTree.Decode(encoded);

            Console.WriteLine("Decoded: " + decoded);
        }
    }
}
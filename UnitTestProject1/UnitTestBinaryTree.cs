using Framework.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    [TestClass]
    public class UnitTestBinaryTree
    {
        [TestMethod]
        public void UnitTestBinaryTreeV1()
        {
            Tree b = new Tree(2, new Tree(1), new Tree(4, new Tree(3), new Tree(5)));
            Tree c = new Tree(10, new Tree(8), new Tree(12));
            Tree a = new Tree(6, b, c);

            Console.Error.WriteLine("parcours infixe a :");
            a.inorderTraversal();
            Console.Error.WriteLine("parcours infixe b :");
            b.inorderTraversal();
            Console.Error.WriteLine("parcours infixe c :");
            c.inorderTraversal();

            Console.Error.WriteLine("parcours prefixe a :");
            a.PrefixTraversal();
            Console.Error.WriteLine("parcours prefixe b :");
            b.PrefixTraversal();
            Console.Error.WriteLine("parcours prefixe c :");
            c.PrefixTraversal();

            Console.Error.WriteLine("parcours postfixe a :");
            a.PostfixeTraversal();
            Console.Error.WriteLine("parcours postfixe b :");
            b.PostfixeTraversal();
            Console.Error.WriteLine("parcours postfixe c :");
            c.PostfixeTraversal();

            Console.Error.WriteLine("number of nodes " + a.NumberOfNodes());

            a.InsertValueInTree(6);
            a.InsertValueInTree(11);
            a.InsertValueInTree(7);
            a.InsertValueInTree(15);

            Console.Error.WriteLine("L'arbre a est-il egal a lui-meme? " + Tree.IsTreeIsTheSame(a, a));
            Console.Error.WriteLine("Les arbres a et b sont-ils egaux? " + Tree.IsTreeIsTheSame(a, b));
            Console.Error.WriteLine("La hauteur de l'arbre a est " + Tree.HeightTree(a));
            Console.Error.WriteLine("L'arbre a est-il un ABR? " + Tree.IsBinaryTree(a));
            Console.Error.WriteLine("7 est-il present dans a? " + a.Research(7));
            Console.Error.WriteLine("12 est-il present dans a? " + a.Research(12));
            //a.InsertValueInTree(35);

            //b.RemoveValueInTree(10);
            //a.RemoveValueInTree(10);
            Console.Error.WriteLine("35 est-il present dans a? " + a.Research(35));
            Console.Error.WriteLine("10 est-il present dans a? " + a.Research(10));
            //Console.Error.WriteLine("Le resultat de l'ajout de 7 dans a " + a);

            Console.Error.WriteLine("La valeur minimum de l'arbre A est  " + Tree.TreeMinimumValue(a).Value);
            Console.Error.WriteLine("La valeur Maximum de l'arbre A est  " + Tree.TreeMaximumValue(a).Value);

            Tree temp = Tree.TreeSearchSuccessor(a.Right);
            //a.getParent(a.Right);
            Console.Error.WriteLine("L'arbre enfant est {0}", a.Right.Value);
            Console.Error.WriteLine("L'arbre parent est {0}", temp.Value);
            Tree temp2 = Tree.TreeSearchSuccessor(a.Left);
            ////a.getParent(a.Left);
            Console.Error.WriteLine("L'arbre enfant est {0}", a.Left.Value);
            Console.Error.WriteLine("L'arbre parent est {0}", temp2.Value);

            Tree temp3 = Tree.TreeSearchPredecessor(a.Right);
            Console.Error.WriteLine("L'arbre enfant est {0}", a.Right.Value);
            Console.Error.WriteLine("L'arbre parent est {0}", temp3.Value);

            Tree temp4 = Tree.TreeSearchPredecessor(a.Left);
            Console.Error.WriteLine("L'arbre enfant est {0}", a.Left.Value);
            Console.Error.WriteLine("L'arbre parent est {0}", temp4.Value);

            Console.Error.WriteLine("Fringe " + a.fringe());

            //foreach (var item in a.fringe())
            //{
            //    Console.WriteLine(item.Value);
            //}

            Console.Error.WriteLine("est une feuille " + a.IsLeaf());

            //Tree a = new Tree();
            //Tree temp = new Tree(81);
            //Tree.InsertWithParent(b, temp);

            //Tree temp2 = new Tree(102);
            //Tree.InsertWithParent(b, temp2);

            //Tree temp3 = new Tree(28);
            //Tree.InsertWithParent(b, temp3);

            //Tree temp4 = new Tree(6);
            //Tree.InsertWithParent(b, temp4);

            //Console.Error.WriteLine("parcours infixe a :");
            //a.ParcoursInfixe();

            //Console.Error.WriteLine("parcours prefixe a :");
            //a.ParcoursPrefixe();

            //Console.Error.WriteLine("parcours infixe b :");
            //b.ParcoursInfixe();

            //Console.Error.WriteLine("parcours infixe c :");
            //c.ParcoursInfixe();

            //Tree test = Tree.RemoveNode(b, 58);
            ////test = Tree.deleteRec(a, 102);
            //test = Tree.RemoveNode(b, 45);
            //test = Tree.RemoveNode(b, 12);

            //Tree.SupprimerNode(a, a.Left.Left);

            //a.RemoveValueInTree(102);
            Console.Error.WriteLine("La hauteur de l'arbre a est " + Tree.HeightTree(a));

            Console.Error.WriteLine(a.ToString());
            Console.Error.WriteLine("##############################################################");

            Console.Error.WriteLine(b.ToString());
            Console.Error.WriteLine("##############################################################");

            Console.Error.WriteLine(c.ToString());
        }

        [TestMethod]
        public void UnitTestTreeRedBlack()
        {
            TreeRedBlack tree = new TreeRedBlack();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(1);
            tree.Insert(9);
            tree.Insert(-1);
            tree.Insert(11);
            tree.Insert(6);

            Console.Error.WriteLine(tree.ToString());
            //tree.DisplayTree();
            //tree.Delete(-1);
            //tree.DisplayTree();
            //tree.Delete(9);
            //tree.DisplayTree();
            //tree.Delete(5);
            //tree.ToString();
            //tree.DisplayTree();

            //Console.ReadLine();
        }
    }
}
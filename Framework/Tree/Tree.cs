using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tree
{
    /// <summary>
    /// Arbre binaire de recherche
    /// </summary>
    public class Tree : UtilTree
    {
        /// <summary>
        /// Création Feuille
        /// </summary>
        /// <param name="val"></param>
        public Tree(int val)
        {
            this.Value = val;
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// Constructeur avec les valeurs des branches
        /// </summary>
        /// <param name="val">valeur</param>
        /// <param name="g">Branche gauche</param>
        /// <param name="d">Branche droite</param>
        public Tree(int val, Tree g, Tree d)
        {
            this.Value = val;
            this.Left = g;
            this.Right = d;
        }

        /// <summary>
        ///valeur enfant gauche
        /// </summary>
        /// <returns></returns>
        public bool HasLeft()
        {
            return Left != null;
        }

        /// <summary>
        /// valeur enfant droite
        /// </summary>
        /// <returns></returns>
        public bool HasRight()
        {
            return Right != null;
        }

        /// <summary>
        /// si c'est un noeud avec 2 feuilles
        /// </summary>
        /// <returns></returns>
        public bool IsInternal()
        {
            return Left != null || Right != null;
        }

        /// <summary>
        /// Si c'est une feuille
        /// </summary>
        /// <returns></returns>
        public bool IsLeaf()
        {
            return Left == null && Right == null;
        }

        /// <summary>
        /// Constructeur vide
        /// </summary>
        public Tree()
        {
        }

        /// <summary>
        /// Parcours dans l'ordre croissant
        /// La racine se trouve entre le sous arbres gauche et le sous arbres droit
        /// </summary>
        public void inorderTraversal()
        {
            if (Left != null)
            {
                Left.inorderTraversal();
            }
            Console.Error.WriteLine(Value);
            if (Right != null)
            {
                Right.inorderTraversal();
            }
        }

        /// <summary>
        /// Parcours
        /// La racine sera affiché avant le coté gauche puis le coté droit
        /// </summary>
        public void PrefixTraversal()
        {
            Console.Error.WriteLine(Value);
            if (Left != null)
                Left.PrefixTraversal();
            if (Right != null)
                Right.PrefixTraversal();
        }

        /// <summary>
        /// Parcours
        ///  la racine sera affiché aprés les valeurs des sous arbres coté gauche puis coté droit
        /// </summary>
        public void PostfixeTraversal()
        {
            if (Left != null)
                Left.PostfixeTraversal();
            if (Right != null)
                Right.PostfixeTraversal();
            Console.Error.WriteLine(Value);
        }

        /// <summary>
        /// Recherche de valeur dans l'arbre
        /// </summary>
        /// <param name="value">valeur</param>
        /// <returns>booléen</returns>
        public bool Research(int value)
        {
            if (value == Value)
                return true;
            if ((value < Value) && (Left != null))
                return (Left.Research(value));
            if ((value > Value) && (Right != null))
                return (Right.Research(value));
            return false;
        }

        /// <summary>
        /// Insertion d'une valeur pour le Tree courant
        /// </summary>
        /// <param name="value">valeur</param>
        public void InsertValueInTree(int value)
        {
            if (value == Value)
                return;  // la valeur est deja dans l'arbre
            if (value < Value)
            {
                if (Left != null)
                    Left.InsertValueInTree(value);
                else
                    Left = new Tree(value);
            }
            if (value > Value)
            {
                if (Right != null)
                    Right.InsertValueInTree(value);
                else
                    Right = new Tree(value);
            }
        }

        /// <summary>
        /// Remove d'une valeur pour le Tree courant
        /// </summary>
        /// <param name="valToDelete">Valeur à effacer</param>
        /// <returns></returns>
        public Tree RemoveValueInTree(int valToDelete)
        {
            if (this == null) return this;

            if (valToDelete < this.Value)
            {
                this.Left = RemoveNode(this.Left, valToDelete);
            }
            else if (valToDelete > this.Value)
            {
                this.Right = RemoveNode(this.Right, valToDelete);
            }
            else
            {
                // Noeud avec un enfant ou sans enfant
                if (this.Left == null)
                    return this.Right;
                else if (this.Right == null)
                    return this.Left;

                //Noeud avec 2 enfants , recherche du neoud le plus petit
                Tree Temp = Tree.TreeMinimumValue(this.Right);
                this.Value = Temp.Value;

                //Efface le successeur dans l'ordre
                this.Right = RemoveNode(this.Right, this.Value);
            }

            return this;
        }

        //public static void Transplante(Tree Depart, Tree u, Tree v)
        //{
        //    if (u.Parent == null)
        //    {
        //        Depart = v;
        //    }
        //    else if (u == u.Parent.Left)
        //    {
        //        u.Parent.Left = v;
        //    }
        //    else
        //    {
        //        u.Parent.Right = v;
        //    }
        //    if (v != null)
        //    {
        //        v.Parent = u.Parent;
        //    }
        //}

        //public static void SupprimerNode(Tree depart, Tree z)
        //{
        //    if (z.Left == null)
        //    {
        //        Tree.Transplante(depart, z, z.Right);
        //    }
        //    else if (z.Right == null)
        //    {
        //        Tree.Transplante(depart, z, z.Left);
        //    }
        //    else
        //    {
        //        Tree y = Tree.TreeMinimumValue(z.Right);
        //        if (y.Parent != z)
        //        {
        //            Tree.Transplante(depart, y, y.Right);
        //            y.Right = z.Right;
        //            y.Right.Parent = y;
        //        }
        //        Tree.Transplante(depart, z, y);
        //        y.Left = z.Left;
        //        y.Left.Parent = y;
        //    }
        //}

        /// <summary>
        /// Insertion d'une branche dans un arbre choisi
        /// Rajoute le parent automatiquement
        /// </summary>
        /// <param name="t"></param>
        /// <param name="toInsert"></param>
        public static void InsertWithParent(Tree t, Tree toInsert)
        {
            Tree y = null;
            Tree x = t;
            while (x != null)
            {
                y = x;
                if (toInsert.Value < x.Value)
                {
                    x = x.Left;
                }
                else
                {
                    x = x.Right;
                }
            }
            toInsert.Parent = y;
            if (y == null)
            {
                t = toInsert;
            }
            else if (toInsert.Value < y.Value)
            {
                y.Left = toInsert;
            }
            else
            {
                y.Right = toInsert;
            }
        }

        /// <summary>
        /// Efface le noeud puis réorganise le Tree
        /// </summary>
        /// <param name="treeBase">Tree</param>
        /// <param name="valToDelete">Valeur à effacer</param>
        /// <returns>Le nouvel arbre</returns>
        public static Tree RemoveNode(Tree treeBase, int valToDelete)
        {
            if (treeBase == null) return treeBase;

            if (valToDelete < treeBase.Value)
            {
                treeBase.Left = RemoveNode(treeBase.Left, valToDelete);
            }
            else if (valToDelete > treeBase.Value)
            {
                treeBase.Right = RemoveNode(treeBase.Right, valToDelete);
            }
            else
            {
                // Noeud avec un enfant ou sans enfant
                if (treeBase.Left == null)
                    return treeBase.Right;
                else if (treeBase.Right == null)
                    return treeBase.Left;

                //Noeud avec 2 enfants , recherche du neoud le plus petit
                Tree Temp = Tree.TreeMinimumValue(treeBase.Right);
                treeBase.Value = Temp.Value;

                //Efface le successeur dans l'ordre
                treeBase.Right = RemoveNode(treeBase.Right, treeBase.Value);
            }

            return treeBase;
        }

        /// <summary>
        /// Vérifie si le Tree a est le même que le Tree b
        /// </summary>
        /// <param name="a">Tree a</param>
        /// <param name="b">Tree b</param>
        /// <returns>booléen</returns>
        public static bool IsTreeIsTheSame(Tree a, Tree b)
        {
            if ((a == null) && (b == null))
                return true;
            if ((a == null) && (b != null))
                return false;
            if ((a != null) && (b == null))
                return false;

            // A ce point, a et b != null, on peut acceder a leurs champs
            if (a.Value != b.Value)
                return false;
            return (IsTreeIsTheSame(a.Left, b.Left)
                && IsTreeIsTheSame(a.Right, b.Right));
        }

        /// <summary>
        /// Hauteur de larbre
        /// </summary>
        /// <param name="a">Arbre a</param>
        /// <returns></returns>
        public static int HeightTree(Tree a)
        {
            if (a == null)
                return 0;
            else
                return (1 + Math.Max(HeightTree(a.Left), HeightTree(a.Right)));
        }

        /// <summary>
        /// Nombres de noeuds dans l'arbre
        /// </summary>
        /// <returns></returns>
        public int NumberOfNodes()
        {
            return 1 + (HasLeft() ? Left.NumberOfNodes() : 0) + (HasRight() ? Right.NumberOfNodes() : 0);
        }

        public List<Tree> fringe()
        {
            List<Tree> f = new List<Tree>();
            addToFringe(f);
            return f;
        }

        /**
         * Helper method for fringe, adding fringe data to the ArrayList.
         */

        private void addToFringe(List<Tree> fringe)
        {
            if (IsLeaf())
                fringe.Add(new Tree());
            else
            {
                if (HasLeft())
                    Left.addToFringe(fringe);
                if (HasRight())
                    Right.addToFringe(fringe);
            }
        }

        /// <summary>
        /// vérifie si le Tree a est un arbre binaire
        /// </summary>
        /// <param name="a">Tree a</param>
        /// <returns>booléen</returns>
        public static bool IsBinaryTree(Tree a)
        {
            if (a == null)
                return true;
            if ((a.Left != null) && (a.Left.Value > a.Value))
                return false;
            if ((a.Right != null) && (a.Value > a.Right.Value))
                return false;
            return (IsBinaryTree(a.Left) && IsBinaryTree(a.Right));
        }

        /// <summary>
        ///Recherche du noeud qui succéde au noeud présent
        /// </summary>
        /// <param name="a">Tree</param>
        /// <returns>Le noeud qui succéde le noeud entrant</returns>
        public static Tree TreeSearchSuccessor(Tree a)
        {
            if (a == null)
            {
                return null;
            }
            if (a.Right != null)
            {
                return Tree.TreeMinimumValue(a.Right);
            }
            Tree b = a.Parent;
            while ((b != null) && (a == b.Right))
            {
                a = b;
                b = b.Parent;
            }
            return b;
        }

        /// <summary>
        /// Recherche du noeud précedent
        /// </summary>
        /// <param name="a">Tree</param>
        /// <returns>Le neodu précédent</returns>
        public static Tree TreeSearchPredecessor(Tree a)
        {
            if (a == null)
            {
                return null;
            }
            if (a.Left != null)
            {
                return Tree.TreeMaximumValue(a.Left);
            }

            Tree y = a.Parent;
            while (y != null && a == y.Left)
            {
                a = y;
                y = y.Parent;
            }

            return y;
        }

        /// <summary>
        /// Recherche de la valeur minimum dans l'arbre binaire
        /// </summary>
        /// <param name="a">Tree</param>
        /// <returns>Tree</returns>
        public static Tree TreeMinimumValue(Tree a)
        {
            while (a.Left != null)
            {
                a = a.Left;
            }
            return a;
        }

        /// <summary>
        /// Recherche de la valeur maximum dans l'arbre binaire
        /// </summary>
        /// <param name="a">Tree</param>
        /// <returns>Tree</returns>
        public static Tree TreeMaximumValue(Tree a)
        {
            while (a.Right != null)
            {
                a = a.Right;
            }
            return a;
        }

        /// <summary>
        /// Calcul de l'affichage du Tree
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="isTail"></param>
        /// <param name="sb"></param>
        /// <returns>L'affichage du Tree construit</returns>
        public StringBuilder toString(StringBuilder prefix, bool isTail, StringBuilder sb)
        {
            if (Right != null)
            {
                Right.toString(new StringBuilder().Append(prefix).Append(isTail ? "│   " : "    "), false, sb);
            }
            sb.Append(prefix).Append(isTail ? "└── " : "┌── ").Append(Value.ToString()).Append("\n");
            if (Left != null)
            {
                Left.toString(new StringBuilder().Append(prefix).Append(isTail ? "    " : "│   "), true, sb);
            }
            return sb;
        }

        /// <summary>
        /// Affiche le Tree
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return this.toString(new StringBuilder(), true, new StringBuilder()).ToString();
        }
    }
}
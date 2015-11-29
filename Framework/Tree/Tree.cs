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
    public class Tree
    {
        public int Value { get; set; }

        public Tree Left { get; set; }

        public Tree Right { get; set; }

        public Tree Parent { get; set; }

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
        ///Constructeur avec simple valeur
        /// </summary>
        /// <param name="val"></param>
        public Tree(int val)
        {
            this.Value = val;
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
        public void ParcoursInfixe()
        {
            if (Left != null)
            {
                Left.ParcoursInfixe();
            }
            Console.Error.WriteLine(Value);
            if (Right != null)
            {
                Right.ParcoursInfixe();
            }
        }

        /// <summary>
        /// Parcours
        /// La racine sera affiché avant le coté gauche puis le coté droit
        /// </summary>
        public void ParcoursPrefixe()
        {
            Console.Error.WriteLine(Value);
            if (Left != null)
                Left.ParcoursPrefixe();
            if (Right != null)
                Right.ParcoursPrefixe();
        }

        /// <summary>
        /// Parcours
        ///  la racine sera affiché aprés les valeurs des sous arbres coté gauche puis coté droit
        /// </summary>
        public void ParcoursPostfixe()
        {
            if (Left != null)
                Left.ParcoursPostfixe();
            if (Right != null)
                Right.ParcoursPostfixe();
            Console.Error.WriteLine(Value);
        }

        /// <summary>
        /// Recherche de valeur dans l'arbre
        /// </summary>
        /// <param name="value">valeur</param>
        /// <returns>booléen</returns>
        public bool Recherche(int value)
        {
            if (value == Value)
                return true;
            if ((value < Value) && (Left != null))
                return (Left.Recherche(value));
            if ((value > Value) && (Right != null))
                return (Right.Recherche(value));
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

        public static void Transplante(Tree Depart, Tree u, Tree v)
        {
            if (u.Parent == null)
            {
                Depart = v;
            }
            else if (u == u.Parent.Left)
            {
                u.Parent.Left = v;
            }
            else
            {
                u.Parent.Right = v;
            }
            if (v != null)
            {
                v.Parent = u.Parent;
            }
        }

        public static void SupprimerNode(Tree depart, Tree z)
        {
            if (z.Left == null)
            {
                Tree.Transplante(depart, z, z.Right);
            }
            else if (z.Right == null)
            {
                Tree.Transplante(depart, z, z.Left);
            }
            else
            {
                Tree y = Tree.TreeMinimumValue(z.Right);
                if (y.Parent != z)
                {
                    Tree.Transplante(depart, y, y.Right);
                    y.Right = z.Right;
                    y.Right.Parent = y;
                }
                Tree.Transplante(depart, z, y);
                y.Left = z.Left;
                y.Left.Parent = y;
            }
        }

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
        /// Vérifie si le Tree a est le même que le Tree b
        /// </summary>
        /// <param name="a">Tree a</param>
        /// <param name="b">Tree b</param>
        /// <returns>booléen</returns>
        public static bool ArbresEgaux(Tree a, Tree b)
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
            return (ArbresEgaux(a.Left, b.Left)
                && ArbresEgaux(a.Right, b.Right));
        }

        /// <summary>
        /// hauteur de larbre
        /// </summary>
        /// <param name="a">Arbre a</param>
        /// <returns></returns>
        public static int Hauteur(Tree a)
        {
            if (a == null)
                return 0;
            else
                return (1 + Math.Max(Hauteur(a.Left), Hauteur(a.Right)));
        }

        /// <summary>
        /// vérifie si le Tree a est un arbre binaire
        /// </summary>
        /// <param name="a">Tree a</param>
        /// <returns>booléen</returns>
        public static bool EstUnArbreBinaire(Tree a)
        {
            if (a == null)
                return true;
            if ((a.Left != null) && (a.Left.Value > a.Value))
                return false;
            if ((a.Right != null) && (a.Value > a.Right.Value))
                return false;
            return (EstUnArbreBinaire(a.Left) && EstUnArbreBinaire(a.Right));
        }

        /// <summary>
        ///Recherche du noeud successeur
        /// </summary>
        /// <param name="a">Tree</param>
        /// <returns>Le noeud qui succéde le noeud entrant</returns>
        public static Tree TreeSuccesor(Tree a)
        {
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

        //public void ParcoursLargeur()
        //{
        //    File file = new File();
        //    file.ajouter(this);

        //    Arbre a, b;
        //    while (!file.estVide())
        //    {
        //        a = (Arbre)file.retirer();
        //        System.out.println(a.getValeur());
        //        b = a.getSousArbreGauche();
        //        if (b != null)
        //            file.ajouter(b);
        //        b = a.getSousArbreDroit();
        //        if (b != null)
        //            file.ajouter(b);
        //    }
        //}

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
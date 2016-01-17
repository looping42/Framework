using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.BTree.BTreeV2
{
    public class BTreeV2<T> where T : IComparable
    {
        private int Degree; // order of tree

        public BtreeNodeV2<T> root;  //every tree has at least a root node

        // ---------------------------------------------------------
        // here is the constructor for tree                        |
        // ---------------------------------------------------------

        public BTreeV2(int order)
        {
            this.Degree = order;

            root = new BtreeNodeV2<T>(order, null);
        }

        public void nonfullInsert(BtreeNodeV2<T> x, T key)
        {
            int i = x.NumberKeyInNode; //i is number of keys in node x

            if (x.leaf)
            {
                while (i >= 1 && key.CompareTo(x.key[i - 1]) > 0)//here find spot to put key.
                {
                    x.key[i] = x.key[i - 1];//shift values to make room

                    i--;//decrement
                }

                x.key[i] = key;//finally assign value to node
                x.NumberKeyInNode++; //increment count of keys in this node now.
            }
            else
            {
                int j = 0;
                while (j < x.NumberKeyInNode && key.CompareTo(x.key[j]) > 0)//find spot to recurse
                {                        //on correct child
                    j++;
                }

                //	i++;

                if (x.child[j].NumberKeyInNode == Degree * 2 - 1)
                {
                    split(x, j, x.child[j]);//call split on node x's ith child

                    if (key.CompareTo(x.key[j]) > 0)
                    {
                        j++;
                    }
                }

                nonfullInsert(x.child[j], key);//recurse
            }
        }

        //  --------------------------------------------------------
        //  this will be the split method.  It will split node we  |
        //  want to insert into if it is full.                     |
        //  --------------------------------------------------------

        public void split(BtreeNodeV2<T> x, int i, BtreeNodeV2<T> y)
        {
            //création nouveau noeud pour le split
            BtreeNodeV2<T> z = new BtreeNodeV2<T>(Degree, null);
            z.leaf = y.leaf;//set boolean to same as y
            z.NumberKeyInNode = Degree - 1;//this is updated size

            for (int j = 0; j < Degree - 1; j++)
            {
                z.key[j] = y.key[j + Degree]; //copy end of y into front of z
            }
            if (!y.leaf)//if not leaf we have to reassign child nodes.
            {
                for (int k = 0; k < Degree; k++)
                {
                    z.child[k] = y.child[k + Degree]; //reassing child of y
                }
            }

            y.NumberKeyInNode = Degree - 1; //new size of y

            for (int j = x.NumberKeyInNode; j > i; j--)//if we push key into x we have
            {                //to rearrange child nodes
                x.child[j + 1] = x.child[j]; //shift children of x
            }
            x.child[i + 1] = z; //reassign i+1 child of x

            for (int j = x.NumberKeyInNode; j > i; j--)
            {
                x.key[j + 1] = x.key[j]; // shift keys
            }
            x.key[i] = y.key[Degree - 1];//finally push value up into root.

            y.key[Degree - 1] = default(T); //erase value where we pushed from

            for (int j = 0; j < Degree - 1; j++)
            {
                y.key[j + Degree] = default(T); //'delete' old values
            }

            x.NumberKeyInNode++;  //increase count of keys in x
        }

        //--------------------------------------------------------------
        //this will be the method to insert in general, it will call    |
        //insert non full if needed.                                    |
        //--------------------------------------------------------------

        /// <summary>
        /// Insére une clé dans un arbre
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="key"></param>
        public void insert(BTreeV2<T> tree, T key)
        {
            BtreeNodeV2<T> r = tree.root;//this method finds the node to be inserted as
                                         //it goes through this starting at root node.
            if (r.NumberKeyInNode == 2 * Degree - 1)//if is full
            {
                BtreeNodeV2<T> s = new BtreeNodeV2<T>(Degree, null);//new node

                tree.root = s;    //\
                                  // \
                s.leaf = false;//  \
                               //   > this is to initialize node.
                s.NumberKeyInNode = 0;   //  /
                                         // /
                s.child[0] = r;///

                split(s, 0, r);//split root

                nonfullInsert(s, key); //call insert method
            }
            else
                nonfullInsert(r, key);//if its not full just insert it
        }

        //--------------------------------------------------------------
        //this method will delete a key value from the leaf node it is |
        //in.  We will use the search method to traverse through the   |
        //tree to find the node where the key is in.  We will then     |
        //iterated through key[] array until we get to node and will   |
        //assign k[i] = k[i+1] overwriting key we want to delete and   |
        //keeping blank spots out as well.  Note that this is the most |
        //simple case of delete that there is and we will not have time|
        //to implement all cases properly.                             |
        //--------------------------------------------------------------

        public void deleteKey(BTreeV2<T> t, T key)
        {
            BtreeNodeV2<T> temp = new BtreeNodeV2<T>(Degree, null);//temp Bnode

            temp = search(t.root, key);//call of search method on tree for key

            if (temp.leaf && temp.NumberKeyInNode > Degree - 1)
            {
                int i = 0;

                while (key.CompareTo(temp.getValue(i)) > 0)
                {
                    i++;
                }
                for (int j = i; j < 2 * Degree - 2; j++)
                {
                    temp.key[j] = temp.getValue(j + 1);
                }
                temp.NumberKeyInNode--;
            }
            else
            {
                Console.WriteLine("This node is either not a leaf or has less than order - 1 keys.");
            }
        }

        // ---------------------------------------------------------------------------------
        // this will be method to print out a node, or recurses when root node is not leaf |
        // ---------------------------------------------------------------------------------

        public void print(BtreeNodeV2<T> n)
        {
            for (int i = 0; i < n.NumberKeyInNode; i++)
            {
                Console.Write("key " + n.getValue(i) + "|");//this part prints root node
            }

            if (!n.leaf)//this is called when root is not leaf;
            {
                for (int j = 0; j <= n.NumberKeyInNode; j++)//in this loop we recurse
                {                 //to print out tree in
                    if (n.getChild(j) != null) //preorder fashion.
                    {             //going from left most
                                  //child to right most
                        Console.WriteLine();
                        print(n.getChild(j));     //child.
                    }
                }
            }
        }

        // Function to traverse all nodes in a subtree rooted with this node
        public void traverse(BtreeNodeV2<T> n)
        {
            // There are n keys and n+1 children, travers through n keys
            // and first n children
            int i;
            for (i = 0; i < n.NumberKeyInNode; i++)
            {
                // If this is not leaf, then before printing key[i],
                // traverse the subtree rooted with child C[i].
                if (n.leaf == false)
                    traverse(n.getChild(i));
                Console.Write(n.key[i] + "|");
            }

            // Print the subtree rooted with last child
            if (n.leaf == false)
            {
                Console.WriteLine();
                traverse(n.getChild(i));
            }
        }

        // --------------------------------------------------------
        // this will be method to search for a given node where   |
        // we want to insert a key value. this method is called   |
        // from SearchnPrintNode.  It returns a node with key     |
        // value in it                                            |
        // --------------------------------------------------------

        /// <summary>
        /// recherche la clé en entrée , le noeud est la racine
        /// </summary>
        /// <param name="node">noeud racine</param>
        /// <param name="key">clé à chercher</param>
        /// <returns>le noeud concerné</returns>
        public BtreeNodeV2<T> search(BtreeNodeV2<T> node, T key)
        {
            int i = 0;//we always want to start searching the 0th index of node.

            while (i < node.NumberKeyInNode && key.CompareTo(node.key[i]) > 0)
            {
                i++;
            }
            if (i <= node.NumberKeyInNode && key.CompareTo(node.key[i]) == 0)//obviously if key is in node we went to return node.
            {
                return node;
            }
            if (node.leaf)//since we've already checked root if it is leaf we don't have anything else to check
            {
                return null;
            }
            else//else if it is not leave recurse down through ith child
            {
                return search(node.getChild(i), key);
            }
        }

        /// <summary>
        /// Affiche le noeud trouvé dans l'arbre si la clé existe
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="key"></param>
        public void SearchPrintNode(BTreeV2<T> tree, T key)
        {
            BtreeNodeV2<T> temp = new BtreeNodeV2<T>(Degree, null);

            temp = search(tree.root, key);

            if (temp == null)
            {
                Console.WriteLine("The Key does not exist in this tree");
            }
            else
            {
                print(temp);
            }
        }
    }
}
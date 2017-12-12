using Framework.ShortestPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tree
{
    public class TreeRedBlack : NodeForTree
    {
        /// <summary>
        /// noeud racine de l'arbre
        /// </summary>
        private NodeForTree root;

        private void LeftRotate(NodeForTree X)
        {
            NodeForTree Y = X.right; // set Y
            X.right = Y.left;//turn Y's left subtree into X's right subtree
            if (Y.left != null)
            {
                Y.left.parent = X;
            }
            if (Y != null)
            {
                Y.parent = X.parent;//link X's parent to Y
            }
            if (X.parent == null)
            {
                root = Y;
            }
            if (X == X.parent.left)
            {
                X.parent.left = Y;
            }
            else
            {
                X.parent.right = Y;
            }
            Y.left = X; //put X on Y's left
            if (X != null)
            {
                X.parent = Y;
            }
        }

        private void RightRotate(NodeForTree Y)
        {
            // right rotate is simply mirror code from left rotate
            NodeForTree X = Y.left;
            Y.left = X.right;
            if (X.right != null)
            {
                X.right.parent = Y;
            }
            if (X != null)
            {
                X.parent = Y.parent;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            if (Y == Y.parent.right)
            {
                Y.parent.right = X;
            }
            if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }

            X.right = Y;//put Y on X's right
            if (Y != null)
            {
                Y.parent = X;
            }
        }

        /// <summary>
        /// Display Tree
        /// </summary>
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Nothing in the tree!");
                return;
            }
            if (root != null)
            {
                InOrderDisplay(root);
            }
        }

        private void InOrderDisplay(NodeForTree current)
        {
            if (current != null)
            {
                InOrderDisplay(current.left);
                Console.Write("({0}) ", current.Value);
                InOrderDisplay(current.right);
            }
        }

        public NodeForTree Find(int key)
        {
            bool isFound = false;
            NodeForTree temp = root;
            NodeForTree item = null;
            while (!isFound)
            {
                if (temp == null)
                {
                    break;
                }
                if (key < temp.Value)
                {
                    temp = temp.left;
                }
                if (key > temp.Value)
                {
                    temp = temp.right;
                }
                if (key == temp.Value)
                {
                    isFound = true;
                    item = temp;
                }
            }
            if (isFound)
            {
                Console.WriteLine("{0} was found", key);
                return temp;
            }
            else
            {
                Console.WriteLine("{0} not found", key);
                return null;
            }
        }

        public void Insert(int item)
        {
            NodeForTree newItem = new NodeForTree(item);
            if (root == null)
            {
                root = newItem;
                root.colour = RedOrBlack.Black;
                return;
            }
            NodeForTree Y = null;
            NodeForTree X = root;
            while (X != null)
            {
                Y = X;
                if (newItem.Value < X.Value)
                {
                    X = X.left;
                }
                else
                {
                    X = X.right;
                }
            }
            newItem.parent = Y;
            if (Y == null)
            {
                root = newItem;
            }
            else if (newItem.Value < Y.Value)
            {
                Y.left = newItem;
            }
            else
            {
                Y.right = newItem;
            }
            newItem.left = null;
            newItem.right = null;
            newItem.colour = RedOrBlack.Red;//colour the new node red
            InsertFixUp(newItem);//call method to check for violations and fix
        }

        private void InsertFixUp(NodeForTree item)
        {
            //Checks Red-Black Tree properties
            while (item != root && item.parent.colour == RedOrBlack.Red)
            {
                /*We have a violation*/
                if (item.parent == item.parent.parent.left)
                {
                    NodeForTree Y = item.parent.parent.right;
                    if (Y != null && Y.colour == RedOrBlack.Red)//Case 1: uncle is red
                    {
                        item.parent.colour = RedOrBlack.Black;
                        Y.colour = RedOrBlack.Black;
                        item.parent.parent.colour = RedOrBlack.Red;
                        item = item.parent.parent;
                    }
                    else //Case 2: uncle is black
                    {
                        if (item == item.parent.right)
                        {
                            item = item.parent;
                            LeftRotate(item);
                        }
                        //Case 3: recolour & rotate
                        item.parent.colour = RedOrBlack.Black;
                        item.parent.parent.colour = RedOrBlack.Red;
                        RightRotate(item.parent.parent);
                    }
                }
                else
                {
                    //mirror image of code above
                    NodeForTree X = null;

                    X = item.parent.parent.left;
                    if (X != null && X.colour == RedOrBlack.Black)//Case 1
                    {
                        item.parent.colour = RedOrBlack.Red;
                        X.colour = RedOrBlack.Red;
                        item.parent.parent.colour = RedOrBlack.Black;
                        item = item.parent.parent;
                    }
                    else //Case 2
                    {
                        if (item == item.parent.left)
                        {
                            item = item.parent;
                            RightRotate(item);
                        }
                        //Case 3: recolour & rotate
                        item.parent.colour = RedOrBlack.Black;
                        item.parent.parent.colour = RedOrBlack.Red;
                        LeftRotate(item.parent.parent);
                    }
                }
                root.colour = RedOrBlack.Black;//re-colour the root black as necessary
            }
        }

        public void Delete(int key)
        {
            //first find the node in the tree to delete and assign to item pointer/reference
            NodeForTree item = Find(key);
            NodeForTree X = null;
            NodeForTree Y = null;

            if (item == null)
            {
                Console.WriteLine("Nothing to delete!");
                return;
            }
            if (item.left == null || item.right == null)
            {
                Y = item;
            }
            else
            {
                Y = TreeSuccessor(item);
            }
            if (Y.left != null)
            {
                X = Y.left;
            }
            else
            {
                X = Y.right;
            }
            if (X != null)
            {
                X.parent = Y;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            else if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }
            else
            {
                Y.parent.left = X;
            }
            if (Y != item)
            {
                item.Value = Y.Value;
            }
            if (Y.colour == RedOrBlack.Black)
            {
                DeleteFixUp(X);
            }
        }

        private void DeleteFixUp(NodeForTree X)
        {
            while (X != null && X != root && X.colour == RedOrBlack.Black)
            {
                if (X == X.parent.left)
                {
                    NodeForTree W = X.parent.right;
                    if (W.colour == RedOrBlack.Red)
                    {
                        W.colour = RedOrBlack.Black; //case 1
                        X.parent.colour = RedOrBlack.Red; //case 1
                        LeftRotate(X.parent); //case 1
                        W = X.parent.right; //case 1
                    }
                    if (W.left.colour == RedOrBlack.Black && W.right.colour == RedOrBlack.Black)
                    {
                        W.colour = RedOrBlack.Red; //case 2
                        X = X.parent; //case 2
                    }
                    else if (W.right.colour == RedOrBlack.Black)
                    {
                        W.left.colour = RedOrBlack.Black; //case 3
                        W.colour = RedOrBlack.Red; //case 3
                        RightRotate(W); //case 3
                        W = X.parent.right; //case 3
                    }
                    W.colour = X.parent.colour; //case 4
                    X.parent.colour = RedOrBlack.Black; //case 4
                    W.right.colour = RedOrBlack.Black; //case 4
                    LeftRotate(X.parent); //case 4
                    X = root; //case 4
                }
                else //mirror code from above with "right" & "left" exchanged
                {
                    NodeForTree W = X.parent.left;
                    if (W.colour == RedOrBlack.Red)
                    {
                        W.colour = RedOrBlack.Black;
                        X.parent.colour = RedOrBlack.Red;
                        RightRotate(X.parent);
                        W = X.parent.left;
                    }
                    if (W.right.colour == RedOrBlack.Black && W.left.colour == RedOrBlack.Black)
                    {
                        W.colour = RedOrBlack.Black;
                        X = X.parent;
                    }
                    else if (W.left.colour == RedOrBlack.Black)
                    {
                        W.right.colour = RedOrBlack.Black;
                        W.colour = RedOrBlack.Red;
                        LeftRotate(W);
                        W = X.parent.left;
                    }
                    W.colour = X.parent.colour;
                    X.parent.colour = RedOrBlack.Black;
                    W.left.colour = RedOrBlack.Black;
                    RightRotate(X.parent);
                    X = root;
                }
            }
            if (X != null)
                X.colour = RedOrBlack.Black;
        }

        private NodeForTree Minimum(NodeForTree X)
        {
            while (X.left.left != null)
            {
                X = X.left;
            }
            if (X.left.right != null)
            {
                X = X.left.right;
            }
            return X;
        }

        private NodeForTree TreeSuccessor(NodeForTree X)
        {
            if (X.left != null)
            {
                return Minimum(X);
            }
            else
            {
                NodeForTree Y = X.parent;
                while (Y != null && X == Y.right)
                {
                    X = Y;
                    Y = Y.parent;
                }
                return Y;
            }
        }
    }
}
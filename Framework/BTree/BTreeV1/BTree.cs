using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.BTree
{
    public class BTree<TK, TP> where TK : IComparable<TK>
    {
        public BTree(int degree)
        {
            if (degree < 2)
            {
                throw new ArgumentException("BTree degree must be at least 2", "degree");
            }

            this.Root = new BtreeNode<TK, TP>(degree);
            this.Degree = degree;
            this.Height = 1;
        }

        public BtreeNode<TK, TP> Root { get; private set; }

        public int Degree { get; private set; }

        public int Height { get; private set; }

        /// <summary>
        /// Searches a key in the BTree, returning the entry with it and with the pointer.
        /// </summary>
        /// <param name="key">Key being searched.</param>
        /// <returns>Entry for that key, null otherwise.</returns>
        public Entry<TK, TP> Search(TK key)
        {
            return this.SearchInternal(this.Root, key);
        }

        /// <summary>
        /// Inserts a new key associated with a pointer in the BTree. This
        /// operation splits BtreeNodes as required to keep the BTree properties.
        /// </summary>
        /// <param name="newKey">Key to be inserted.</param>
        /// <param name="newPointer">Pointer to be associated with inserted key.</param>
        public void Insert(TK newKey, TP newPointer)
        {
            // there is space in the root BtreeNode
            if (!this.Root.HasReachedMaxEntries)
            {
                this.InsertNonFull(this.Root, newKey, newPointer);
                return;
            }

            // need to create new BtreeNode and have it split
            BtreeNode<TK, TP> oldRoot = this.Root;
            this.Root = new BtreeNode<TK, TP>(this.Degree);
            this.Root.Children.Add(oldRoot);
            this.SplitChild(this.Root, 0, oldRoot);
            this.InsertNonFull(this.Root, newKey, newPointer);

            this.Height++;
        }

        /// <summary>
        /// Deletes a key from the BTree. This operations moves keys and BtreeNodes
        /// as required to keep the BTree properties.
        /// </summary>
        /// <param name="keyToDelete">Key to be deleted.</param>
        public void Delete(TK keyToDelete)
        {
            this.DeleteInternal(this.Root, keyToDelete);

            // if root's last entry was moved to a child BtreeNode, remove it
            if (this.Root.Entries.Count == 0 && !this.Root.IsLeaf)
            {
                this.Root = this.Root.Children.Single();
                this.Height--;
            }
        }

        /// <summary>
        /// Internal method to delete keys from the BTree
        /// </summary>
        /// <param name="BtreeNode">BtreeNode to use to start search for the key.</param>
        /// <param name="keyToDelete">Key to be deleted.</param>
        private void DeleteInternal(BtreeNode<TK, TP> BtreeNode, TK keyToDelete)
        {
            int i = BtreeNode.Entries.TakeWhile(entry => keyToDelete.CompareTo(entry.Key) > 0).Count();

            // found key in BtreeNode, so delete if from it
            if (i < BtreeNode.Entries.Count && BtreeNode.Entries[i].Key.CompareTo(keyToDelete) == 0)
            {
                this.DeleteKeyFromBtreeNode(BtreeNode, keyToDelete, i);
                return;
            }

            // delete key from subtree
            if (!BtreeNode.IsLeaf)
            {
                this.DeleteKeyFromSubtree(BtreeNode, keyToDelete, i);
            }
        }

        /// <summary>
        /// Helper method that deletes a key from a subtree.
        /// </summary>
        /// <param name="parentBtreeNode">Parent BtreeNode used to start search for the key.</param>
        /// <param name="keyToDelete">Key to be deleted.</param>
        /// <param name="subtreeIndexInBtreeNode">Index of subtree BtreeNode in the parent BtreeNode.</param>
        private void DeleteKeyFromSubtree(BtreeNode<TK, TP> parentBtreeNode, TK keyToDelete, int subtreeIndexInBtreeNode)
        {
            BtreeNode<TK, TP> childBtreeNode = parentBtreeNode.Children[subtreeIndexInBtreeNode];

            // BtreeNode has reached min # of entries, and removing any from it will break the btree property,
            // so this block makes sure that the "child" has at least "degree" # of BtreeNodes by moving an
            // entry from a sibling BtreeNode or merging BtreeNodes
            if (childBtreeNode.HasReachedMinEntries)
            {
                int leftIndex = subtreeIndexInBtreeNode - 1;
                BtreeNode<TK, TP> leftSibling = subtreeIndexInBtreeNode > 0 ? parentBtreeNode.Children[leftIndex] : null;

                int rightIndex = subtreeIndexInBtreeNode + 1;
                BtreeNode<TK, TP> rightSibling = subtreeIndexInBtreeNode < parentBtreeNode.Children.Count - 1
                                                ? parentBtreeNode.Children[rightIndex]
                                                : null;

                if (leftSibling != null && leftSibling.Entries.Count > this.Degree - 1)
                {
                    // left sibling has a BtreeNode to spare, so this moves one BtreeNode from left sibling
                    // into parent's BtreeNode and one BtreeNode from parent into this current BtreeNode ("child")
                    childBtreeNode.Entries.Insert(0, parentBtreeNode.Entries[subtreeIndexInBtreeNode]);
                    parentBtreeNode.Entries[subtreeIndexInBtreeNode] = leftSibling.Entries.Last();
                    leftSibling.Entries.RemoveAt(leftSibling.Entries.Count - 1);

                    if (!leftSibling.IsLeaf)
                    {
                        childBtreeNode.Children.Insert(0, leftSibling.Children.Last());
                        leftSibling.Children.RemoveAt(leftSibling.Children.Count - 1);
                    }
                }
                else if (rightSibling != null && rightSibling.Entries.Count > this.Degree - 1)
                {
                    // right sibling has a BtreeNode to spare, so this moves one BtreeNode from right sibling
                    // into parent's BtreeNode and one BtreeNode from parent into this current BtreeNode ("child")
                    childBtreeNode.Entries.Add(parentBtreeNode.Entries[subtreeIndexInBtreeNode]);
                    parentBtreeNode.Entries[subtreeIndexInBtreeNode] = rightSibling.Entries.First();
                    rightSibling.Entries.RemoveAt(0);

                    if (!rightSibling.IsLeaf)
                    {
                        childBtreeNode.Children.Add(rightSibling.Children.First());
                        rightSibling.Children.RemoveAt(0);
                    }
                }
                else
                {
                    // this block merges either left or right sibling into the current BtreeNode "child"
                    if (leftSibling != null)
                    {
                        childBtreeNode.Entries.Insert(0, parentBtreeNode.Entries[subtreeIndexInBtreeNode]);
                        var oldEntries = childBtreeNode.Entries;
                        childBtreeNode.Entries = leftSibling.Entries;
                        childBtreeNode.Entries.AddRange(oldEntries);
                        if (!leftSibling.IsLeaf)
                        {
                            var oldChildren = childBtreeNode.Children;
                            childBtreeNode.Children = leftSibling.Children;
                            childBtreeNode.Children.AddRange(oldChildren);
                        }

                        parentBtreeNode.Children.RemoveAt(leftIndex);
                        parentBtreeNode.Entries.RemoveAt(subtreeIndexInBtreeNode);
                    }
                    else
                    {
                        Debug.Assert(rightSibling != null, "BtreeNode should have at least one sibling");
                        childBtreeNode.Entries.Add(parentBtreeNode.Entries[subtreeIndexInBtreeNode]);
                        childBtreeNode.Entries.AddRange(rightSibling.Entries);
                        if (!rightSibling.IsLeaf)
                        {
                            childBtreeNode.Children.AddRange(rightSibling.Children);
                        }

                        parentBtreeNode.Children.RemoveAt(rightIndex);
                        parentBtreeNode.Entries.RemoveAt(subtreeIndexInBtreeNode);
                    }
                }
            }

            // at this point, we know that "child" has at least "degree" BtreeNodes, so we can
            // move on - this guarantees that if any BtreeNode needs to be removed from it to
            // guarantee BTree's property, we will be fine with that
            this.DeleteInternal(childBtreeNode, keyToDelete);
        }

        /// <summary>
        /// Helper method that deletes key from a BtreeNode that contains it, be this
        /// BtreeNode a leaf BtreeNode or an internal BtreeNode.
        /// </summary>
        /// <param name="BtreeNode">BtreeNode that contains the key.</param>
        /// <param name="keyToDelete">Key to be deleted.</param>
        /// <param name="keyIndexInBtreeNode">Index of key within the BtreeNode.</param>
        private void DeleteKeyFromBtreeNode(BtreeNode<TK, TP> BtreeNode, TK keyToDelete, int keyIndexInBtreeNode)
        {
            // if leaf, just remove it from the list of entries (we're guaranteed to have
            // at least "degree" # of entries, to BTree property is maintained
            if (BtreeNode.IsLeaf)
            {
                BtreeNode.Entries.RemoveAt(keyIndexInBtreeNode);
                return;
            }

            BtreeNode<TK, TP> predecessorChild = BtreeNode.Children[keyIndexInBtreeNode];
            if (predecessorChild.Entries.Count >= this.Degree)
            {
                Entry<TK, TP> predecessor = this.DeletePredecessor(predecessorChild);
                BtreeNode.Entries[keyIndexInBtreeNode] = predecessor;
            }
            else
            {
                BtreeNode<TK, TP> successorChild = BtreeNode.Children[keyIndexInBtreeNode + 1];
                if (successorChild.Entries.Count >= this.Degree)
                {
                    Entry<TK, TP> successor = this.DeleteSuccessor(predecessorChild);
                    BtreeNode.Entries[keyIndexInBtreeNode] = successor;
                }
                else
                {
                    predecessorChild.Entries.Add(BtreeNode.Entries[keyIndexInBtreeNode]);
                    predecessorChild.Entries.AddRange(successorChild.Entries);
                    predecessorChild.Children.AddRange(successorChild.Children);

                    BtreeNode.Entries.RemoveAt(keyIndexInBtreeNode);
                    BtreeNode.Children.RemoveAt(keyIndexInBtreeNode + 1);

                    this.DeleteInternal(predecessorChild, keyToDelete);
                }
            }
        }

        /// <summary>
        /// Helper method that deletes a predecessor key (i.e. rightmost key) for a given BtreeNode.
        /// </summary>
        /// <param name="BtreeNode">BtreeNode for which the predecessor will be deleted.</param>
        /// <returns>Predecessor entry that got deleted.</returns>
        private Entry<TK, TP> DeletePredecessor(BtreeNode<TK, TP> BtreeNode)
        {
            if (BtreeNode.IsLeaf)
            {
                var result = BtreeNode.Entries[BtreeNode.Entries.Count - 1];
                BtreeNode.Entries.RemoveAt(BtreeNode.Entries.Count - 1);
                return result;
            }

            return this.DeletePredecessor(BtreeNode.Children.Last());
        }

        /// <summary>
        /// Helper method that deletes a successor key (i.e. leftmost key) for a given BtreeNode.
        /// </summary>
        /// <param name="BtreeNode">BtreeNode for which the successor will be deleted.</param>
        /// <returns>Successor entry that got deleted.</returns>
        private Entry<TK, TP> DeleteSuccessor(BtreeNode<TK, TP> BtreeNode)
        {
            if (BtreeNode.IsLeaf)
            {
                var result = BtreeNode.Entries[0];
                BtreeNode.Entries.RemoveAt(0);
                return result;
            }

            return this.DeletePredecessor(BtreeNode.Children.First());
        }

        /// <summary>
        /// Helper method that search for a key in a given BTree.
        /// </summary>
        /// <param name="BtreeNode">BtreeNode used to start the search.</param>
        /// <param name="key">Key to be searched.</param>
        /// <returns>Entry object with key information if found, null otherwise.</returns>
        private Entry<TK, TP> SearchInternal(BtreeNode<TK, TP> BtreeNode, TK key)
        {
            int i = BtreeNode.Entries.TakeWhile(entry => key.CompareTo(entry.Key) > 0).Count();

            if (i < BtreeNode.Entries.Count && BtreeNode.Entries[i].Key.CompareTo(key) == 0)
            {
                return BtreeNode.Entries[i];
            }

            return BtreeNode.IsLeaf ? null : this.SearchInternal(BtreeNode.Children[i], key);
        }

        /// <summary>
        /// Helper method that splits a full BtreeNode into two BtreeNodes.
        /// </summary>
        /// <param name="parentBtreeNode">Parent BtreeNode that contains BtreeNode to be split.</param>
        /// <param name="BtreeNodeToBeSplitIndex">Index of the BtreeNode to be split within parent.</param>
        /// <param name="BtreeNodeToBeSplit">BtreeNode to be split.</param>
        private void SplitChild(BtreeNode<TK, TP> parentBtreeNode, int BtreeNodeToBeSplitIndex, BtreeNode<TK, TP> BtreeNodeToBeSplit)
        {
            var newBtreeNode = new BtreeNode<TK, TP>(this.Degree);

            parentBtreeNode.Entries.Insert(BtreeNodeToBeSplitIndex, BtreeNodeToBeSplit.Entries[this.Degree - 1]);
            parentBtreeNode.Children.Insert(BtreeNodeToBeSplitIndex + 1, newBtreeNode);

            newBtreeNode.Entries.AddRange(BtreeNodeToBeSplit.Entries.GetRange(this.Degree, this.Degree - 1));

            // remove also Entries[this.Degree - 1], which is the one to move up to the parent
            BtreeNodeToBeSplit.Entries.RemoveRange(this.Degree - 1, this.Degree);

            if (!BtreeNodeToBeSplit.IsLeaf)
            {
                newBtreeNode.Children.AddRange(BtreeNodeToBeSplit.Children.GetRange(this.Degree, this.Degree));
                BtreeNodeToBeSplit.Children.RemoveRange(this.Degree, this.Degree);
            }
        }

        private void InsertNonFull(BtreeNode<TK, TP> BtreeNode, TK newKey, TP newPointer)
        {
            int positionToInsert = BtreeNode.Entries.TakeWhile(entry => newKey.CompareTo(entry.Key) >= 0).Count();

            // leaf BtreeNode
            if (BtreeNode.IsLeaf)
            {
                BtreeNode.Entries.Insert(positionToInsert, new Entry<TK, TP>() { Key = newKey, Pointer = newPointer });
                return;
            }

            // non-leaf
            BtreeNode<TK, TP> child = BtreeNode.Children[positionToInsert];
            if (child.HasReachedMaxEntries)
            {
                this.SplitChild(BtreeNode, positionToInsert, child);
                if (newKey.CompareTo(BtreeNode.Entries[positionToInsert].Key) > 0)
                {
                    positionToInsert++;
                }
            }

            this.InsertNonFull(BtreeNode.Children[positionToInsert], newKey, newPointer);
        }
    }
}
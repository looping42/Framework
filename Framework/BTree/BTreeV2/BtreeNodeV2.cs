using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.BTree.BTreeV2
{
    public class BtreeNodeV2<T>
    {
        public int t { get; set; }//variable to determine order of tree
        public BtreeNodeV2<T> parent { get; set; }

        public T[] key { get; set; }

        public BtreeNodeV2<T>[] child { get; set; }
        public bool leaf { get; set; }

        public int NumberKeyInNode { get; set; }

        // ----------------------------------------------------
        // initial value constructor for new node             |
        // will be called from BTree.java                     |
        // ----------------------------------------------------

        public BtreeNodeV2(int t, BtreeNodeV2<T> parent)
        {
            this.t = t;  //assign size
            this.parent = parent; //assign parent
            key = new T[2 * t - 1]; // array of proper size
            child = new BtreeNodeV2<T>[2 * t]; // array of refs proper size
            leaf = true; // everynode is leaf at first;
            NumberKeyInNode = 0; //until we add keys later.
        }

        // -----------------------------------------------------
        // this is method to return key value at index position|
        // -----------------------------------------------------

        public T getValue(int index)
        {
            return key[index];
        }

        // ----------------------------------------------------
        // this is method to get ith child of node            |
        // ----------------------------------------------------

        public BtreeNodeV2<T> getChild(int index)
        {
            return child[index];
        }
    }
}
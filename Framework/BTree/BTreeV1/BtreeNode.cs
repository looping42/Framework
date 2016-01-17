using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.BTree
{
    public class Entry<TK, TP> : IEquatable<Entry<TK, TP>>
    {
        public TK Key { get; set; }

        public TP Pointer { get; set; }

        public bool Equals(Entry<TK, TP> other)
        {
            return this.Key.Equals(other.Key) && this.Pointer.Equals(other.Pointer);
        }
    }

    public class BtreeNode<TK, TP>
    {
        private int degree;

        public BtreeNode(int degree)
        {
            this.degree = degree;
            this.Children = new List<BtreeNode<TK, TP>>(degree);
            this.Entries = new List<Entry<TK, TP>>(degree);
        }

        public List<BtreeNode<TK, TP>> Children { get; set; }

        public List<Entry<TK, TP>> Entries { get; set; }

        public bool IsLeaf
        {
            get
            {
                return this.Children.Count == 0;
            }
        }

        public bool HasReachedMaxEntries
        {
            get
            {
                return this.Entries.Count == (2 * this.degree) - 1;
            }
        }

        public bool HasReachedMinEntries
        {
            get
            {
                return this.Entries.Count == this.degree - 1;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Graph.Kruskal
{
    public class KruskalNode
    {
        public int Parent { get; set; }
        public int Value { get; set; }

        public KruskalNode(int parent, int value)
        {
            this.Parent = parent;
            this.Value = value;
        }

        public KruskalNode()
        {
        }
    }
}
using Framework.Tree;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ShortestPath
{
    public enum RedOrBlack
    {
        Red,
        Black
    }

    public class NodeForTree
    {
        public RedOrBlack colour { get; set; }

        public NodeForTree left { get; set; }
        public NodeForTree right { get; set; }
        public NodeForTree parent { get; set; }
        public int Value { get; set; }

        public NodeForTree()
        {
        }

        public NodeForTree(int data)
        {
            this.Value = data;
        }

        public NodeForTree(RedOrBlack colour)
        {
            this.colour = colour;
        }

        public NodeForTree(int data, RedOrBlack colour)
        {
            this.Value = data;
            this.colour = colour;
        }
    }
}
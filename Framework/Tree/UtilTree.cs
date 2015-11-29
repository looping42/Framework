using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tree
{
    public enum RedOrBlack
    {
        Red,
        Black
    }

    public class UtilTree
    {
        public int Value { get; set; }

        public Tree Left { get; set; }

        public Tree Right { get; set; }

        public Tree Parent { get; set; }

        public RedOrBlack Couleur { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ShortestPath
{
    public class Edge
    {
        public Node A { get; set; }
        public Node B { get; set; }
        public int Weight { get; set; }
    }
}
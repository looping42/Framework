using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Graph
{
    public class GraphNode
    {
        public int Value { get; set; }

        public string Value2 { get; set; }

        public Coloration Color { get; set; }

        public int DatationStart { get; set; }

        public int DatationEnd { get; set; }

        public GraphNode ParentNode { get; set; }

        public GraphNode(string val)
        {
            Value2 = val;
        }

        public GraphNode()
        {
        }
    }
}
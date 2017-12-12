using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Graph.Prim
{

    public interface IMinTree
    {
        List<Framework.Graph.Prim.MinimumSpanningTree.MstEdge> findMinTree();
    }

}

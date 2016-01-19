using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Graph
{
    public enum Coloration { white, gray, black };

    public class Graph
    {
        public bool IsDirectGraph { get; set; }

        //private members
        private HashSet<GraphNode> _vertexes;

        private Dictionary<GraphNode, LinkedList<GraphEdge>> _VertexEdgeMapping;

        //Constructor
        public Graph(bool isDirect)
        {
            IsDirectGraph = isDirect;
            _vertexes = new HashSet<GraphNode>();
            _VertexEdgeMapping = new Dictionary<GraphNode, LinkedList<GraphEdge>>();
        }

        public bool AddVertex(GraphNode vertex)
        {
            try
            {
                _vertexes.Add(vertex);
                _VertexEdgeMapping.Add(vertex, new LinkedList<GraphEdge>());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Add vertex failed! {0}", e.Message);
                return false;
            }
        }

        public bool AddEdge(GraphNode from, GraphNode to, int weight)
        {
            try
            {
                GraphEdge newEdge = new GraphEdge(from, to, weight);
                _VertexEdgeMapping[from].AddLast(newEdge);
                if (IsDirectGraph == false)
                {
                    GraphEdge backEdge = new GraphEdge(to, from, weight);
                    _VertexEdgeMapping[to].AddLast(backEdge);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Add edge failed! {0}", e.Message);
                return false;
            }
        }

        public bool BreadthFirstSearch(GraphNode rootVertex)
        {
            try
            {
                Console.WriteLine("******* Breadth First Search  ********");

                Dictionary<GraphNode, string> color = new Dictionary<GraphNode, string>();
                Dictionary<GraphNode, GraphNode> parent = new Dictionary<GraphNode, GraphNode>();

                foreach (GraphNode vertex in _vertexes)
                {
                    color.Add(vertex, Coloration.white.ToString());
                    parent.Add(vertex, null);
                }

                color[rootVertex] = Coloration.gray.ToString();

                Queue<GraphNode> queue = new Queue<GraphNode>();
                queue.Enqueue(rootVertex);

                while (queue.Count != 0)
                {
                    GraphNode temp = queue.Dequeue();
                    foreach (GraphEdge edge in _VertexEdgeMapping[temp])
                    {
                        if (color[edge.ToVertex] == Coloration.white.ToString())
                        {
                            color[edge.ToVertex] = Coloration.gray.ToString();
                            parent[edge.ToVertex] = temp;
                            queue.Enqueue(edge.ToVertex);
                        }
                    }
                    color[temp] = Coloration.black.ToString();
                    Console.WriteLine("Vertex {0} has been found!", temp.Value2);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Breadth First Search failed! {0}", e.Message);
                return false;
            }
        }

        public bool DepthSearchFirst()
        {
            try
            {
                Console.WriteLine("******* Depth First Search  ********");

                Dictionary<GraphNode, string> color = new Dictionary<GraphNode, string>();
                Dictionary<GraphNode, GraphNode> parent = new Dictionary<GraphNode, GraphNode>();
                foreach (GraphNode vertex in _vertexes)
                {
                    color.Add(vertex, Coloration.white.ToString());
                    parent.Add(vertex, null);
                }

                foreach (GraphNode vertex in _vertexes)
                {
                    if (color[vertex] == Coloration.white.ToString())
                    {
                        DFS_Visit(vertex, color, parent);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Depth search first failed! {0}", e.Message);
                return false;
            }
        }

        private bool DFS_Visit(GraphNode vertex, Dictionary<GraphNode, string> color, Dictionary<GraphNode, GraphNode> parent)
        {
            try
            {
                color[vertex] = Coloration.gray.ToString();
                foreach (GraphEdge edge in _VertexEdgeMapping[vertex])
                {
                    if (color[edge.ToVertex] == Coloration.white.ToString())
                    {
                        parent[edge.ToVertex] = vertex;
                        DFS_Visit(edge.ToVertex, color, parent);
                    }
                }

                color[vertex] = Coloration.black.ToString();
                Console.WriteLine("Vertex {0} has been found!", vertex.Value2);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("DFS_Visit failed! {0}", e.Message);
                return false;
            }
        }
    }
}
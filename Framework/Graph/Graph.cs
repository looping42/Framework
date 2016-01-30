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
        /// <summary>
        /// Poids lambda qui va servir à connaître la distance entre le noeud de départ et les autres noeuds
        /// </summary>
        private int _datationToGive;

        /// <summary>
        /// Si les liens du graph sont à sens unique true sinon false
        /// </summary>
        public bool IsSensUniqLinkEdge { get; set; }

        /// <summary>
        /// membres privée noeud du graph
        /// </summary>
        private HashSet<GraphNode> _nodes;

        /// <summary>
        /// Dicctionnaire contenant les noeuds et leurs liens
        /// </summary>
        private Dictionary<GraphNode, LinkedList<GraphEdge>> nodesKeyEdgeValues;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="isDirect">Si le graphe a des liens en double sens </param>
        public Graph(bool isDirect)
        {
            IsSensUniqLinkEdge = isDirect;
            _nodes = new HashSet<GraphNode>();
            nodesKeyEdgeValues = new Dictionary<GraphNode, LinkedList<GraphEdge>>();
        }

        /// <summary>
        /// Ajout des noeuds
        /// </summary>
        /// <param name="node">noeud à ajouter</param>
        public void AddNode(GraphNode node)
        {
            _nodes.Add(node);
            nodesKeyEdgeValues.Add(node, new LinkedList<GraphEdge>());
        }

        /// <summary>
        /// Ajout liens
        /// </summary>
        /// <param name="from">noeud de départ</param>
        /// <param name="to">noeud d'arrivée</param>
        /// <param name="weight">poids entre les 2 noeuds</param>
        public void AddEdge(GraphNode from, GraphNode to, int weight)
        {
            GraphEdge newEdge = new GraphEdge(from, to, weight);
            nodesKeyEdgeValues[from].AddLast(newEdge);
            if (IsSensUniqLinkEdge == false)
            {
                GraphEdge backEdge = new GraphEdge(to, from, weight);
                nodesKeyEdgeValues[to].AddLast(backEdge);
            }
        }

        /// <summary>
        /// Pacrours en profondeur
        /// </summary>
        /// <returns>liste des noeuds du parcours en profondeur</returns>
        public List<GraphNode> DepthSearchFirst()
        {
            List<GraphNode> result = new List<GraphNode>();

            foreach (GraphNode node in _nodes)
            {
                node.Color = Coloration.white;
                node.ParentNode = null;
            }
            _datationToGive = 0;
            foreach (GraphNode node in _nodes)
            {
                if (node.Color == Coloration.white)
                {
                    DFS_Visit(node, result);
                }
            }
            return result;
        }

        /// <summary>
        /// Parcours des noeuds en profondeurs
        /// </summary>
        /// <param name="node">noeud courant</param>
        /// <param name="result">liste des noeud pour le résultat</param>
        private void DFS_Visit(GraphNode node, List<GraphNode> result)
        {
            _datationToGive = _datationToGive + 1;
            node.DatationStart = _datationToGive;
            node.Color = Coloration.gray;

            foreach (GraphEdge edge in nodesKeyEdgeValues[node])//explore l'arc
            {
                if (edge.ToVertex.Color == Coloration.white)
                {
                    edge.ToVertex.ParentNode = node;
                    DFS_Visit(edge.ToVertex, result);
                }
            }
            //noircit le noeud courant car on en a finit avec lui
            node.Color = Coloration.black;
            _datationToGive = _datationToGive + 1;
            node.DatationEnd = _datationToGive;
            result.Add(node);
        }

        /// <summary>
        /// Parcours en largeur
        /// </summary>
        /// <param name="rootNode">noeud racine</param>
        /// <returns>Liste des noeud du parcours en largeur</returns>
        public List<GraphNode> BreadthFirstSearch(GraphNode rootNode)
        {
            List<GraphNode> result = new List<GraphNode>();

            foreach (GraphNode node in _nodes)
            {
                node.Color = Coloration.white;
                node.DatationStart = int.MaxValue;
                node.ParentNode = null;
            }
            rootNode.Color = Coloration.gray;
            rootNode.ParentNode = null;
            rootNode.DatationStart = 0;

            Queue<GraphNode> queue = new Queue<GraphNode>();
            queue.Enqueue(rootNode);

            while (queue.Count != 0)
            {
                GraphNode nodeTampon = queue.Dequeue();
                foreach (GraphEdge edge in nodesKeyEdgeValues[nodeTampon])
                {
                    if (edge.ToVertex.Color == Coloration.white)
                    {
                        edge.ToVertex.Color = Coloration.gray;
                        edge.ToVertex.ParentNode = nodeTampon;
                        edge.ToVertex.DatationStart = nodeTampon.DatationStart + 1;
                        queue.Enqueue(edge.ToVertex);
                    }
                }
                nodeTampon.Color = Coloration.black;
                result.Add(nodeTampon);
            }

            return result;
        }
    }
}
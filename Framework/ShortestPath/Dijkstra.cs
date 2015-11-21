using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ShortestPath
{
    public class Dijkstra
    {
        //Noeud de départ
        public int posNodeStart { get; set; }

        public List<Edge> edges { get; set; }

        public List<Node> nodes { get; set; }

        public List<Node> nodeTampon { get; set; }

        /// <summary>
        /// Initialisation Bellman-Ford
        /// </summary>
        public void initialize()
        {
            foreach (Node n in nodes)
            {
                n.Value = int.MaxValue;
                n.parent = null;
            }
            nodes[posNodeStart].Value = 0;
        }

        /// <summary>
        /// Relâche Les noeuds ,si la valeur du Noeud 2 est supérieur à la valeur du Noeud 1 + (valeur noeud1 +noued2 )
        /// affecte cette valeur au noeud 2 et le noeud 1 devient son parent
        /// </summary>
        /// <param name="n1">Noeud 1</param>
        /// <param name="n2">Noeud 2</param>
        private void relax(Node n1, Node n2)
        {
            if (n2.Value > (n1.Value + getWeight(n1, n2)))
            {
                n2.Value = n1.Value + getWeight(n1, n2);
                n2.parent = n1;
            }
        }

        /// <summary>
        ///retourne le poid entre 2 noeuds
        /// </summary>
        /// <param name="n1">Noeud 1</param>
        /// <param name="n2">Noeud 2</param>
        /// <returns>poids entre les 2 noeuds</returns>
        public int getWeight(Node n1, Node n2)
        {
            int value = 0;
            foreach (Edge edge in edges)
            {
                if (edge.A == n1 && edge.B == n2)
                {
                    value = edge.Weight;
                }
                else if (edge.B == n1 && edge.A == n2)
                {
                    value = edge.Weight;
                }
            }
            return value;
        }

        public Node ExtractMin()
        {
            double min = double.PositiveInfinity;
            Node toRemove = new Node();

            foreach (Node node in nodeTampon)
            {
                if (node.Value < min)
                {
                    min = node.Value;
                    toRemove = node;
                }
            }
            return toRemove;
        }

        /// <summary>
        /// trouve les noeuds voisins
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public List<Node> getNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            foreach (Edge e in edges)
            {
                if (e.A.Equals(node))
                {
                    neighbors.Add(e.B);
                }
                else if (e.B.Equals(node))
                {
                    neighbors.Add(e.A);
                }
            }
            return neighbors;
        }

        public void DijkstraWork()
        {
            initialize();
            nodeTampon = new List<Node>();
            nodeTampon.AddRange(nodes);

            while (nodeTampon.Count > 0)
            {
                Node smallest = ExtractMin();
                nodeTampon.Remove(smallest);

                //var temp = getNeighbors(smallest);

                foreach (Node edgeNeighbour in getNeighbors(smallest))
                {
                    relax(smallest, edgeNeighbour);
                }
            }
        }
    }
}
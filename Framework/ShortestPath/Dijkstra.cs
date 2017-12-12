using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ShortestPath
{
    /// <summary>
    /// Algorithme de Dijkstra
    /// Trouve le plus court chemin pour chaque noeud en partant du noeud de départ(int)
    /// la liste des noeud contenant le plus court chemin se trouve dans la List<Node> nodes en scannant les noeuds parents
    /// </summary>
    public class Dijkstra : UtilShortestPath
    {
        public Node ExtractMin()
        {
            double min = double.PositiveInfinity;
            Node toRemove = new Node();

            foreach (Node node in NodeTampon)
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
        public List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            foreach (Edge e in Edges)
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

        /// <summary>
        /// retourne le poid entre 2 noeuds
        /// </summary>
        /// <param name="n1">Noeud 1</param>
        /// <param name="n2">Noeud 2</param>
        /// <returns>Poids entre les 2 noeuds</returns>
        public int GetWeightDijkstra(Node n1, Node n2)
        {
            int value = 0;
            foreach (Edge edge in Edges)
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

        /// <summary>
        /// Relâche Les noeuds ,si la valeur du Noeud 2 est supérieur à la valeur du Noeud 1 + (valeur noeud1 +noued2 )
        /// affecte cette valeur au noeud 2 et le noeud 1 devient son parent
        /// </summary>
        /// <param name="n1">Noeud 1</param>
        /// <param name="n2">Noeud 2</param>
        public void RelaxDijkstra(Node n1, Node n2)
        {
            if (n2.Value > (n1.Value + GetWeightDijkstra(n1, n2)))
            {
                n2.Value = n1.Value + GetWeightDijkstra(n1, n2);
                n2.parent = n1;
            }
        }

        /// <summary>
        /// Algo Dijkstra
        /// </summary>
        public void DijkstraWork()
        {
            Initialize();
            NodeTampon = new List<Node>();
            NodeTampon.AddRange(Nodes);

            while (NodeTampon.Count > 0)
            {
                Node smallest = ExtractMin();
                NodeTampon.Remove(smallest);

                foreach (Node edgeNeighbour in GetNeighbors(smallest))
                {
                    RelaxDijkstra(smallest, edgeNeighbour);
                }
            }
        }
    }
}
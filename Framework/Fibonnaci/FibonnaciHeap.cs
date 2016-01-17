using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Fibonnaci
{
    public class FibonnaciHeap<T>
    {
        private static readonly double _oneOverLogPhi = 1.0 / Math.Log((1.0 + Math.Sqrt(5.0)) / 2.0);
        private FibHeapNode<T> _minNode;
        private int _nNodes;

        /// <summary>
        /// Si l'arbe est vide renvoie un booléen
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return _minNode == null;
        }

        /// <summary>
        /// Vide l'arbre
        /// </summary>
        public void Clear()
        {
            _minNode = null;
            _nNodes = 0;
        }

        /// <summary>
        /// change la valeur d'uné clé d'un noeud
        /// </summary>
        /// <param name="node">noeud courant</param>
        /// <param name="newValue"></param>
        public void DecreaseKey(FibHeapNode<T> node, double newValue)
        {
            if (newValue > node.Key)
            {
                throw new ArgumentException("decreaseKey() got larger key value");
            }

            node.Key = newValue;

            FibHeapNode<T> noeudTampon = node.Parent;

            if ((noeudTampon != null) && (node.Key < noeudTampon.Key))
            {
                Cut(node, noeudTampon);
                CascadingCut(noeudTampon);
            }

            if (node.Key < _minNode.Key)
            {
                _minNode = node;
            }
        }

        /// <summary>
        /// Efface la valeur d'un noeud
        /// </summary>
        /// <param name="node"></param>
        public void Delete(FibHeapNode<T> node)
        {
            // fais du noeud parent le noeud avec la valeur la plus petite possible
            DecreaseKey(node, Double.NegativeInfinity);

            // remove le noeud le plus petit
            ExtractMin();
        }

        /// <summary>
        /// insére un nouveau noeud avec sa clé
        /// </summary>
        /// <param name="node">Noeud courant</param>
        /// <param name="key">clé à insérer</param>
        public void Insert(FibHeapNode<T> node)
        {
            //Si l'arbre n'est pas vide
            if (_minNode != null)
            {
                node.Left = _minNode;
                node.Right = _minNode.Right;
                _minNode.Right = node;
                node.Right.Left = node;

                if (node.Key < _minNode.Key)
                {
                    _minNode = node;
                }
            }
            else
            {
                _minNode = node;
            }

            _nNodes++;
        }

        /// <summary>
        /// Renvoie le plus petit noeud de l'arbre
        /// </summary>
        /// <returns>noeud le plus petit</returns>
        public FibHeapNode<T> Min()
        {
            return _minNode;
        }

        /// <summary>
        ///extrait(remove) le noeud le plus petit de l'arbre
        /// </summary>
        /// <returns>renvoie le noeud qui vient dêtre retirer</returns>
        public FibHeapNode<T> ExtractMin()
        {
            FibHeapNode<T> minNode = _minNode;

            if (minNode != null)
            {
                int numKids = minNode.Degree;
                FibHeapNode<T> oldMinChild = minNode.Child;

                // Pour chaque enfant du noeud oldMinChild
                while (numKids > 0)
                {
                    FibHeapNode<T> tempRight = oldMinChild.Right;

                    // Retire oldMinChild de la  child list
                    oldMinChild.Left.Right = oldMinChild.Right;
                    oldMinChild.Right.Left = oldMinChild.Left;

                    // Ajout de oldMinChild à la racine de la liste
                    oldMinChild.Left = _minNode;
                    oldMinChild.Right = _minNode.Right;
                    _minNode.Right = oldMinChild;
                    oldMinChild.Right.Left = oldMinChild;

                    // Met la valeur parent[oldMinChild] à null
                    oldMinChild.Parent = null;
                    oldMinChild = tempRight;
                    numKids--;
                }

                // Retire minNode de la racine de la liste
                minNode.Left.Right = minNode.Right;
                minNode.Right.Left = minNode.Left;

                if (minNode == minNode.Right)
                {
                    _minNode = null;
                }
                else
                {
                    _minNode = minNode.Right;
                    Consolidate();
                }

                // Diminue la valeur globale de la liste
                _nNodes--;
            }

            return minNode;
        }

        /// <summary>
        /// le nombre de noeud dans l'arbre
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return _nNodes;
        }

        /// <summary>
        /// jointure de 2 tas de fibonnacci
        /// </summary>
        /// <param name="h1">fibonnaci 1</param>
        /// <param name="h2"></param>
        /// <returns></returns>
        public static FibonnaciHeap<T> Union(FibonnaciHeap<T> h1, FibonnaciHeap<T> h2)
        {
            var h = new FibonnaciHeap<T>();

            if ((h1 != null) && (h2 != null))
            {
                h._minNode = h1._minNode;

                if (h._minNode != null)
                {
                    if (h2._minNode != null)
                    {
                        h._minNode.Right.Left = h2._minNode.Left;
                        h2._minNode.Left.Right = h._minNode.Right;
                        h._minNode.Right = h2._minNode;
                        h2._minNode.Left = h._minNode;

                        if (h2._minNode.Key < h1._minNode.Key)
                        {
                            h._minNode = h2._minNode;
                        }
                    }
                }
                else
                {
                    h._minNode = h2._minNode;
                }

                h._nNodes = h1._nNodes + h2._nNodes;
            }

            return h;
        }

        /// <summary>
        /// Coupe le lien entre noeudcourant et son nom parent ; ce qui fais de lui un noeud parent
        /// </summary>
        /// <param name="noeudCourant"></param>
        protected void CascadingCut(FibHeapNode<T> noeudCourant)
        {
            FibHeapNode<T> noeudparent = noeudCourant.Parent;

            // si il ya un noeud parent
            if (noeudparent != null)
            {
                // si le noeud enfant n'est pas marqué
                if (!noeudCourant.Mark)
                {
                    noeudCourant.Mark = true;
                }
                else
                {
                    // si il est marqué on le coupe de son noeud parent
                    Cut(noeudCourant, noeudparent);

                    // cut its parent as well
                    CascadingCut(noeudparent);
                }
            }
        }

        protected void Consolidate()
        {
            int arraySize = ((int)Math.Floor(Math.Log(_nNodes) * _oneOverLogPhi)) + 1;

            var tabFiboNode = new List<FibHeapNode<T>>(arraySize);

            // Initialize degree array
            for (var i = 0; i < arraySize; i++)
            {
                tabFiboNode.Add(null);
            }

            // Find the number of root nodes.
            var numRoots = 0;
            FibHeapNode<T> x = _minNode;

            if (x != null)
            {
                numRoots++;
                x = x.Right;

                while (x != _minNode)
                {
                    numRoots++;
                    x = x.Right;
                }
            }

            // For each node in root list do...
            while (numRoots > 0)
            {
                // Access this node's degree..
                int d = x.Degree;
                FibHeapNode<T> next = x.Right;

                // ..and see if there's another of the same degree.
                for (;;)
                {
                    FibHeapNode<T> y = tabFiboNode[d];
                    if (y == null)
                    {
                        // Nope.
                        break;
                    }

                    // There is, make one of the nodes a child of the other.
                    // Do this based on the key value.
                    if (x.Key > y.Key)
                    {
                        FibHeapNode<T> temp = y;
                        y = x;
                        x = temp;
                    }

                    // FibHeapNode<T> newChild disappears from root list.
                    Link(y, x);

                    // We've handled this degree, go to next one.
                    tabFiboNode[d] = null;
                    d++;
                }

                // Save this node for later when we might encounter another
                // of the same degree.
                tabFiboNode[d] = x;

                // Move forward through list.
                x = next;
                numRoots--;
            }

            // Set min to null (effectively losing the root list) and
            // reconstruct the root list from the array entries in array[].
            _minNode = null;

            for (var i = 0; i < arraySize; i++)
            {
                FibHeapNode<T> y = tabFiboNode[i];
                if (y == null)
                {
                    continue;
                }

                // We've got a live one, add it to root list.
                if (_minNode != null)
                {
                    // First remove node from root list.
                    y.Left.Right = y.Right;
                    y.Right.Left = y.Left;

                    // Now add to root list, again.
                    y.Left = _minNode;
                    y.Right = _minNode.Right;
                    _minNode.Right = y;
                    y.Right.Left = y;

                    // Check if this is a new min.
                    if (y.Key < _minNode.Key)
                    {
                        _minNode = y;
                    }
                }
                else
                {
                    _minNode = y;
                }
            }
        }

        /// <summary>
        /// l'inverse de l'operation link , retire le noeud enfant de la liste des noeuds enfants
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected void Cut(FibHeapNode<T> x, FibHeapNode<T> y)
        {
            // remove newParent from childlist of newChild and decrement degree[newChild]
            x.Left.Right = x.Right;
            x.Right.Left = x.Left;
            y.Degree--;

            // reset newChild.child if necessary
            if (y.Child == x)
            {
                y.Child = x.Right;
            }

            if (y.Degree == 0)
            {
                y.Child = null;
            }

            // add newParent to root list of heap
            x.Left = _minNode;
            x.Right = _minNode.Right;
            _minNode.Right = x;
            x.Right.Left = x;

            // set parent[newParent] to nil
            x.Parent = null;

            // set mark[newParent] to false
            x.Mark = false;
        }

        /// <summary>
        /// fais de newChild un enfant du noeud newParent
        /// </summary>
        /// <param name="newChild"></param>
        /// <param name="newParent"></param>
        protected void Link(FibHeapNode<T> newChild, FibHeapNode<T> newParent)
        {
            // remove newChild from root list of heap
            newChild.Left.Right = newChild.Right;
            newChild.Right.Left = newChild.Left;

            // make newChild a child of newParent
            newChild.Parent = newParent;

            if (newParent.Child == null)
            {
                newParent.Child = newChild;
                newChild.Right = newChild;
                newChild.Left = newChild;
            }
            else
            {
                newChild.Left = newParent.Child;
                newChild.Right = newParent.Child.Right;
                newParent.Child.Right = newChild;
                newChild.Right.Left = newChild;
            }

            // increase degree[newParent]
            newParent.Degree++;

            // set mark[newChild] false
            newChild.Mark = false;
        }

        /// <summary>
        /// Affichage de l'arbre
        /// </summary>
        public void display()
        {
            Console.WriteLine("\nHeap = ");
            FibHeapNode<T> ptr = _minNode;
            displayChild(ptr);
            if (ptr == null)
            {
                Console.WriteLine("Empty\n");
            }
            do
            {
                Console.WriteLine("Key : " + ptr.Key + " Data : " + ptr.Data);
                ptr = ptr.Right;
            } while (ptr != _minNode && ptr.Right != null);
            Console.WriteLine();
        }

        public void displayChild(FibHeapNode<T> newChild)
        {
            if (newChild.Child != null)
            {
                Console.WriteLine("Key : " + newChild.Child.Key + " Data : " + newChild.Child.Data);
                displayChild(newChild.Child);
            }
        }
    }
}
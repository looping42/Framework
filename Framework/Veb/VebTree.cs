using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Veb
{
    public class VebTree
    {
        /// <summary>
        /// Base du noeud (taille)
        /// </summary>
        private static int BASE_SIZE = 2;

        /// <summary>
        /// Valeur max et minimum
        /// </summary>
        private static int NULL = -1;

        private VebNode Root;

        /// <summary>
        /// Return la nouvelle instance de l'arbre
        /// </summary>
        /// <param name="universeSize">taille d'univers</param>
        /// <returns>instance static de l'arbre</returns>
        public static VebTree CreateVebTree(int universeSize)
        {
            if (IsPowerOf2(universeSize))
            {
                return new VebTree(universeSize);
            }
            else
            {
                Console.WriteLine("ERROR: Must create a tree with size a power of 2!");
                return null;
            }
        }

        /// <summary>
        /// insertiond ans l'arbre
        /// </summary>
        /// <param name="x">valeur type nombre</param>
        public void Insert(int x)
        {
            InsertR(Root, x);
        }

        /// <summary>
        /// Efface la valeur de l'arbre
        /// </summary>
        /// <param name="x">nombre</param>
        public void Delete(int x)
        {
            DeleteR(Root, x);
        }

        /// <summary>
        /// recherche la valeur dans l'arbre
        /// </summary>
        /// <param name="x">nombre</param>
        /// <returns>true si trouvé false sinon</returns>
        public bool Search(int x)
        {
            return SearchR(Root, x);
        }

        /// <summary>
        /// return le predeccesseur de la valeur
        /// </summary>
        /// <param name="x">nombre</param>
        /// <returns></returns>
        public int Predecessor(int x)
        {
            return PredecessorR(Root, x);
        }

        /// <summary>
        /// Renvoie le successeur de la valeur passé en paramétres
        /// </summary>
        /// <param name="x">x</param>
        /// <returns>la valeur suivante</returns>
        public int Successor(int x)
        {
            return SuccessorR(Root, x);
        }

        /// <summary>
        /// Valeur minimum dans larbre
        /// </summary>
        /// <returns></returns>
        public int Min()
        {
            return Root.min;
        }

        /// <summary>
        /// returne la valeur maximum dans l'arbre
        /// </summary>
        /// <returns>-1 sir larbre est vide</returns>
        public int Max()
        {
            return Root.max;
        }

        /// <summary>
        /// Crée l'arbre avec la taille d'univers en paramétres
        /// </summary>
        /// <param name="universeSize">taille d'univers</param>
        public VebTree(int universeSize)
        {
            /*
             * This node will handle creating all the other nodes,
             * and the full tree will be built.
             */
            Root = new VebNode(universeSize);
        }

        /// <summary>
        /// Insertion du noeud et de sa valeur
        /// </summary>
        /// <param name="node">noeud</param>
        /// <param name="x">valeur</param>
        private void InsertR(VebNode node, int x)
        {
            //si le noeud est vide
            if (NULL == node.min)
            {
                node.min = x;
                node.max = x;
            }
            if (x < node.min)
            {
                int tempValue = x;
                x = node.min;
                node.min = tempValue;
            }
            if (x > node.min && node.UniverseSize > BASE_SIZE)
            {
                int highOfX = High(node, x);
                int lowOfX = Low(node, x);

                //Cas ou le cluster ne soit pas vide
                if (NULL != node.cluster[highOfX].min)
                {
                    //Insertion recursive du cluster
                    InsertR(node.cluster[highOfX], lowOfX);
                }
                else
                {
                    //Insertion sommaire recursif
                    InsertR(node.Summary, highOfX);
                    node.cluster[highOfX].min = lowOfX;
                    node.cluster[highOfX].max = lowOfX;
                }
            }
            if (x > node.max)
            {
                node.max = x;
            }
        }

        /// <summary>
        /// Efface le noeud en parametre
        /// </summary>
        /// <param name="node"></param>
        /// <param name="x"></param>
        private void DeleteR(VebNode node, int x)
        {
            if (node.min == node.max)
            {
                node.min = NULL;
                node.max = NULL;
            }
            else if (BASE_SIZE == node.UniverseSize)
            {
                if (0 == x)
                {
                    node.min = 1;
                }
                else
                {
                    node.min = 0;
                }
                node.max = node.min;
            }
            else if (x == node.min)
            {
                int summaryMin = node.Summary.min;
                x = Index(node, summaryMin, node.cluster[summaryMin].min);
                node.min = x;

                int highOfX = High(node, x);
                int lowOfX = Low(node, x);
                DeleteR(node.cluster[highOfX], lowOfX);

                if (NULL == node.cluster[highOfX].min)
                {
                    DeleteR(node.Summary, highOfX);
                    if (x == node.max)
                    {
                        int summaryMax = node.Summary.max;
                        if (NULL == summaryMax)
                        {
                            node.max = node.min;
                        }
                        else
                        {
                            node.max = Index(node, summaryMax, node.cluster[summaryMax].max);
                        }
                    }
                }
                else if (x == node.max)
                {
                    node.max = Index(node, highOfX, node.cluster[highOfX].max);
                }
            }
        }

        /// <summary>
        /// recherche le noeud en parametres
        /// </summary>
        /// <param name="node">noeud </param>
        /// <param name="x">valeur</param>
        /// <returns>true si trouvé false sinon</returns>
        private bool SearchR(VebNode node, int x)
        {
            if (x == node.min || x == node.max)
            {
                return true;
            }
            else if (BASE_SIZE == node.UniverseSize)
            {
                return false;
            }
            else
            {
                return SearchR(node.cluster[High(node, x)], Low(node, x));
            }
        }

        /// <summary>
        /// recherche du noeud predécesseur
        /// </summary>
        /// <param name="node">noeud</param>
        /// <param name="x">sa valeur</param>
        /// <returns>valeur du noeud prédecesseur</returns>
        public int PredecessorR(VebNode node, int x)
        {
            if (BASE_SIZE == node.UniverseSize)
            {
                if ((1 == x) && (0 == node.min))
                {
                    return 0;
                }
                else
                {
                    return NULL;
                }
            }
            else if ((NULL != node.max) && (x > node.max))
            {
                return node.max;
            }
            else
            {
                int highOfX = High(node, x);
                int lowOfX = Low(node, x);

                int minCluster = node.cluster[highOfX].min;
                if ((NULL != minCluster) && (lowOfX > minCluster))
                {
                    return Index(node, highOfX, PredecessorR(node.cluster[highOfX], lowOfX));
                }
                else
                {
                    int clusterPred = PredecessorR(node.Summary, highOfX);
                    if (NULL == clusterPred)
                    {
                        if ((NULL != node.min) && (x > node.min))
                        {
                            return node.min;
                        }
                        else
                        {
                            return NULL;
                        }
                    }
                    else
                    {
                        return Index(node, clusterPred, node.cluster[clusterPred].max);
                    }
                }
            }
        }

        /// <summary>
        /// affiche le successeur de la valeur courante
        /// </summary>
        /// <param name="node">noeud racine</param>
        /// <param name="x">valeur </param>
        /// <returns>valeur suivante</returns>
        public int SuccessorR(VebNode node, int x)
        {
            if (BASE_SIZE == node.UniverseSize)
            {
                if ((0 == x) && (1 == node.max))
                {
                    return 1;
                }
                else
                {
                    return NULL;
                }
            }
            else if ((NULL != node.min) && (x < node.min))
            {
                return node.min;
            }
            else
            {
                int highOfX = High(node, x);
                int lowOfX = Low(node, x);
                //Sinon max faible = arbre web maximum
                int maxCluster = node.cluster[highOfX].max;

                if ((NULL != maxCluster) && (lowOfX < maxCluster))
                {
                    //return decalage avec méthode successeur
                    return Index(node, highOfX, SuccessorR(node.cluster[highOfX], lowOfX));
                }
                else
                {
                    //Succ partie = arbre veb successeur
                    int clustersuc = SuccessorR(node.Summary, highOfX);

                    if (NULL == clustersuc)
                    {
                        return NULL;
                    }
                    else
                    {
                        return Index(node, clustersuc, node.cluster[clustersuc].min);
                    }
                }
            }
        }

        /// <summary>
        /// Renvoie la valeur de la premiere moitier du bit de x
        /// </summary>
        /// <param name="node"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private int High(VebNode node, int x)
        {
            return (int)Math.Floor(x / LowerSquareRoot(node));
        }

        /// <summary>
        /// Renvoie la seconde moitier des bits de x
        /// </summary>
        /// <param name="node"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private int Low(VebNode node, int x)
        {
            return x % (int)LowerSquareRoot(node);
        }

        /// <summary>
        /// renvoie la valeur signifiant des bits de x
        /// </summary>
        /// <param name="node">noeud</param>
        /// <returns></returns>
        private double LowerSquareRoot(VebNode node)
        {
            return Math.Pow(2, Math.Floor((Math.Log10(node.UniverseSize) / Math.Log10(2)) / 2.0));
        }

        /// <summary>
        /// Retourne le noeud racine
        /// </summary>
        /// <param name="node">noeud courant</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int Index(VebNode node, int x, int y)
        {
            return (int)(x * LowerSquareRoot(node) + y);
        }

        /// <summary>
        /// Retorun true si la valeur est une puissance de 2 false sinon
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static bool IsPowerOf2(int x)
        {
            if (0 == x)
            {
                return false;
            }

            while (x % 2 == 0)
            {
                x = x / 2;
            }

            if (x > 1)
            {
                return false;
            }

            return true;
        }
    }
}
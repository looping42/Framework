using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Veb
{
    public class VebNode
    {
        public int UniverseSize { get; set; }
        public VebNode Summary { get; set; }
        public VebNode[] cluster { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="universeSize">taille d'univers</param>
        public VebNode(int universeSize)
        {
            this.UniverseSize = universeSize;
            min = VebTree.NULL;
            max = VebTree.NULL;

            /* Allocate the summary and cluster children. */
            initializeChildren(universeSize);
        }

        /// <summary>
        /// initialisation de l'enfant
        /// </summary>
        /// <param name="universeSize">taille de l'univers</param>
        private void initializeChildren(int universeSize)
        {
            if (universeSize <= VebTree.BASE_SIZE)
            {
                Summary = null;
                cluster = null;
            }
            else
            {
                int childUnivereSize = higherSquareRoot();

                Summary = new VebNode(childUnivereSize);
                cluster = new VebNode[childUnivereSize];

                for (int i = 0; i < childUnivereSize; i++)
                {
                    cluster[i] = new VebNode(childUnivereSize);
                }
            }
        }

        /// <summary>
        /// return la valeur du plus signifiant des bits de la taille d'univers
        /// </summary>
        /// <returns></returns>
        private int higherSquareRoot()
        {
            return (int)Math.Pow(2, Math.Ceiling((Math.Log10(UniverseSize) / Math.Log10(2)) / 2));
        }
    }
}

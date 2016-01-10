using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CutRod
{
    public class DynamicProg
    {
        /// <summary>
        /// Découpe de barre
        /// Calcul de la combinaison (champs) de valeurs maximal pour un tableau donnée d'une longueur n
        /// </summary>
        /// <param name="prix">tableau comprenant lec valeurs</param>
        /// <param name="n">taille des barres max</param>
        /// <returns></returns>
        public static int CutRod(int[] prix, int n)
        {
            int[] val = new int[n + 1];
            val[0] = 0;

            // Construire la table val [] en bas vers le haut de manière et retourner la dernière entrée
            // De la table
            for (int i = 1; i <= n; i++)
            {
                int max_val = int.MinValue;

                for (int j = 0; j < i; j++)
                {
                    max_val = Math.Max(max_val, prix[j] + val[i - j - 1]);
                }
                val[i] = max_val;
            }

            return val[n];
        }

        /// <summary>
        /// mutliplication des matrices
        /// </summary>
        /// <param name="a">matrice a</param>
        /// <param name="b">matrice b</param>
        /// <returns></returns>
        public static int[,] multipleMatrice(int[,] a, int[,] b)
        {
            int[,] c = new int[a.GetLength(0), b.GetLength(1)];
            if (a.GetLength(1) != b.GetLength(0))
            {
                Log.Logger.Info("\n Number of columns in First Matrix should be equal to Number of rows in Second Matrix.");
                Log.Logger.Info("\n Please re-enter correct dimensions.");
                return c;
            }
            else
            {
                for (int i = 0; i < c.GetLength(0); i++)
                {
                    for (int j = 0; j < c.GetLength(1); j++)
                    {
                        c[i, j] = 0;

                        for (int k = 0; k < a.GetLength(1); k++)
                        {
                            c[i, j] = c[i, j] + a[i, k] + b[k, j];
                        }
                    }
                }
                return c;
            }
        }
    }
}
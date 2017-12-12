using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CutRod
{
    public static class DynamicProg
    {
        /// <summary>
        /// Découpe de barre
        /// Calcul de la combinaison (champs) de valeurs maximal pour une taille de barre max donnée d'une longueur n
        /// </summary>
        /// <param name="n">taille des barres max</param>
        /// <param name="prix">tableau comprenant lec valeurs</param>
        /// <returns></returns>
        public static int CutRod(int n, int[] prix)
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
        public static int[,] MultipleMatrice(int[,] a, int[,] b)
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

        #region Algorithme gloutons

        /// <summary>
        /// Recherche du maximum de combinaison possible d'activité pour une seule personne
        /// une activité par période max : classé le tableau f en entére dans l'ordre alphabétique
        /// </summary>
        /// <param name="s">tableau comprenant le début des activités</param>
        /// <param name="f">tableau comprenant la fin des activités</param>
        /// <returns>la liste des activités choisis</returns>
        public static List<int> SelectMaxActivities(int[] s, int[] f)
        {
            //longueur tableau
            int n = f.Length;
            List<int> result = new List<int>();

            // la premiére activité est toujours choisis
            int i = 0;
            result.Add(i);
            // pour le reste
            for (int j = 1; j < n; j++)
            {
                //si l'activité a un départ plus grand ou égal au temps de fin de l'activité déjà préselectionnée
                //on l'a choisi
                if (s[j] >= f[i])
                {
                    result.Add(j);
                    i = j;
                }
            }
            return result;
        }

        /// <summary>
        /// Recherche du maximum de combinaison possible d'activité pour une seule personne
        /// une activité par période max
        /// </summary>
        /// <param name="s">tableau comprenant le début des activités</param>
        /// <param name="f">tableau comprenant la fin des activités</param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>string comprenant les activités choisis</returns>
        public static string SelectMaxActivitiesRecursive(int[] s, int[] f, int i, int j)
        {
            int m = i + 1;
            // Trouve premiére activité à finir
            while (m < j && s[m] < f[i])
            {
                m = m + 1;
            }
            if (m < j)
            {
                return m + " " + SelectMaxActivitiesRecursive(s, f, m, j);
            }
            else return "";
        }

        #endregion Algorithme gloutons
    }
}
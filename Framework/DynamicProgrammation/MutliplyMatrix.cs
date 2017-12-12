using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DynamicProgrammation
{
    public class MutliplyMatrix
    {
        protected int[,] s;
        protected int[,] m;

        /// <summary>
        /// multiplication des matrices dans l'ordre
        /// </summary>
        /// <param name="p">tableau</param>
        /// <param name="i">départ</param>
        /// <param name="j">arrivée</param>
        /// <returns></returns>
        public static int OrderChaineMatrix(int[] p, int i, int j)
        {
            if (i == j)
                return 0;
            int k;
            int min = int.MaxValue;
            int count;

            // place parenthesis at different places between first and last matrix,
            // recursively calculate count of multiplcations for each parenthesis
            // placement and return the minimum count
            for (k = i; k < j; k++)
            {
                count = OrderChaineMatrix(p, i, k) +
                        OrderChaineMatrix(p, k + 1, j) +
                        p[i - 1] * p[k] * p[j];

                if (count < min)
                    min = count;
            }

            // Return minimum count
            return min;
        }

        /// <summary>
        /// affiche la mutliplication des matrices
        /// </summary>
        /// <param name="s"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void printOptimalParenthesizations(int[,] s, int i, int j)
        {
            if (i == j)
            {
                Console.Write("A" + (i + 1) + " ");
            }
            else
            {
                Console.Write("(");
                printOptimalParenthesizations(s, i, s[i, j]);
                printOptimalParenthesizations(s, s[i, j] + 1, j);
                Console.Write(")");
            }
        }

        /// <summary>appel de l'affichage des matrices
        ///
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void printOptimalParenthesizationsCall(int i, int j)
        {
            for (int o = 0; o < m.GetLength(0); o++)
            {
                for (int p = 0; p < m.GetLength(1); p++)
                {
                    Console.Write(m[o, p] + ";");
                }
                Console.WriteLine();
            }

            printOptimalParenthesizations(s, i, j);
        }

        /// <summary>
        ///multiplication des matrices dans l'ordre
        /// </summary>
        /// <param name="p">tableau</param>
        public void matrixChainOrder(int[] p)
        {
            int n = p.Length - 1;
            m = new int[n, n];
            s = new int[n, n];

            for (int ii = 1; ii < n; ii++)
            {
                for (int i = 0; i < n - ii; i++)
                {
                    int j = i + ii;
                    m[i, j] = int.MaxValue;
                    for (int k = i; k < j; k++)
                    {
                        int q = m[i, k] + m[k + 1, j] + p[i] * p[k + 1] * p[j + 1];
                        if (q < m[i, j])
                        {
                            m[i, j] = q;
                            s[i, j] = k;
                        }
                    }
                }
            }
        }
    }
}
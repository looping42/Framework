using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.BackTrace
{
    public class KnightTourProblem
    {
        /* A utility function to check if i,j are valid indexes
           for N*N chessboard */

        private bool IsSafe(int x, int y, int[,] sol)
        {
            return (x >= 0 && x < sol.GetLongLength(0) && y >= 0 && y < sol.GetLongLength(1) && sol[x, y] == -1);
        }

        /* A utility function to print solution matrix sol[N][N] */

        private void PrintSolution(int[,] sol)
        {
            for (int x = 0; x < sol.GetLongLength(0); x++)
            {
                for (int y = 0; y < sol.GetLongLength(1); y++)
                {
                    Console.Error.Write(sol[x, y] + " ");
                }
                Console.Error.WriteLine('\n');
            }
        }

        /* This function solves the Knight Tour problem using
           Backtracking.  This function mainly uses solveKTUtil()
           to solve the problem. It returns false if no complete
           tour is possible, otherwise return true and prints the
           tour.
           Please note that there may be more than one solutions,
           this function prints one of the feasible solutions.  */

        public bool SolveKT()
        {
            int[,] sol = new int[8, 8];

            /* Initialization of solution matrix */
            for (int x = 0; x < sol.GetLongLength(0); x++)
            {
                for (int y = 0; y < sol.GetLongLength(1); y++)
                {
                    sol[x, y] = -1;
                }
            }

            /* xMove[] and yMove[] define next move of Knight.
               xMove[] is for next value of x coordinate
               yMove[] is for next value of y coordinate */
            int[] xMove = { 2, 1, -1, -2, -2, -1, 1, 2 };
            int[] yMove = { 1, 2, 2, 1, -1, -2, -2, -1 };

            // Since the Knight is initially at the first block
            sol[0, 0] = 0;

            /* Start from 0,0 and explore all tours using
               solveKTUtil() */
            if (SolveKTUtil(0, 0, 1, sol, xMove, yMove) == false)
            {
                Console.Error.WriteLine("Solution does not exist");

                return false;
            }
            else
                PrintSolution(sol);

            return true;
        }

        /* A recursive utility function to solve Knight Tour
           problem */

        private bool SolveKTUtil(int x, int y, int movei, int[,] sol, int[] xMove, int[] yMove)
        {
            int k, next_x, next_y;
            if (movei == sol.GetLongLength(0) * sol.GetLongLength(1))
                return true;

            /* Try all next moves from the current coordinate x, y */
            for (k = 0; k < 8; k++)
            {
                next_x = x + xMove[k];
                next_y = y + yMove[k];
                if (IsSafe(next_x, next_y, sol))
                {
                    sol[next_x, next_y] = movei;
                    if (SolveKTUtil(next_x, next_y, movei + 1, sol,
                                    xMove, yMove) == true)
                        return true;
                    else
                        sol[next_x, next_y] = -1;// backtracking
                }
            }

            return false;
        }
    }
}
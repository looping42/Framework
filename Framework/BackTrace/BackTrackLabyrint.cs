using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.BackTrace
{
    public static class BackTrackLabyrint
    {
        /* A utility function to check if x,y is valid index for N*N maze */

        private static bool IsSafe(int[,] maze, int x, int y)
        {
            // if (x,y outside maze) return false
            if (x >= 0 && x < maze.GetLength(0) && y >= 0 && y < maze.GetLength(1) && maze[x, y] == 1)
                return true;

            return false;
        }

        public static bool SolveMaze(int[,] maze, int x, int y, int[,] sol, string dir)
        {
            // if (x,y is goal) return true
            if (x == maze.GetLongLength(0) - 1 && y == maze.GetLongLength(1) - 1)
            {
                sol[x, y] = 1;
                return true;
            }

            // Check if maze[x][y] is valid
            if (IsSafe(maze, x, y) == true)
            {
                // mark x,y as part of solution path
                sol[x, y] = 1;

                /* Move forward in x direction */
                if ((dir != "up") && (SolveMaze(maze, x + 1, y, sol, "down") == true))
                    return true;

                /* If moving in x direction doesn't give solution then
                   Move down in y direction  */
                if ((dir != "left") && (SolveMaze(maze, x, y + 1, sol, "right") == true))
                    return true;

                /* Move forward in x -1 direction */
                if ((dir != "down") && (SolveMaze(maze, x - 1, y, sol, "up") == true))
                    return true;

                /* If moving in x direction doesn't give solution then
                   Move down in y -1 direction  */
                if ((dir != "right") && (SolveMaze(maze, x, y - 1, sol, "left") == true))
                    return true;

                /* If none of the above movements work then BACKTRACK:
                    unmark x,y as part of solution path */
                sol[x, y] = 0;
                return false;
            }

            return false;
        }

        public static void PrintSolution(int[,] sol)
        {
            for (int i = 0; i < sol.GetLongLength(0); i++)
            {
                for (int j = 0; j < sol.GetLongLength(1); j++)
                    Console.Error.Write(sol[i, j] + " ");

                Console.Error.WriteLine();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Geometry.Carre
{
    public static class Carre
    {
        /// <summary>
        /// recherche de la distance entre le point p et q
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        private static int DistSq(Point p, Point q)
        {
            return (p.X - q.X) * (p.X - q.X) +
                   (p.Y - q.Y) * (p.Y - q.Y);
        }

        /// <summary>
        /// Fonction qui retourne vrai si les différent point forme un carré
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        public static bool IsSquare(Point p1, Point p2, Point p3, Point p4)
        {
            int d2 = DistSq(p1, p2);  // from p1 to p2
            int d3 = DistSq(p1, p3);  // from p1 to p3
            int d4 = DistSq(p1, p4);  // from p1 to p4

            // If lengths if (p1, p2) and (p1, p3) are same, then
            // following conditions must met to form a square.
            // 1) Square of length of (p1, p4) is same as twice
            //    the square of (p1, p2)
            // 2) p4 is at same distance from p2 and p3
            if (d2 == d3 && 2 * d2 == d4)
            {
                int d = DistSq(p2, p4);
                return (d == DistSq(p3, p4) && d == d2);
            }

            // The below two cases are similar to above case
            if (d3 == d4 && 2 * d3 == d2)
            {
                int d = DistSq(p2, p3);
                return (d == DistSq(p2, p4) && d == d3);
            }
            if (d2 == d4 && 2 * d2 == d3)
            {
                int d = DistSq(p2, p3);
                return (d == DistSq(p3, p4) && d == d2);
            }

            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Geometry.IntersectSegment
{
    /// <summary>
    /// Vérifie si les lignes p1 et q1 et la ligne p2 et q2 se croise
    /// si c'est le cas renvoie true sinon false
    ///
    /// </summary>
    public static class IntersectSegment
    {
        public static bool DoIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            // Find the four orientations needed for general and
            // special cases
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);
            // General case
            if (o1 != o2 && o3 != o4)
                return true;
            // Special Cases
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1
            if (o1 == 0 && OnSegment(p1, p2, q1)) return true;
            // p1, q1 and p2 are colinear and q2 lies on segment p1q1
            if (o2 == 0 && OnSegment(p1, q2, q1)) return true;
            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (o3 == 0 && OnSegment(p2, p1, q2)) return true;
            // p2, q2 and q1 are colinear and q1 lies on segment p2q2
            if (o4 == 0 && OnSegment(p2, q1, q2)) return true;
            return false; // Doesn’t fall in any of the above cases
        }

        /// <summary>
        /// Trouve l'orientation necessaire pour atteindre le point r
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <returns>sens des aiguile d'une montre ou inversé ou colinéaire</returns>
        private static int Orientation(Point p, Point q, Point r)
        {
            // See 10th slides from following link for derivation of the formula
            // http://www.dcs.gla.ac.uk/~pat/52233/slides/Geometry1x1.pdf
            double val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0;  // colinear
            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        /// <summary>
        /// Vérifie si le point q se trouve sur la droite p-r
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <returns>booléen</returns>
        private static bool OnSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                     q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;
            return false;
        }

        /// <summary>
        /// recherche d'un point situé dans un polygone
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="n">Nombre de sommet minimum de 3</param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool IsInside(Point[] polygon, int n, Point p)
        {
            // There must be at least 3 vertices in polygon[]
            if (n < 3) return false;

            // Create a point for line segment from p to infinite
            Point extreme = new Point(int.MaxValue, p.Y);

            // Count intersections of the above line with sides of polygon
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;

                // Check if the line segment from 'p' to 'extreme' intersects
                // with the line segment from 'polygon[i]' to 'polygon[next]'
                if (DoIntersect(polygon[i], polygon[next], p, extreme))
                {
                    // If the point 'p' is colinear with line segment 'i-next',
                    // then check if it lies on segment. If it lies, return true,
                    // otherwise false
                    if (Orientation(polygon[i], p, polygon[next]) == 0)
                        return OnSegment(polygon[i], p, polygon[next]);

                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise
            return count % 2 == 1;  // Same as (count%2 == 1)
        }

        // Prints convex hull of a set of n points.
        /// <summary>
        /// Récupére la liste des points qui entoure tout les autres
        /// le point de départ se situe le plus à gauche
        /// </summary>
        /// <param name="points"></param>
        /// <param name="n">nomber de point en paramétres minimum de 3</param>
        public static List<Point> ConvexHullJarvis(Point[] points, int n)
        {
            List<Point> hull = new List<Point>();
            // There must be at least 3 points
            if (n < 3) return hull;

            // Recherche du point le plus à gauche
            int l = 0;
            for (int i = 1; i < n; i++)
                if (points[i].X < points[l].X)
                    l = i;

            // Start from leftmost point, keep moving counterclockwise
            // until reach the start point again.  This loop runs O(h)
            // times where h is number of points in result or output.
            int p = l, q;
            do
            {
                // Add current point to result
                hull.Add(points[p]);

                // Search for a point 'q' such that orientation(p, x,
                // q) is counterclockwise for all points 'x'. The idea
                // is to keep track of last visited most counterclock-
                // wise point in q. If any point 'i' is more counterclock-
                // wise than q, then update q.
                q = (p + 1) % n;
                for (int i = 0; i < n; i++)
                {
                    // If i is more counterclockwise than current q, then
                    // update q
                    if (Orientation(points[p], points[i], points[q]) == 2)
                        q = i;
                }

                // Now q is the most counterclockwise with respect to p
                // Set p as q for next iteration, so that q is added to
                // result 'hull'
                p = q;
            } while (p != l);  // While we don't come to first point

            return hull;
        }
    }
}
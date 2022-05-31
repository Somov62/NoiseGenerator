using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator2
{
    internal static class Tools
    {
       
        public static double LinearInterpolation(double node1, double node2, double pointCoord = 0.5)
        {
            //pointCoord = QunticCurve(pointCoord);
            //pointCoord = CubicCurve(pointCoord);
            return Math.Round(node1 + (node2 - node1) * pointCoord, 3);
        }

        public static double QunticCurve(double t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        public static double CubicCurve(double t)
        {
            return -2 * t * t * t + 3 * t * t;
        }

    }
}

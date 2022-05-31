using System;

namespace Generator1
{
    public class Tools
    {
        public static double LinearInterpolation(double node1, double node2, double pointCoord = 0.5)
        {
            pointCoord = QunticCurve(pointCoord);
            //pointCoord = CubicCurve(pointCoord);
            return node1 + (node2 - node1) * pointCoord;
        }

        private static double QunticCurve(double t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        private static double CubicCurve(double t)
        {
            return -2 * t * t * t + 3 * t * t;
        }

        public static (double, double) GetRandomVector()
        {
            Random rnd = new Random();
            return rnd.Next(4) switch
            {
                0 => (1, 0),
                1 => (-1, 0),
                2 => (0, 1),
                _ => (0, -1),
            };
        }

        public static double ScalarProduct((double, double) vector1, (double, double) vector2)
        {
            return vector1.Item1 * vector2.Item1 + vector1.Item2 * vector2.Item2;
        }
    }
}

using System;

namespace Noise
{
    public static class Generator
    {
        private static readonly int _width = 500;
        private static readonly int _height = 500;

        private static (double, double)[,] GenerateGrid()
        {

            (double, double)[,] grid = new (double, double)[_height + 1, _width + 1];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = GetRandomVector();
                }
            }
            return grid;
        }

        public static double[,] GenerateNoise()
        {
            Random rnd = new();

            var grid = GenerateGrid();

            double[,] noise = new double[_height, _width];

            for (int i = 0; i < noise.GetLength(0); i++)
            {
                for (int j = 0; j < noise.GetLength(1); j++)
                {
                    double pointCoordX = rnd.NextDouble();
                    double pointCoordY = rnd.NextDouble();

                    (double, double) pointVector = new(pointCoordX, pointCoordY);

                    double leftTop = ScalarProduct(pointVector, grid[i, j]);
                    double rightTop = ScalarProduct(pointVector, grid[i, j + 1]);
                    double leftBottom = ScalarProduct(pointVector, grid[i + 1, j]);
                    double rightBottom = ScalarProduct(pointVector, grid[i + 1, j + 1]);

                    double top = LinearInterpolation(leftTop, rightTop, pointCoordX);
                    double bottom = LinearInterpolation(leftBottom, rightBottom, pointCoordX);
                    noise[i, j] = Math.Round(LinearInterpolation(top, bottom, pointCoordY), 1);

                }
            }

            Smooth(noise, 80);

            ReduceColors(noise);

            return noise;
        }

        

        private static void Smooth(double[,] noise, int count)
        {
            for (int k = 1; k < count; k++)
            {
                for (int i = 0; i < noise.GetLength(0); i++)
                {
                    for (int j = 0; j < noise.GetLength(1); j++)
                    {
                        int iIndex = i;
                        int jIndex = j;
                        if (i + 1 == noise.GetLength(0)) iIndex = i - 1;
                        if (j + 1 == noise.GetLength(1)) jIndex = j - 1;
                        double nodeLeftTop = noise[i, j];
                        double nodeLeftBottom = noise[iIndex + 1, j];
                        double nodeRightTop = noise[i, jIndex + 1];
                        double nodeRightBottom = noise[iIndex + 1, jIndex + 1];

                        double lerpTop = LinearInterpolation(nodeLeftTop, nodeRightTop);
                        double lerpBottom = LinearInterpolation(nodeLeftBottom, nodeRightBottom);
                        noise[i, j] = LinearInterpolation(lerpTop, lerpBottom);
                    }
                }
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(k);
            }
        }

        private static void ReduceColors(double[,] noise)
        {
            for (int i = 0; i < noise.GetLength(0); i++)
            {
                for (int j = 0; j < noise.GetLength(1); j++)
                {
                    noise[i, j] = Math.Round(noise[i, j], 1);
                }
            }
        }
        private static double LinearInterpolation(double node1, double node2, double pointCoord = 0.5)
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

        private static (double, double) GetRandomVector()
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

        private static double ScalarProduct((double, double) vector1, (double, double) vector2)
        {
            return vector1.Item1 * vector2.Item1 + vector1.Item2 * vector2.Item2;
        }
    }
}

using System;

namespace Generator1
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
                    grid[i, j] = Tools.GetRandomVector();
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

                    double leftTop = Tools.ScalarProduct(pointVector, grid[i, j]);
                    double rightTop = Tools.ScalarProduct(pointVector, grid[i, j + 1]);
                    double leftBottom = Tools.ScalarProduct(pointVector, grid[i + 1, j]);
                    double rightBottom = Tools.ScalarProduct(pointVector, grid[i + 1, j + 1]);

                    double top = Tools.LinearInterpolation(leftTop, rightTop, pointCoordX);
                    double bottom = Tools.LinearInterpolation(leftBottom, rightBottom, pointCoordX);
                    noise[i, j] = Math.Round(Tools.LinearInterpolation(top, bottom, pointCoordY), 1);

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
                        if (i + 1 == noise.GetLength(0)) iIndex = 0;
                        if (j + 1 == noise.GetLength(1)) jIndex = 0;
                        double nodeLeftTop = noise[i, j];
                        double nodeLeftBottom = noise[iIndex + 1, j];
                        double nodeRightTop = noise[i, jIndex + 1];
                        double nodeRightBottom = noise[iIndex + 1, jIndex + 1];

                        double lerpTop = Tools.LinearInterpolation(nodeLeftTop, nodeRightTop);
                        double lerpBottom = Tools.LinearInterpolation(nodeLeftBottom, nodeRightBottom);
                        noise[i, j] = Tools.LinearInterpolation(lerpTop, lerpBottom);
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
       
    }
}

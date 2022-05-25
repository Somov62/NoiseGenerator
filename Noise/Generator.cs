using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise
{
    public static class Generator
    {
        private static readonly int _width = 1000;
        private static readonly int _height = 500;


        private static int[,] GenerateGrid()
        {
            Random rnd = new Random();

            int[,] grid = new int[_height + 1, _width + 1];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = rnd.Next(100);
                }
            }

            return grid;
        }

        public static int[,] GenerateNoise()
        {
            var grid = GenerateGrid();

            int[,] noise = new int[_height, _width];

            for (int i = 0; i < noise.GetLength(0); i++)
            {
                for (int j = 0; j < noise.GetLength(1); j++)
                {
                    int nodeLeftTop = grid[i, j];
                    int nodeLeftBottom = grid[i + 1, j];
                    int nodeRightTop = grid[i, j + 1];
                    int nodeRightBottom = grid[i + 1, j + 1];

                    int lerpTop = LinearInterpolation(nodeLeftTop, nodeRightTop);
                    int lerpBottom = LinearInterpolation(nodeLeftBottom, nodeRightBottom);
                    noise[i, j] = LinearInterpolation(lerpTop, lerpBottom);
                }
            }


            //smooth
            for (int k = 1; k < 100; k++)
            {
                for (int i = 0; i < noise.GetLength(0) - k; i++)
                {
                    for (int j = 0; j < noise.GetLength(1) - k; j++)
                    {
                        int nodeLeftTop = noise[i, j];
                        int nodeLeftBottom = noise[i + 1, j];
                        int nodeRightTop = noise[i, j + 1];
                        int nodeRightBottom = noise[i + 1, j + 1];

                        int lerpTop = LinearInterpolation(nodeLeftTop, nodeRightTop);
                        int lerpBottom = LinearInterpolation(nodeLeftBottom, nodeRightBottom);
                        noise[i, j] = LinearInterpolation(lerpTop, lerpBottom);
                    }
                }
            }
            return noise;
        }

        private static int LinearInterpolation(int node1, int node2, double pointCoord = 0.5)
        {
            pointCoord = QunticCurve(pointCoord);
            return Convert.ToInt32(node1 + (node2 - node1) * pointCoord);
        }

        private static double QunticCurve(double t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }
    }
}

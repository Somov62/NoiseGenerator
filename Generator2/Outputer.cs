using System;

namespace Generator2
{
    public static class Outputer
    {
        public static double[,] GetMapArea(this MapMatrix map, int x, int y, int width, int height)
        {
            int size = map[0, 0]._size;

            double[,] data = new double[height * size, width * size];

            int jIndex = y;
            for (int i = 0; i < height * size; i += size)
            {
                int iIndex = x;
                for (int j = 0; j < width * size; j += size)
                {
                    for (int q = 0; q < size; q++)
                    {
                        for (int k = 0; k < size; k++)
                        {
                            if (map[iIndex, jIndex] == null) continue;
                            data[j + k, i + q] = Math.Round(map[iIndex, jIndex]._data[k, q], 2);
                        }
                    }
                    iIndex++;
                }
                jIndex++;
            }
            return data;
        }

    }
}

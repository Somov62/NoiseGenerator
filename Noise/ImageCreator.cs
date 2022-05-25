using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Noise
{
    internal static class ImageCreator
    {
        private static readonly int _countColors = 100;
        private static readonly int _increaseColor = 255 / _countColors;
        public static void CreateImage(int[,] noise)
        {
            Bitmap flag = new Bitmap(noise.GetLength(1), noise.GetLength(0));

            for (int i = 0; i < noise.GetLength(0); i++)
            {
                for (int j = 0; j < noise.GetLength(1); j++)
                {
                    int colorRatio = 255 - (noise[i, j] * _increaseColor);
                    flag.SetPixel(j, i, Color.FromArgb(colorRatio, colorRatio, colorRatio));
                }
            }

            flag.Save("noise.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}

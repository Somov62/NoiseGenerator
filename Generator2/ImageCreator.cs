using System;
using System.Drawing;

namespace Generator2
{
    public static class ImageCreator
    {
        public static void CreateImage(double[,] noise)
        {
            Bitmap flag = new Bitmap(noise.GetLength(1), noise.GetLength(0));

            for (int i = 0; i < noise.GetLength(0); i++)
            {
                for (int j = 0; j < noise.GetLength(1); j++)
                {
                    int colorRatio = Convert.ToInt32(63 * noise[i, j]) + 128;
                    flag.SetPixel(j, i, Color.FromArgb(colorRatio, colorRatio, colorRatio));
                }
            }

            flag.Save("noise.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}

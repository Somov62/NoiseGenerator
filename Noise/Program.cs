using System;
using System.Diagnostics;
using Generator2;
using Generator1;

namespace Noise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowPosition(0, 0);

            #region gen1

            var noise = Generator1.Generator.GenerateNoise();
            Generator1.ImageCreator.CreateImage(noise);

            #endregion

            #region gen2
            Generator2.Generator generator = new(29102003);

            for (int i = -10; i < 10; i++)
            {
                for (int j = -10; j < 10; j++)
                {
                    generator.GenerateChunk(i, j);
                }
            }
            var map = generator.Map.GetMapArea(-10, -10, 20, 20);

            Generator2.ImageCreator.CreateImage(map);
            #endregion

            Console.WriteLine("success");

            Process.Start("cmd.exe", "/C " + "mspaint.exe " + Environment.CurrentDirectory + @"\noise.png");

            Console.ReadKey();
        }


        public static void OutputNoise<T>(T[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write("{0,7}", map[i, j]);
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}

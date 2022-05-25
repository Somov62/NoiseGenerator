using System;

namespace Noise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowPosition(0, 0);
            var noise = Generator.GenerateNoise();
            //OutputNoise(noise);
            ImageCreator.CreateImage(noise);
            Console.WriteLine("success");
            Console.ReadKey();
        }


        public static void OutputNoise(int[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}

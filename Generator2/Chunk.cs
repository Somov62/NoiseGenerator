using System;

namespace Generator2
{
    public class Chunk
    {
        public readonly int _size;
        public double[,] _data;

        public Chunk(int size = 16) => _size = size;

        public double MainNode { get; private set; }
        public double RightTopNode{ get; private set; }
        public double LeftBottomNode { get; private set; }
        public double RightBottomNode{ get; private set; }

        public bool IsGenerated { get; private set; }

        public double[,] CreateChunk (double mainCoord, double rightTopCoord,
            double rightBottomCoord, double leftBottomCoord)
        {
            MainNode = mainCoord;
            RightTopNode = rightTopCoord;
            LeftBottomNode = leftBottomCoord;
            RightBottomNode = rightBottomCoord;
            double[,] chunk = new double[_size + 1, _size + 1];
            chunk[0, 0] = mainCoord;
            chunk[0, _size] = leftBottomCoord;
            chunk[_size, 0] = rightTopCoord;
            chunk[_size, _size] = rightBottomCoord;
            int j = 1;
            for (double i = 1.0 / _size; j < _size; i += 1.0 / _size, j++)
            {
                chunk[j, 0] = Tools.LinearInterpolation(chunk[0, 0], chunk[_size, 0], i);
                chunk[0, j] = Tools.LinearInterpolation(chunk[0, 0], chunk[0, _size], i);
                chunk[j, _size] = Tools.LinearInterpolation(chunk[0, _size], chunk[_size, _size], i);
                chunk[_size, j] = Tools.LinearInterpolation(chunk[_size, 0], chunk[_size, _size], i);
            }

            for (int i = 1; i < _size; i++)
            {
                for (j = 1; j < _size; j++)
                {
                    double top = chunk[0, j];
                    double bottom = chunk[_size, j];
                    double left = chunk[i, 0];
                    double right = chunk[i, _size];

                    double vertical = Tools.LinearInterpolation(top, bottom, 1.0 / _size * i);
                    double horizontal = Tools.LinearInterpolation(left, right, 1.0 / _size * j);

                    chunk[i, j] = Tools.LinearInterpolation(vertical, horizontal);
                }
            }
            _data = CutMatrix(chunk);
            IsGenerated = true;
            return chunk;
        }

        private double[,] CutMatrix(double[,] noise)
        {
            double[,] chunk = new double[_size, _size];
            for (int i = 0; i < noise.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < noise.GetLength(1) - 1; j++)
                {
                    chunk[i, j] = noise[i, j];
                }
            }
            return chunk;
        }
    }
}

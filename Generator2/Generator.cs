using System;

namespace Generator2
{
    public class Generator
    {
        public MapMatrix _map = new MapMatrix();


        private readonly Random _random;

        public Generator(int seed)
        {
            _random = new Random(seed);
            _map[0, 0] = GenerateChunk(0, 0);
        }

        public MapMatrix Map { get => _map; }
        public Chunk GenerateChunk(int x, int y)
        {
            Chunk chunk = new Chunk(128);
            _map[x, y] = chunk;
            chunk.CreateChunk(GetLeftTop(x, y), GetRightTop(x, y), GetRightBottom(x, y), GetLeftBottom(x, y));
            return chunk;
        }
        private double GetLeftTop(int x, int y)
        {
            if (_map[x - 1, y] != null)
            {
                return _map[x - 1, y].RightTopNode;
            }
            if (_map[x, y - 1] != null)
            {
                return _map[x, y - 1].LeftBottomNode;
            }
            if (_map[x - 1, y - 1] != null)
            {
                return _map[x - 1, y - 1].RightBottomNode;
            }
            return GetRandomValue();
        }

        private double GetLeftBottom(int x, int y)
        {
            if (_map[x - 1, y] != null)
            {
                return _map[x - 1, y].RightBottomNode;
            }
            if (_map[x, y + 1] != null)
            {
                return _map[x, y + 1].MainNode;
            }
            if (_map[x - 1 , y + 1] != null)
            {
                return _map[x - 1, y + 1].RightTopNode;
            }
            return GetRandomValue();
        }

        private double GetRightBottom(int x, int y)
        {
            if (_map[x + 1, y] != null)
            {
                return _map[x + 1, y].LeftBottomNode;
            }
            if (_map[x, y + 1] != null)
            {
                return _map[x, y + 1].RightTopNode;
            }
            if (_map[x + 1, y + 1] != null)
            {
                return _map[x + 1, y + 1].MainNode;
            }
            return GetRandomValue();
        }

        private double GetRightTop(int x, int y)
        {
            if (_map[x + 1, y] != null)
            {
                return _map[x + 1, y].MainNode;
            }
            if (_map[x, y - 1] != null)
            {
                return _map[x, y - 1].RightBottomNode;
            }
            if (_map[x + 1, y - 1] != null)
            {
                return _map[x + 1, y - 1].LeftBottomNode;
            }
            return GetRandomValue();
        }

        private double GetRandomValue()
        {
            return (_random.NextDouble()/**2-1*/) + _random.Next(-2, 2);
        }


    }
}

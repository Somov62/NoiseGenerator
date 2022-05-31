namespace Generator2
{
    public class MapMatrix
    {
        private Chunk[,] _data;
        
        public MapMatrix()
        {
            _data = new Chunk[1, 1];
        }

        public Chunk this[int x, int y] 
        {
            get
            {
                int i = Size / 2 + y;
                int j = Size / 2 + x;
                while (i < 0 || j < 0 || i >= Size || j >= Size)
                {
                    ResizeMatrix();
                    i = Size / 2 + y;
                    j = Size / 2 + x;
                }
                //if (_data[i, j] == null) _data[i, j] = new Chunk();
                return _data[i, j];
            }
            set
            {
                int i = Size / 2 + y;
                int j = Size / 2 + x;
                while (i < 0 || j < 0 || i >= Size || j >= Size)
                {
                    ResizeMatrix();
                    i = Size / 2 + y;
                    j = Size / 2 + x;
                }
                _data[i, j] = value;
            }
        }

        public int Size { get => _data.GetLength(0); }
        public void ResizeMatrix()
        {
            var data = new Chunk[_data.GetLength(0) + 2, _data.GetLength(1) + 2];

            for (int i = 0; i < _data.GetLength(0); i++)
            {
                for (int j = 0; j < _data.GetLength(1); j++)
                {
                    data[i + 1, j + 1] = _data[i, j];
                }
            }
            _data = data;
        }
    }
}

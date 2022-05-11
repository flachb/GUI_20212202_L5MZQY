using System.Collections;
using System.Collections.Generic;

namespace VectorWars.Core.Common
{
    public sealed class Grid : IEnumerable<GridElement>
    {
        private readonly GridElement[,] _gridElements;
        private readonly int _rows, _columns;

        public float SizeOfGrid { get; }

        public GridElement this[int x, int y]
            => _gridElements[x, y];

        public Grid(int x, int y, float sizeOfGrid)
        {
            _gridElements = new GridElement[x, y];
            _rows = x;
            _columns = y;
            SizeOfGrid = sizeOfGrid;

            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    _gridElements[i, j] =
                        new GridElement(
                            (i * sizeOfGrid + sizeOfGrid / 2,
                            j * sizeOfGrid + sizeOfGrid / 2));
                }
        }

        public IEnumerator<GridElement> GetEnumerator()
        {
            for (int x = 0; x < _rows; x++)
                for (int y = 0; y < _columns; y++)
                    yield return _gridElements[x, y];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

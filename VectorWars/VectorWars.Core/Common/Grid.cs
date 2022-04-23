namespace VectorWars.Core.Common
{
    public sealed class Grid
    {
        private readonly GridElement[,] _gridElements;

        public GridElement this[int x, int y]
            => _gridElements[x, y];

        public Grid(int x, int y, float sizeOfGrid)
        {
            _gridElements = new GridElement[x, y];

            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    _gridElements[i, j] =
                        new GridElement(
                            (i * sizeOfGrid + sizeOfGrid / 2,
                            j * sizeOfGrid + sizeOfGrid / 2));
                }
        }
    }
}

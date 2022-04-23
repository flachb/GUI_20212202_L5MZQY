using System.Collections;
using System.Collections.Generic;

namespace VectorWars.Core.Common
{
    public sealed class Path : IReadOnlyCollection<Point>
    {
        private readonly List<Point> _points;

        public Path(params Point[] points)
        {
            _points = new List<Point>(points);
        }

        public Point this[int index]
            => _points[index];

        public int Count => _points.Count;

        public IEnumerator<Point> GetEnumerator()
        {
            return _points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _points.GetEnumerator();
        }
    }
}

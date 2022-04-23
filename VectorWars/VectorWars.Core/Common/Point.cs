using System;

namespace VectorWars.Core.Common
{
    public struct Point : IEquatable<Point>
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Point(float x, float y)
            => (X, Y) = (x, y);

        public override bool Equals(object obj)
            => obj is Point && Equals((Point)obj);

        public bool Equals(Point other)
            => X == other.X && Y == other.Y;

        public override int GetHashCode()
            => X.GetHashCode() ^ Y.GetHashCode();

        public static float Distance(Point a, Point b)
        {
            float diffX = a.X - b.X;
            float diffY = a.Y - b.Y;
            return (float)Math.Sqrt(diffX * diffX + diffY * diffY);
        }

        public static Point operator +(Point point, Vector vector)
            => new Point(point.X + vector.X, point.Y + vector.Y);

        public static Point operator -(Point point, Vector vector)
            => new Point(point.X - vector.X, point.Y - vector.Y);

        public static Vector operator +(Point a, Point b)
            => new Vector(a.X + b.X, a.Y + b.Y);

        public static Vector operator -(Point a, Point b)
            => new Vector(a.X - b.X, a.Y - b.Y);

        public static bool operator ==(Point left, Point right)
            => left.Equals(right);

        public static bool operator !=(Point left, Point right)
        => !(left == right);

        public static implicit operator Point((float x, float y) point)
            => new Point(point.x, point.y);
    }
}

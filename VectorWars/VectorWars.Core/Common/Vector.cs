using System;

namespace VectorWars.Core.Common
{
    public struct Vector : IEquatable<Vector>
    {
        public static Vector Zero => new Vector(0F, 0F);
        public static Vector One => new Vector(1F, 1F);
        public static Vector Up => new Vector(0F, 1F);
        public static Vector Down => new Vector(0F, -1F);
        public static Vector Left => new Vector(-1F, 0F);
        public static Vector Right => new Vector(1F, 0F);

        public float X { get; set; }
        public float Y { get; set; }

        public float Magnitude => (float)Math.Sqrt(X * X + Y * Y);

        private const float EPSILON = 0.00001F;

        public Vector(float x, float y)
            => (X, Y) = (x, y);

        public Vector Normalize()
        {
            float mag = Magnitude;
            if (mag > EPSILON)
                return this / mag;

            return Zero;
        }

        public bool Equals(Vector other)
            => X == other.X && Y == other.Y;

        public override bool Equals(object obj)
            => obj is Vector && Equals((Vector)obj);

        public override int GetHashCode()
            => X.GetHashCode() ^ Y.GetHashCode();

        public static bool operator ==(Vector a, Vector b)
        {
            float diffX = a.X - b.X;
            float diffY = a.Y - b.Y;
            return (diffX * diffX + diffY * diffY) < EPSILON * EPSILON;
        }

        public static bool operator !=(Vector a, Vector b)
            => !(a == b);

        public static Vector operator +(Vector a, Vector b) 
            => new Vector(a.X + b.X, a.Y + b.Y);

        public static Vector operator -(Vector a, Vector b)
            => new Vector(a.X - b.X, a.Y - b.Y);

        public static Vector operator *(Vector a, Vector b)
            => new Vector(a.X * b.X, a.Y * b.Y);

        public static Vector operator /(Vector a, Vector b)
            => new Vector(a.X / b.X, a.Y / b.Y);

        public static Vector operator -(Vector a)
            => new Vector(-a.X, -a.Y);

        public static Vector operator *(Vector a, float b)
            => new Vector(a.X * b, a.Y * b);

        public static Vector operator *(float a, Vector b)
            => new Vector(b.X * a, b.Y * a);

        public static Vector operator /(Vector a, float b)
            => new Vector(a.X / b, a.Y / b);
    }
}

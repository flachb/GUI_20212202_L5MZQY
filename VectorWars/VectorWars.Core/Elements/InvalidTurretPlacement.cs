using System;
using VectorWars.Core.Common;

namespace VectorWars.Core.Elements
{
    internal class InvalidTurretPlacement : IMapElement
    {
        public Point Position { get; }

        public Vector Rotation { get; }

        public float Radius => 0f;

        public event Action<IMapElement> Destroyed;

        public InvalidTurretPlacement(Point position)
        {
            Position = position;
        }

        public void Tick(TimeSpan elapsed)
        {
        }
    }
}

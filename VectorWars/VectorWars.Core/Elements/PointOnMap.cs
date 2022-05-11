using System;
using VectorWars.Core.Common;

namespace VectorWars.Core.Elements
{
    internal class PointOnMap : IMapElement
    {
        public PointOnMap(IMapElement element)
        {
            Position = element.Position;
            Rotation = element.Rotation;
            Radius = element.Radius;
        }

        public Point Position { get; }

        public Vector Rotation { get; }

        public float Radius { get; }

        public event Action<IMapElement> Destroyed;

        public void Tick(TimeSpan elapsed)
        {
            throw new NotImplementedException();
        }
    }
}

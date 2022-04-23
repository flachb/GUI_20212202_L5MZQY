using System;
using VectorWars.Core.Common;

namespace VectorWars.Core.Elements
{
    public interface IMapElement : IUpdatable
    {
        Point Position { get; }
        Vector Rotation { get; }
        float Size { get; }

        event Action<IMapElement> Destroyed;
    }
}

using System.Collections.Generic;
using VectorWars.Core.Common;
using VectorWars.Core.Elements;

namespace VectorWars.Core.Handlers
{
    public interface IReadOnlyHandler<TElement> : IUpdatable
        where TElement : IMapElement
    {
        IReadOnlyList<TElement> Elements { get; }
    }
}

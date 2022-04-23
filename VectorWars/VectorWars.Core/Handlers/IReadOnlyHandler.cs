using System.Collections.Generic;
using VectorWars.Core.Elements;

namespace VectorWars.Core.Handlers
{
    public interface IReadOnlyHandler<TElement> 
        where TElement : IMapElement
    {
        IReadOnlyList<TElement> Elements { get; }
    }
}

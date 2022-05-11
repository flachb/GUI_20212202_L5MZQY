using VectorWars.Core.Elements;

namespace VectorWars.Core.Handlers
{
    public interface IHandler<TElement> : IReadOnlyHandler<TElement> 
        where TElement : IMapElement
    {
        void Add(TElement element);
    }
}

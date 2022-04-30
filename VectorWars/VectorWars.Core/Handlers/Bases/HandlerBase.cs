using System;
using System.Collections.Generic;
using VectorWars.Core.Elements;

namespace VectorWars.Core.Handlers.Bases
{
    public abstract class HandlerBase<TElement> : IHandler<TElement>
        where TElement : class, IMapElement
    {
        protected List<TElement> _elements;

        public IReadOnlyList<TElement> Elements => _elements;

        public void Add(TElement element)
        {
            element.Destroyed += OnElementDestroyed;
            _elements.Add(element);
        }

        private void OnElementDestroyed(IMapElement element)
        {
            _elements.Remove(element as TElement);
        }

        public void Tick(TimeSpan elapsed)
        {
            foreach (var element in _elements)
                element.Tick(elapsed);
        }
    }
}

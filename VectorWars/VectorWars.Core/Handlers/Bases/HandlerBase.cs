using System;
using System.Collections.Generic;
using VectorWars.Core.Elements;

namespace VectorWars.Core.Handlers.Bases
{
    public abstract class HandlerBase<TElement> : IHandler<TElement>
        where TElement : class, IMapElement
    {
        protected List<TElement> _elements = new List<TElement>();
        private List<TElement> _elementsToRemove = new List<TElement>();

        public IReadOnlyList<TElement> Elements => _elements;

        public void Add(TElement element)
        {
            element.Destroyed += OnElementDestroyed;
            _elements.Add(element);
        }

        private void OnElementDestroyed(IMapElement element)
        {
            _elementsToRemove.Add(element as TElement);
        }

        public void Tick(TimeSpan elapsed)
        {
            foreach (var element in _elements)
                element.Tick(elapsed);

            foreach (var element in _elementsToRemove)
                _elements.Remove(element);

            _elementsToRemove.Clear();
        }
    }
}

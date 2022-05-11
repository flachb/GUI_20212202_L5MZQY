using VectorWars.Core.Elements;

namespace VectorWars.Core.Common
{
    public sealed class GridElement
    {
        public GridElementType Type { get; internal set; }
        public Point Center { get; }
        public IMapElement? OccupiedBy { get; set; }

        public GridElement(Point center, IMapElement? mapElement = null)
            => (Center, OccupiedBy) = (center, mapElement);
    }
}

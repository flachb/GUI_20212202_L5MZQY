using VectorWars.Core.Common;
using VectorWars.Core.Elements;
using VectorWars.Core.Elements.Types;

namespace VectorWars.Core.Factories.Types
{
    public delegate IProjectile CreateProjectileDelegate(Point position, IMapElement target);

    public interface IProjectileFactory
    {
        CreateProjectileDelegate Create { get; }
    }
}

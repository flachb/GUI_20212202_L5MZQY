using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;

namespace VectorWars.Core.Factories.Types
{
    public delegate ITurret CreateTurretDelegate(Point position);

    public interface ITurretFactory
    {
        CreateTurretDelegate Create { get; }
    }
}

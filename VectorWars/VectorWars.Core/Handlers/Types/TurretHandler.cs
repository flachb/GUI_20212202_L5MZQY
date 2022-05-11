using VectorWars.Core.Elements.Types;
using VectorWars.Core.Handlers.Bases;

namespace VectorWars.Core.Handlers.Types
{
    internal sealed class TurretHandler : HandlerBase<ITurret>
    {
        public void Remove(ITurret turret)
        {
            _elements.Remove(turret);
        }
    }
}

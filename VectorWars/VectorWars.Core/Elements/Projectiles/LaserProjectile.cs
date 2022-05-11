using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Factories.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Projectiles
{
    public class LaserProjectile : ProjectileBase
    {
        public LaserProjectile(IHandler<IEffect> effectHandler, IEffectFactory effectFactory, Point position, IMapElement target) : base(effectHandler, effectFactory, position, target)
        {
        }
        public override float Speed => 750f; //dummy
        public override float Radius => 15f;

    }
}

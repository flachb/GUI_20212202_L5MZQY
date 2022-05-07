using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Factories.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Projectiles
{
    public class BulletProjectile : ProjectileBase
    {
        public BulletProjectile(IHandler<IEffect> effectHandler, IEffectFactory effectFactory, Point position, IMapElement target) : base(effectHandler, effectFactory, position, target)
        {
        }
        public override float Speed => 100f; //dummy
        public override float Radius => 5f;

    }
}

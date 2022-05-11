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
    public class RocketProjectile : ProjectileBase
    {
        public RocketProjectile(IHandler<IEffect> effectHandler, IEffectFactory effectFactory, Point position, IMapElement target) 
            : base(effectHandler, effectFactory, position, new PointOnMap(target))
        {
        }
        public override float Speed => 200f; //dummy
        public override float Radius => 30f;
    }
}

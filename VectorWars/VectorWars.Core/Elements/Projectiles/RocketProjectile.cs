using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Projectiles
{
    public class RocketProjectile : ProjectileBase
    {
        public override float Speed => 100f; //dummy

        public override IMapElement Target => throw new NotImplementedException();

        public override Point Position { get => Position; protected set => Position = value; }
        public override Vector Rotation { get => Rotation; protected set => Rotation = value; }

        public override float Radius => throw new NotImplementedException();

        protected override IEffect CreateEffect()
        {
            throw new NotImplementedException();
        }
        public RocketProjectile(IHandler<IEffect> effectHandler) : base(effectHandler)
        {
        }
    }
}

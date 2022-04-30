using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Elements.Bases;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Effects
{
    public class BulletEffect : EffectBase
    {
        public override TimeSpan Cooldown => TimeSpan.Zero;

        public override TimeSpan Lifespan => TimeSpan.Zero;

        public override int Damage => 5;

        public override float SpeedModifier => 0f;

        public override float Radius => 1f;

        public BulletEffect(IEnemyFinder enemyFinder) : base(enemyFinder)
        {
        }
    }
}

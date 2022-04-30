using System;
using VectorWars.Core.Common;
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

        public BulletEffect(IEnemyFinder enemyFinder, Point position) : base(enemyFinder, position)
        {
        }
    }
}

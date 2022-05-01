using System;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Effects
{
    public class RocketEffect : EffectBase
    {
        public override TimeSpan Cooldown => TimeSpan.Zero;

        public override TimeSpan Lifespan => TimeSpan.Zero;

        public override int Damage => 20;

        public override float SpeedModifier => 0f;

        public override float Radius => 40f;

        public RocketEffect(IEnemyFinder enemyFinder, Point position) : base(enemyFinder)
        {
        }
    }
}

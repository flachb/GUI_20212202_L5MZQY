using System;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Effects
{
    public class RocketEffect : EffectBase
    {
        public RocketEffect(IEnemyFinder enemyFinder, Point position) : base(enemyFinder, position)
        {
        }

        public override TimeSpan Cooldown => TimeSpan.Zero;

        public override TimeSpan Lifespan => TimeSpan.Zero;

        public override int Damage => 200; //dummy

        public override float SpeedModifier => 0f;

        public override float Radius => 30f; //dummy

    }
}

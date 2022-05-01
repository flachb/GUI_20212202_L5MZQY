using System;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Effects
{
    public class LaserEffects : EffectBase
    {
        public override TimeSpan Cooldown => TimeSpan.Zero;

        public override TimeSpan Lifespan => TimeSpan.Zero;

        public override int Damage => 10;

        public override float SpeedModifier => 0f;

        public override float Radius => 1f;

        public LaserEffects(IEnemyFinder enemyFinder, Point position) : base(enemyFinder)
        {
        }
    }
}

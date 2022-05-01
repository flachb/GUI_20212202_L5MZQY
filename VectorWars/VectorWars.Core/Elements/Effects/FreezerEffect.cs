using System;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Effects
{
    public class FreezerEffect : EffectBase
    {
        public override TimeSpan Cooldown => TimeSpan.FromSeconds(0.2d);

        public override TimeSpan Lifespan => TimeSpan.FromSeconds(2d);

        public override int Damage => 0;

        public override float SpeedModifier => 0.2f; //dummy

        public override float Radius => 40f; //dummy

        public FreezerEffect(IEnemyFinder enemyFinder, Point position) : base(enemyFinder)
        {
        }
    }
}

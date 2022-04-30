using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Elements.Bases;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Effects
{
    public class FreezerEffect : EffectBase
    {
        public override TimeSpan Cooldown => TimeSpan.FromSeconds(0.2d);

        public override TimeSpan Lifespan => TimeSpan.FromSeconds(2d);

        public override int Damage => 0;

        public override float SpeedModifier => 0.2f;

        public override float Radius => 40f;

        public FreezerEffect(IEnemyFinder enemyFinder) : base(enemyFinder)
        {

        }
    }
}

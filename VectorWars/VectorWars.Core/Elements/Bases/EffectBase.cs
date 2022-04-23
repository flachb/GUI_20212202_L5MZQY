using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Bases
{
    public abstract class EffectBase : IEffect
    {
        private readonly IEnemyFinder _enemyFinder;

        public abstract TimeSpan Cooldown { get; }
        public abstract TimeSpan Lifespan { get; }
        public abstract int Damage { get; }
        public abstract float SpeedModifier { get; }
        public abstract float Range { get; } //-
        public abstract Point Position { get; } //-
        public abstract Vector Rotation { get; } //-
        public abstract float Size { get; } //-

        public abstract event Action<IMapElement> Destroyed;

        public EffectBase(IEnemyFinder enemyFinder)
        {
            _enemyFinder = enemyFinder;
        }

        public void Tick(TimeSpan elapsed)
        {
            var effectedEnemy = _enemyFinder.GetEnemies(Position, 0);
        }
    }
}

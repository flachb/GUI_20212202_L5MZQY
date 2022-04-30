using System;
using System.Linq;
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
        public abstract float Radius { get; }
        public Point Position { get; }
        public Vector Rotation { get; }

        public event Action<IMapElement> Destroyed;

        private TimeSpan _currentCooldown = TimeSpan.Zero;
        private TimeSpan _currentLifespan;

        public EffectBase(IEnemyFinder enemyFinder)
        {
            _enemyFinder = enemyFinder;
        }

        public void Tick(TimeSpan elapsed)
        {
            _currentCooldown -= elapsed;
            _currentLifespan -= elapsed;

            if (_currentCooldown > TimeSpan.Zero)
                return;

            if (_currentLifespan < TimeSpan.Zero)
            {
                OnDestroyed();
                return;
            }

            var effectedEnemies = _enemyFinder.GetEnemies(Position, Radius);
            if (!effectedEnemies.Any())
                return;

            foreach (var enemy in effectedEnemies)
            {
                enemy.AddEffect(this);
            }

            _currentCooldown = Cooldown;
        }

        protected void OnDestroyed()
        {
            Destroyed?.Invoke(this);
        }
    }
}

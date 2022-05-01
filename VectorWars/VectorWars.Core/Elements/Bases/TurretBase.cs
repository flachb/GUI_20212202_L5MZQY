using System;
using System.Linq;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Factories.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Bases
{
    public abstract class TurretBase : ITurret
    {
        private readonly IEnemyFinder _enemyFinder;
        private readonly IHandler<IProjectile> _projectileHandler;
        private readonly IProjectileFactory _projectileFactory;

        public abstract TimeSpan Cooldown { get; }
        public abstract int BuyPrice { get; }
        public abstract int SellPrice { get; }
        public Point Position { get; init; }
        public Vector Rotation { get; private set; }
        public abstract float Radius { get; }

        public event Action<IMapElement> Destroyed;

        private TimeSpan _currentCooldown = TimeSpan.Zero;
        private IMapElement? _currentTarget = null;

        public TurretBase(
            IEnemyFinder enemyFinder,
            IHandler<IProjectile> projectileHandler,
            IProjectileFactory projectileFactory,
            Point position)
        {
            _enemyFinder = enemyFinder;
            _projectileHandler = projectileHandler;
            _projectileFactory = projectileFactory;

            Position = position;
        }

        public void Tick(TimeSpan elapsed)
        {
            _currentCooldown -= elapsed;

            if (_currentTarget is not null)
            {
                Rotation = (_currentTarget.Position - Position).Normalize();
            }

            if (_currentCooldown > TimeSpan.Zero)
                return;

            var enemiesInRange = _enemyFinder.GetEnemies(Position, Radius);
            if (!enemiesInRange.Any())
                return;

            if (_currentTarget is null || !enemiesInRange.Contains(_currentTarget))
            {
                _currentTarget = enemiesInRange
                    .OrderBy(e => Point.Distance(Position, e.Position))
                    .FirstOrDefault();
            }

            var projectile = _projectileFactory.Create(Position, _currentTarget);
            _projectileHandler.Add(projectile);

            _currentCooldown = Cooldown;
        }

        protected void OnDestroyed()
        {
            Destroyed?.Invoke(this);
        }
    }
}

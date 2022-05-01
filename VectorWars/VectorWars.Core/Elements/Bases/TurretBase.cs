﻿using System;
using System.Linq;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Bases
{
    public abstract class TurretBase : ITurret
    {
        private readonly IEnemyFinder _enemyFinder;
        private readonly IHandler<IProjectile> _projectileHandler;

        public abstract TimeSpan Cooldown { get; }
        public abstract int BuyPrice { get; }
        public abstract int SellPrice { get; }
        public abstract Point Position { get; }
        public abstract Vector Rotation { get; }
        public abstract float Radius { get; }

        public event Action<IMapElement> Destroyed;

        private TimeSpan _currentCooldown = TimeSpan.Zero;
        private IMapElement? _currentTarget = null;

        public TurretBase(
            IEnemyFinder enemyFinder,
            IHandler<IProjectile> projectileHandler)
        {
            _enemyFinder = enemyFinder;
            _projectileHandler = projectileHandler;
        }

        public void Tick(TimeSpan elapsed)
        {
            _currentCooldown -= elapsed;

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

            var projectile = CreateProjectile(_currentTarget);
            _projectileHandler.Add(projectile);

            _currentCooldown = Cooldown;
        }

        protected void OnDestroyed()
        {
            Destroyed?.Invoke(this);
        }

        protected abstract IProjectile CreateProjectile(IMapElement target);
    }
}

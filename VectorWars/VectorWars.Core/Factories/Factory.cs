using System;
using System.Collections.Generic;
using VectorWars.Core.Common;
using VectorWars.Core.Elements;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Factories.Helpers;
using VectorWars.Core.Factories.Internals;
using VectorWars.Core.Factories.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Factories
{
    public class Factory
    {
        private readonly IEnemyFinder _enemyFinder;
        private readonly IHandler<IEffect> _effectHandler;
        private readonly IHandler<IProjectile> _projectileHandler;

        private readonly Dictionary<(Type turretType, Type projectileType, Type effectType), ITurretFactory> _factoryCache;

        internal Factory(
            IEnemyFinder enemyFinder,
            IHandler<IEffect> effectHandler,
            IHandler<IProjectile> projectileHandler)
        {
            _enemyFinder = enemyFinder;
            _effectHandler = effectHandler;
            _projectileHandler = projectileHandler;

            _factoryCache = new Dictionary<(Type turretType, Type projectileType, Type effectType), ITurretFactory>();
        }

        public ITurret CreateTurret<TTurret, TProjectile, TEffect>(Point position)
            where TTurret : ITurret
            where TProjectile : IProjectile
            where TEffect : IEffect
        {
            var cacheKey = (typeof(TTurret), typeof(TProjectile), typeof(TEffect));

            if (!_factoryCache.TryGetValue(cacheKey, out var factory))
            {
                var effectFactory = CreateEffectFactory<TEffect>();
                var projectileFactory = CreateProjectileFactory<TProjectile>(effectFactory);
                var turretFactory = CreateTurretFactory<TTurret>(projectileFactory);

                _factoryCache.Add(cacheKey, turretFactory);
                factory = turretFactory;
            }

            return factory.Create(position);
        }

        private IEffectFactory CreateEffectFactory<TEffect>()
            where TEffect : IEffect
        {
            var effectActivator =
                ObjectActivator.GetActivator<TEffect>(
                    typeof(IEnemyFinder),
                    typeof(Point));

            CreateEffectDelegate factoryMethod = (position) =>
            {
                return effectActivator(
                    _enemyFinder,
                    position);
            };

            return new InternalEffectFactory(factoryMethod);
        }

        private IProjectileFactory CreateProjectileFactory<TProjectile>(IEffectFactory effectFactory)
            where TProjectile : IProjectile
        {
            var projectileActivator =
                ObjectActivator.GetActivator<TProjectile>(
                    typeof(IHandler<IEffect>),
                    typeof(IEffectFactory),
                    typeof(Point),
                    typeof(IMapElement));

            CreateProjectileDelegate factoryMethod = (position, target) =>
            {
                return projectileActivator(
                    _effectHandler,
                    effectFactory,
                    position,
                    target);
            };

            return new InternalProjectileFactory(factoryMethod);
        }

        private ITurretFactory CreateTurretFactory<TTurret>(IProjectileFactory projectileFactory)
            where TTurret : ITurret
        {
            var turretActivator =
                ObjectActivator.GetActivator<TTurret>(
                    typeof(IEnemyFinder),
                    typeof(IHandler<IProjectile>),
                    typeof(IProjectileFactory),
                    typeof(Point));

            CreateTurretDelegate factoryMethod = (position) =>
            {
                return turretActivator(
                    _enemyFinder,
                    _projectileHandler,
                    projectileFactory,
                    position);
            };

            return new InternalTurretFactory(factoryMethod);
        }
    }
}

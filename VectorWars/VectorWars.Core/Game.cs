using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VectorWars.Core.Common;
using VectorWars.Core.Elements;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Factories;
using VectorWars.Core.Handlers.Types;

namespace VectorWars.Core
{
    public class Game
    {
        private Map _map;
        private readonly Player _player;
        private readonly TurretHandler _turretHandler;
        private readonly ProjectileHandler _projectileHandler;
        private readonly EffectHandler _effectHandler;
        private readonly EnemyHandler _enemyHandler;
        private readonly Factory _turretFactory;
        private readonly MapBuilder _mapBuilder;

        private CancellationTokenSource _cancellationTokenSource;
        private Task _loopTask;

        public Map Map => _map;
        public MapBuilder MapBuilder => _mapBuilder;
        public Player Player => _player;

        public bool IsRunning => !_loopTask?.IsCompleted ?? false;

        public event Action<IEnumerable<IMapElement>> Render;
        public event Action MapFinished;

        public Game(Player player)
        {
            _player = player;

            _turretHandler = new TurretHandler();
            _projectileHandler = new ProjectileHandler();
            _effectHandler = new EffectHandler();
            _enemyHandler = new EnemyHandler();

            _turretFactory = new Factory(_enemyHandler, _effectHandler, _projectileHandler);
            _mapBuilder = new MapBuilder(_enemyHandler);
        }

        public void SetupMap(Map map)
        {
            _map = map;

            _map.EnemyReachedFinish += OnEnemyReachedFinish;
            _map.EnemyKilled += OnEnemyKilled;
            _map.Finished += OnMapFinished;
        }

        public void Start()
        {
            if (!_loopTask?.IsCompleted ?? false)
            {
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            _loopTask = StartLoop(_cancellationTokenSource.Token);
        }

        public void Stop()
        {
            if (!_loopTask?.IsCompleted ?? false)
            {
                _cancellationTokenSource?.Cancel();
            }
        }

        public void AddTurret<TTurret, TProjectile, TEffect>(GridElement gridElement)
            where TTurret : ITurret
            where TProjectile : IProjectile
            where TEffect : IEffect
        {
            if (gridElement.OccupiedBy != null)
                return;

            var turret = _turretFactory.CreateTurret<TTurret, TProjectile, TEffect>(gridElement.Center);

            if (_player.Money < turret.BuyPrice)
                return;

            gridElement.OccupiedBy = turret;

            _turretHandler.Add(turret);
            _player.BoughtTurret(turret);
        }

        public void RemoveTurret(GridElement gridElement)
        {
            var mapElement = gridElement.OccupiedBy;

            if (mapElement == null || mapElement is not ITurret turret)
                return;

            _turretHandler.Remove(turret);
            _player.SoldTurret(turret);
            gridElement.OccupiedBy = null;
        }

        private IEnumerable<IMapElement> GetMapElements()
        {
            foreach (var mapElement in _turretHandler.Elements)
                yield return mapElement;

            foreach (var mapElement in _projectileHandler.Elements)
                yield return mapElement;

            foreach (var mapElement in _effectHandler.Elements)
                yield return mapElement;

            foreach (var mapElement in _enemyHandler.Elements)
                yield return mapElement;
        }

        private Task StartLoop(CancellationToken cancellation)
        {
            return Task.Run(async () =>
            {
                var startTime = DateTime.Now;
                var previousElapsed = startTime;

                while (!cancellation.IsCancellationRequested)
                {
                    var elapsedTime = DateTime.Now - previousElapsed;
                    previousElapsed += elapsedTime;

                    _map.Tick(elapsedTime);
                    _turretHandler.Tick(elapsedTime);
                    _projectileHandler.Tick(elapsedTime);
                    _effectHandler.Tick(elapsedTime);
                    _enemyHandler.Tick(elapsedTime);

                    Render?.Invoke(GetMapElements());
                    await Task.Delay(40);
                }
            });
        }

        private void OnMapFinished()
        {
            MapFinished?.Invoke();
        }

        private void OnEnemyKilled(IEnemy enemy)
        {
            _player.EnemyKilled(enemy);
        }

        private void OnEnemyReachedFinish(IEnemy enemy)
        {
            _player.EnemyReachedBase(enemy);
        }
    }
}

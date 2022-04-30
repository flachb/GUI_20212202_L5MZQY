using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VectorWars.Core.Elements;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Factories;
using VectorWars.Core.Handlers.Types;

namespace VectorWars.Core
{
    public class Game
    {
        private readonly Map _map;
        private readonly Player _player;
        private readonly TurretHandler _turretHandler;
        private readonly ProjectileHandler _projectileHandler;
        private readonly EffectHandler _effectHandler;
        private readonly EnemyHandler _enemyHandler;
        private readonly Factory _turretFactory;

        private CancellationTokenSource _cancellationTokenSource;
        private Task _loopTask;

        public Factory TurretFactory => _turretFactory;

        public event Action<IEnumerable<IMapElement>> Render;
        public event Action MapFinished;

        public Game(Map map, Player player)
        {
            _map = map;
            _player = player;

            _turretHandler = new TurretHandler();
            _projectileHandler = new ProjectileHandler();
            _effectHandler = new EffectHandler();
            _enemyHandler = new EnemyHandler();

            _map.EnemyReachedFinish += OnEnemyReachedFinish;
            _map.EnemyKilled += OnEnemyKilled;
            _map.Finished += OnMapFinished;
        }

        public void Start()
        {
            if (_loopTask?.IsCompleted ?? false)
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

        public void AddTurret(ITurret turret)
        {
            _turretHandler.Add(turret);
            _player.BoughtTurret(turret);
        }

        public void RemoveTurret(ITurret turret)
        {
            _turretHandler.Remove(turret);
            _player.SoldTurret(turret);
        }

        private Task StartLoop(CancellationToken cancellation)
        {
            return Task.Run(() =>
            {
                var startTime = DateTime.Now;

                while (!cancellation.IsCancellationRequested)
                {
                    var elapsedTime = DateTime.Now - startTime;

                    _map.Tick(elapsedTime);
                    _turretHandler.Tick(elapsedTime);
                    _projectileHandler.Tick(elapsedTime);
                    _effectHandler.Tick(elapsedTime);
                    _enemyHandler.Tick(elapsedTime);

                    Render?.Invoke(GetMapElements());
                }
            });
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

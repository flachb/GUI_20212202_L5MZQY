using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VectorWars.Core.Common;
using VectorWars.Core.Elements;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core
{
    public sealed class Map : IUpdatable, INotifyPropertyChanged
    {
        private readonly IHandler<IEnemy> _enemyHandler;

        public Grid Grid { get; }
        public Path EnemyPath { get; }
        public SortedList<int, SortedList<TimeSpan, IEnemy>> Waves { get; }

        public event Action Finished;
        public event Action<IEnemy> EnemyReachedFinish;
        public event Action<IEnemy> EnemyKilled;
        public event PropertyChangedEventHandler PropertyChanged;

        private TimeSpan _totalElapsed = TimeSpan.Zero;
        private int _currentWaveCounter = 0;

        public int CurrentWave = 0;

        internal Map(
            IHandler<IEnemy> enemyHandler,
            Grid grid,
            Path enemyPath,
            SortedList<int, SortedList<TimeSpan, IEnemy>> waves)
        {
            _enemyHandler = enemyHandler;
            Grid = grid;
            EnemyPath = enemyPath;
            Waves = waves;
        }

        public void Tick(TimeSpan elapsed)
        {
            if (_currentWaveCounter >= Waves.Count)
                return;

            _totalElapsed += elapsed;

            var currentWave = Waves[_currentWaveCounter];
            var spawnsToRemove = new List<TimeSpan>();

            try
            {
                foreach ((TimeSpan spawnTime, IEnemy enemy) in currentWave)
                {
                    if (spawnTime <= _totalElapsed)
                    {
                        enemy.Destroyed += OnEnemyDestroyed;
                        _enemyHandler.Add(enemy);
                        spawnsToRemove.Add(spawnTime);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            finally
            {
                foreach (var spawn in spawnsToRemove)
                {
                    currentWave.Remove(spawn);
                }
            }

            _currentWaveCounter++;
            CurrentWave++;
            if (_currentWaveCounter == Waves.Count)
            {
                Finished?.Invoke();
            }
        }

        private void OnEnemyDestroyed(IMapElement enemy)
        {
            if (Point.Distance(enemy.Position, EnemyPath[EnemyPath.Count - 1]) < enemy.Radius / 2)
            {
                EnemyReachedFinish?.Invoke(enemy as IEnemy);
            }
            else
            {
                EnemyKilled?.Invoke(enemy as IEnemy);
            }
        }

        private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

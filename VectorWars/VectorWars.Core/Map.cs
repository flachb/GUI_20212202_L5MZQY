using System;
using System.Collections.Generic;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core
{
    public sealed class Map : IUpdatable
    {
        private readonly IHandler<IEnemy> _enemyHandler;

        public Grid Grid { get; }
        public Path EnemyPath { get; }
        public IEnumerable<EnemySpawnInfo> Waves { get; }

        private TimeSpan _totalElapsed = TimeSpan.Zero;
        private int _currentEnemyCounter = 0;

        public void Tick(TimeSpan elapsed)
        {
            _totalElapsed += elapsed;
            for (int i = _currentEnemyCounter; i < Waves.Count(); i++)
                    {

            }
        }
    }
}

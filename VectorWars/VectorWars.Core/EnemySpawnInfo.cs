using System;
using System.Collections.Generic;
using VectorWars.Core.Elements.Types;

namespace VectorWars.Core
{
    public sealed class EnemySpawnInfo
    {
        public IEnemy Enemy { get; }
        public TimeSpan SpawnTime { get; }
    }
}
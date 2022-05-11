using System.Collections.Generic;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Handlers.Bases;

namespace VectorWars.Core.Handlers.Types
{
    internal sealed class EnemyHandler : HandlerBase<IEnemy>, IEnemyFinder
    {
        public IEnumerable<IEnemy> GetEnemies(Point position, float range)
        {
            foreach (var enemy in Elements)
            {
                var distance = Point.Distance(position, enemy.Position);
                if (distance <= range + enemy.Radius)
                    yield return enemy;
            }
        }
    }
}

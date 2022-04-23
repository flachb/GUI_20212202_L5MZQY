using System.Collections.Generic;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Types;

namespace VectorWars.Core.Handlers
{
    public interface IEnemyFinder
    {
        IEnumerable<IEnemy> GetEnemies(Point point, float range);
    }
}

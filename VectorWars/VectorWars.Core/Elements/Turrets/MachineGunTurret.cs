using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Factories.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Turrets
{
    public class MachineGunTurret : TurretBase
    {
        public override TimeSpan Cooldown => TimeSpan.FromSeconds(0.5);

        public override int BuyPrice => 75;

        public override int SellPrice => 45;

        public override float Radius => 60f;

        public MachineGunTurret(IEnemyFinder enemyFinder, IHandler<IProjectile> projectileHandler, IProjectileFactory projectileFactory, Point position) : base(enemyFinder, projectileHandler, projectileFactory, position)
        {
        }
    }
}

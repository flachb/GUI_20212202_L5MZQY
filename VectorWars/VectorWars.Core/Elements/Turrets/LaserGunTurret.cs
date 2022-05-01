using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;
using VectorWars.Core.Elements.Types;
using VectorWars.Core.Handlers;

namespace VectorWars.Core.Elements.Turrets
{
    public class LaserGunTurret : TurretBase
    {
        public override TimeSpan Cooldown => TimeSpan.FromSeconds(1);

        public override int BuyPrice => 120;

        public override int SellPrice => 70;

        public override Point Position => Position;

        public override Vector Rotation => Rotation;

        public override float Radius => 80f;

        protected override IProjectile CreateProjectile(IMapElement target)
        {
            throw new NotImplementedException();
        }

        public LaserGunTurret(IEnemyFinder enemyFinder, IHandler<IProjectile> projectileHandler) : base(enemyFinder, projectileHandler)
        {

        }
    }
}

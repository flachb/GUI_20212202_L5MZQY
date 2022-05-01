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
    public class RocketLauncherTurret : TurretBase
    {
        public override TimeSpan Cooldown => TimeSpan.FromSeconds(5);

        public override int BuyPrice => 200;

        public override int SellPrice => 110;

        public override Point Position => Position;

        public override Vector Rotation => Rotation;

        public override float Radius => 120f;

        protected override IProjectile CreateProjectile(IMapElement target)
        {
            throw new NotImplementedException();
        }

        public RocketLauncherTurret(IEnemyFinder enemyFinder, IHandler<IProjectile> projectileHandler) : base(enemyFinder, projectileHandler)
        {

        }
    }
}

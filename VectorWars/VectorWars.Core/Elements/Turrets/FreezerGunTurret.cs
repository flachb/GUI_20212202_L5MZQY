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
    public class FreezerGunTurret : TurretBase
    {
        public override TimeSpan Cooldown => TimeSpan.FromSeconds(4);

        public override int BuyPrice => 150;

        public override int SellPrice => 85;

        public override Point Position => Position;

        public override Vector Rotation => Rotation;

        public override float Radius => 60f;

        protected override IProjectile CreateProjectile(IMapElement target)
        {
            throw new NotImplementedException();
        }

        public FreezerGunTurret(IEnemyFinder enemyFinder, IHandler<IProjectile> projectileHandler) : base(enemyFinder, projectileHandler)
        {
        }
    }
}

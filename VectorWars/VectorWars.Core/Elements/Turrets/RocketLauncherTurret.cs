﻿using System;
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
    public class RocketLauncherTurret : TurretBase
    {
        public override TimeSpan Cooldown => TimeSpan.FromSeconds(5);

        public override int BuyPrice => 200;

        public override int SellPrice => 110;

        public override float Radius => 50f;

        public override float Range => 200f;

        public RocketLauncherTurret(IEnemyFinder enemyFinder, IHandler<IProjectile> projectileHandler, IProjectileFactory projectileFactory, Point position) : base(enemyFinder, projectileHandler, projectileFactory, position)
        { 
        }
    }
}

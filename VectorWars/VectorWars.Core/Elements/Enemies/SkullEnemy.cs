using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;

namespace VectorWars.Core.Elements.Enemies
{
    public class SkullEnemy : EnemyBase //Blue & dummy
    {
        public override int Health { get => Health; protected set => Health = 150; }
        public override float Speed { get => Speed; protected set => Speed = 1; }
        public override int Damage { get => Damage; protected set => Damage = 20; }

        public override int Reward => 75;

        public override float Radius => 10f;

        public SkullEnemy(Path path) : base(path)
        {
        }
    }
}

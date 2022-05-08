using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;

namespace VectorWars.Core.Elements.Enemies
{
    public class ManEnemy : EnemyBase //Red & dummy
    {
        public override int Health { get => Health; protected set => Health = 400; }
        public override float Speed { get => Speed; protected set => Speed = 0.5f; }
        public override int Damage { get => Damage; protected set => Damage = 50; }

        public override int Reward => 200;

        public override float Radius => 10f;

        public ManEnemy(Path path) : base(path)
        {
        }
    }
}

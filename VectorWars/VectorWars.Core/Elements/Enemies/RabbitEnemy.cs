using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;

namespace VectorWars.Core.Elements.Enemies
{
    public class RabbitEnemy : EnemyBase //Purple & dummy
    {
        public override int Health { get => Health; protected set => Health = 150; }
        public override float Speed { get => Speed; protected set => Speed = 1.5f; }
        public override int Damage { get => Damage; protected set => Damage = 20; }

        public override int Reward => 100;

        public override float Radius => 10f;

        public RabbitEnemy(Path path) : base(path)
        {
        }
    }
}

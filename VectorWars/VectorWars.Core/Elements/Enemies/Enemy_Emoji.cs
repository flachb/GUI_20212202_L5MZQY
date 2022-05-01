using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;

namespace VectorWars.Core.Elements.Enemies
{
    public class Enemy_Emoji : EnemyBase //Green & dummy
    {
        public override int Health { get => Health; protected set => Health = 100; }
        public override float Speed { get => Speed; protected set => Speed = 1; }
        public override int Damage { get => Damage; protected set => Damage = 10; }

        public override int Reward => 50;

        public override Point Position { get => Position; protected set => Position = value; }
        public override Vector Rotation { get => Rotation; protected set => Rotation = value; }

        public override float Radius => 10f;

        public Enemy_Emoji(Path path) : base(path)
        {
        }
    }
}

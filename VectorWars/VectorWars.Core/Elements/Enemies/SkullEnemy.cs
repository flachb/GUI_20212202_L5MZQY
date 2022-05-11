using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;

namespace VectorWars.Core.Elements.Enemies
{
    public class SkullEnemy : EnemyBase //Blue & dummy
    {
        public static SkullEnemy Default { get; } = new SkullEnemy();
        private SkullEnemy()
            : base(Path.Emtpy) { }

        public override int Health { get; protected set; } = 150;
        public override float Speed { get; protected set; } = 80f;
        public override int Damage { get; protected set; } = 20;

        public override int Reward => 75;

        public override float Radius => 50f;

        public SkullEnemy(Path path) : base(path)
        {
        }
    }
}

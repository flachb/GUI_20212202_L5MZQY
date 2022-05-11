using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;

namespace VectorWars.Core.Elements.Enemies
{
    public class RabbitEnemy : EnemyBase //Purple & dummy
    {
        public static RabbitEnemy Default { get; } = new RabbitEnemy();
        private RabbitEnemy()
            : base(Path.Emtpy) { }

        public override int Health { get; protected set; } = 150;
        public override float Speed { get; protected set; } = 150;
        public override int Damage { get; protected set; } = 20;

        public override int Reward => 100;

        public override float Radius => 15f;

        public RabbitEnemy(Path path) : base(path)
        {
        }
    }
}

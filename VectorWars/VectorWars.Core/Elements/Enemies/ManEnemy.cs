using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;

namespace VectorWars.Core.Elements.Enemies
{
    public class ManEnemy : EnemyBase //Red & dummy
    {
        public static ManEnemy Default { get; } = new ManEnemy();
        private ManEnemy()
            : base(Path.Emtpy) { }

        public override int Health { get; protected set; } = 400;
        public override float Speed { get; protected set; } = 50f;
        public override int Damage { get; protected set; } = 50;

        public override int Reward => 200;

        public override float Radius => 40f;

        public ManEnemy(Path path) : base(path)
        {
        }
    }
}

using VectorWars.Core.Common;
using VectorWars.Core.Elements.Bases;

namespace VectorWars.Core.Elements.Enemies
{
    public class EmojiEnemy : EnemyBase //Green & dummy
    {
        public static EmojiEnemy Default { get; } = new EmojiEnemy();
        private EmojiEnemy()
            : base(Path.Emtpy){ }

        public override int Health { get; protected set; } = 100;
        public override float Speed { get; protected set; } = 90f;
        public override int Damage { get; protected set; } = 10;
        public override int Reward => 50;

        public override float Radius => 20f;


        public EmojiEnemy(Path path) : base(path)
        {
        }
    }
}

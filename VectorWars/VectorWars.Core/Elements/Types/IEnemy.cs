namespace VectorWars.Core.Elements.Types
{
    public interface IEnemy : IMapElement
    {
        int Health { get; set; }

        float Speed { get; set; }

        int Damage { get; }

        int Reward { get; }
    }
}

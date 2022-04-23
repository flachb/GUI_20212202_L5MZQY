namespace VectorWars.Core.Elements.Types
{
    public interface IEnemy : IMapElement
    {
        int Health { get; }

        float Speed { get; }

        int Damage { get; }

        int Reward { get; }
    }
}

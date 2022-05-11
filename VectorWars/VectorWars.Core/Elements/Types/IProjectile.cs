namespace VectorWars.Core.Elements.Types
{
    public interface IProjectile : IMapElement
    {
        float Speed { get; }

        IMapElement Target { get; }
    }
}

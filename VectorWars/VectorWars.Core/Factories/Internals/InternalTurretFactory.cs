using VectorWars.Core.Factories.Types;

namespace VectorWars.Core.Factories.Internals
{
    internal class InternalTurretFactory : ITurretFactory
    {
        public CreateTurretDelegate Create { get; }

        public InternalTurretFactory(CreateTurretDelegate createTurretDelegate)
        {
            Create = createTurretDelegate;
        }
    }
}
